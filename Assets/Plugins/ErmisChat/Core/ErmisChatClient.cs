using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ermis.Core.Configs;
using Ermis.Core.Exceptions;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient;
using Ermis.Core.State;
using Ermis.Core.State.Caches;
using Ermis.Core.Models;
using Ermis.Core.QueryBuilders.Filters;
using Ermis.Core.QueryBuilders.Sort;
using Ermis.Core.Requests;
using Ermis.Core.Responses;
using Ermis.Core.StatefulModels;
using Ermis.Libs;
using Ermis.Libs.AppInfo;
using Ermis.Libs.Auth;
using Ermis.Libs.ChatInstanceRunner;
using Ermis.Libs.Http;
using Ermis.Libs.Logs;
using Ermis.Libs.NetworkMonitors;
using Ermis.Libs.Serialization;
using Ermis.Libs.Time;
using Ermis.Libs.Websockets;
using Ermis.Libs.Utils;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;
#if ERMIS_TESTS_ENABLED
using System.Text;
#endif

namespace Ermis.Core
{
    /// <summary>
    /// Connection has been established
    /// You can access local user data via <see cref="ErmisChatClient.LocalUserData"/>
    /// </summary>
    public delegate void ConnectionMadeHandler(IErmisLocalUserData localUserData);

    /// <summary>
    /// Connection state change handler
    /// </summary>
    public delegate void ConnectionChangeHandler(ConnectionState previous, ConnectionState current);

    /// <summary>
    /// Channel deletion handler
    /// </summary>
    public delegate void ChannelDeleteHandler(string channelCid, string channelId, ChannelType channelType);

    //ErmisTodo: Handle restoring state after lost connection

    public delegate void ChannelInviteHandler(IErmisChannel channel, IErmisUser invitee);

    /// <summary>
    /// Member added to the channel handler
    /// </summary>
    public delegate void ChannelMemberAddedHandler(IErmisChannel channel, IErmisChannelMember member);

    /// <summary>
    /// Member removed from the channel handler
    /// </summary>
    public delegate void ChannelMemberRemovedHandler(IErmisChannel channel, IErmisChannelMember member);

    /// <inheritdoc cref="IErmisChatClient"/>
    public sealed class ErmisChatClient : IErmisChatClient
    {
        public event ConnectionMadeHandler Connected;

        public event Action Disconnected;

        public event Action Disposed;

        public event ConnectionChangeHandler ConnectionStateChanged;

        public event ChannelDeleteHandler ChannelDeleted;

        public event ChannelInviteHandler ChannelInviteReceived;
        public event ChannelInviteHandler ChannelInviteAccepted;
        public event ChannelInviteHandler ChannelInviteRejected;

        public event ChannelMemberAddedHandler AddedToChannelAsMember;
        public event ChannelMemberRemovedHandler RemovedFromChannelAsMember;

        public const int QueryUsersLimitMaxValue = 30;
        public const int QueryUsersOffsetMaxValue = 1000;

        public ConnectionState ConnectionState => InternalLowLevelClient.ConnectionState;

        public bool IsConnected => InternalLowLevelClient.ConnectionState == ConnectionState.Connected;
        public bool IsConnecting => InternalLowLevelClient.ConnectionState == ConnectionState.Connecting;

        public IErmisLocalUserData LocalUserData => _localUserData;

        private ErmisLocalUserData _localUserData;

        public IReadOnlyList<IErmisChannel> WatchedChannels => _cache.Channels.AllItems;

        public double? NextReconnectTime => InternalLowLevelClient.NextReconnectTime;

        public IErmisChatLowLevelClient LowLevelClient => InternalLowLevelClient;

        /// <inheritdoc cref="ErmisChatLowLevelClient.SDKVersion"/>
        public static Version SDKVersion => ErmisChatLowLevelClient.SDKVersion;

        /// <summary>
        /// Recommended method to create an instance of <see cref="IErmisChatClient"/>
        /// If you wish to create an instance with non default dependencies you can use the <see cref="CreateClientWithCustomDependencies"/>
        /// </summary>
        /// <param name="config">[Optional] configuration</param>
        public static IErmisChatClient CreateDefaultClient(IErmisClientConfig config = default)
        {
            if (config == null)
            {
                config = ErmisClientConfig.Default;
            }

            var logs = ErmisDependenciesFactory.CreateLogger(config.LogLevel.ToLogLevel());
            var websocketClient
                = ErmisDependenciesFactory.CreateWebsocketClient(logs, config.LogLevel.IsDebugEnabled());
            var httpClient = ErmisDependenciesFactory.CreateHttpClient();
            var serializer = ErmisDependenciesFactory.CreateSerializer();
            var timeService = ErmisDependenciesFactory.CreateTimeService();
            var applicationInfo = ErmisDependenciesFactory.CreateApplicationInfo();
            var gameObjectRunner = ErmisDependenciesFactory.CreateChatClientRunner();
            var networkMonitor = ErmisDependenciesFactory.CreateNetworkMonitor();

            var client = new ErmisChatClient(websocketClient, httpClient, serializer, timeService, networkMonitor,
                applicationInfo, logs, config);

            gameObjectRunner?.RunChatInstance(client);
            return client;
        }

        /// <summary>
        /// Create instance of <see cref="ITokenProvider"/>
        /// </summary>
        /// <param name="urlFactory">Delegate that will return a valid url that return JWT auth token for a given user ID</param>
        /// <example>
        /// <code>
        /// ErmisChatClient.CreateDefaultTokenProvider(userId => new Uri($"https:your-awesome-page.com/get_token?userId={userId}"));
        /// </code>
        /// </example>
        public static ITokenProvider CreateDefaultTokenProvider(TokenProvider.TokenUriHandler urlFactory)
            => ErmisDependenciesFactory.CreateTokenProvider(urlFactory);

        /// <summary>
        /// Create a new instance of <see cref="IErmisChatLowLevelClient"/> with custom provided dependencies.
        /// If you want to create a default new instance then just use the <see cref="CreateDefaultClient"/>.
        /// Important! Custom created client require calling the <see cref="Update"/> and <see cref="Destroy"/> methods.
        /// </summary>
        public static IErmisChatClient CreateClientWithCustomDependencies(IWebsocketClient websocketClient,
            IHttpClient httpClient, ISerializer serializer, ITimeService timeService, INetworkMonitor networkMonitor,
            IApplicationInfo applicationInfo, ILogs logs, IErmisClientConfig config)
            => new ErmisChatClient(websocketClient, httpClient, serializer, timeService, networkMonitor,
                applicationInfo, logs, config);

        /// <inheritdoc cref="ErmisChatLowLevelClient.CreateDeveloperAuthToken"/>
        public static string CreateDeveloperAuthToken(string userId)
            => ErmisChatLowLevelClient.CreateDeveloperAuthToken(userId);

        /// <inheritdoc cref="ErmisChatLowLevelClient.SanitizeUserId"/>
        public static string SanitizeUserId(string userId) => ErmisChatLowLevelClient.SanitizeUserId(userId);

        public void SetAuthorizationCredentials(AuthCredentials authCredentials)
            => InternalLowLevelClient.SeAuthorizationCredentials(authCredentials);

        public Task<IErmisLocalUserData> ConnectUserAsync(AuthCredentials userAuthCredentials,
            CancellationToken cancellationToken = default)
        {
            InternalLowLevelClient.ConnectUser(userAuthCredentials);

            //ErmisTodo: test calling this method multiple times in a row

            //ErmisTodo: timeout, like 5 seconds?
            _connectUserCancellationToken = cancellationToken;

            _connectUserCancellationTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(_connectUserCancellationToken);
            _connectUserCancellationTokenSource.Token.Register(TryCancelWaitingForUserConnection);

            //ErmisTodo: check if we can pass the cancellation token here
            _connectUserTaskSource = new TaskCompletionSource<IErmisLocalUserData>();
            return _connectUserTaskSource.Task;
        }

        public Task<IErmisLocalUserData> ConnectUserAsync(string apiKey, string userId, string userAuthToken,
            CancellationToken cancellationToken = default)
        {
            ErmisAsserts.AssertNotNullOrEmpty(apiKey, nameof(apiKey));
            ErmisAsserts.AssertNotNullOrEmpty(userId, nameof(userId));
            ErmisAsserts.AssertNotNullOrEmpty(userAuthToken, nameof(userAuthToken));

            return ConnectUserAsync(new AuthCredentials(apiKey, userId, userAuthToken), cancellationToken);
        }

        public async Task<IErmisLocalUserData> ConnectUserAsync(string apiKey, string userId,
            ITokenProvider tokenProvider,
            CancellationToken cancellationToken = default)
        {
            ErmisAsserts.AssertNotNullOrEmpty(apiKey, nameof(apiKey));
            ErmisAsserts.AssertNotNullOrEmpty(userId, nameof(userId));
            ErmisAsserts.AssertNotNull(tokenProvider, nameof(tokenProvider));

            var ownUserDto
                = await InternalLowLevelClient.ConnectUserAsync(apiKey, userId, tokenProvider, cancellationToken);
            return UpdateLocalUser(ownUserDto);
        }

        //ErmisTodo: test scenario: ConnectUserAsync and immediately call DisconnectUserAsync
        //ErmisTodo: this should cancel token that would be globally passed to all async tasks so the moment we disconnect all async tasks are cancelled
        public Task DisconnectUserAsync()
        {
            TryCancelWaitingForUserConnection();
            return InternalLowLevelClient.DisconnectAsync(permanent: true);
        }

        public async Task<ErmisCurrentUnreadCounts> GetLatestUnreadCountsAsync()
        {
            var dto = await InternalLowLevelClient.InternalChannelApi.GetUnreadCountsAsync();
            var response = dto.ToDomain<WrappedUnreadCountsResponseInternalDTO, ErmisCurrentUnreadCounts>();

            _localUserData.TryUpdateFromDto<WrappedUnreadCountsResponseInternalDTO, ErmisLocalUserData>(dto, _cache);

            return response;
        }

        public bool IsLocalUser(IErmisUser user) => LocalUserData.User == user;

        public Task<IErmisChannel> CreateChannelWithIdAsync(ChannelType channelType, string channelId,
            string name = null, IEnumerable<IErmisUser> members = null)
            => InternalCreateChannelWithIdAsync(channelType, channelId, name, presence: true, state: true,
                watch: true, members);

        public Task<SearchPublicChannelResponse> SearchPublicChannelAsync(SearchPublicChannelRequest searchChannelsRequest)
        => InternalSearchPublicChannelAsync(searchChannelsRequest);

        internal async Task<SearchPublicChannelResponse> InternalSearchPublicChannelAsync(SearchPublicChannelRequest searchChannelsRequest)
        {
            var searchResponseDto = await InternalLowLevelClient.InternalChannelApi.SearchPublicChannelAsync(searchChannelsRequest.TrySaveToDto());
            SearchPublicChannelResponse searchPublicChannelResponse = null;
            return searchPublicChannelResponse.TryLoadFromDto(searchResponseDto);
        }

        public Task<IErmisChannel> CreateChannelWithMembersAsync(ChannelType channelType,
            IEnumerable<IErmisUser> members)
        => InternalGetOrCreateChannelWithMemberAsync(channelType, members);

        public async Task<IEnumerable<IErmisChannel>> QueryChannelsAsync(GetChannelsRequest request=null)
        {
            if(request==null)
            {
                request=new GetChannelsRequest();
                request.FilterConditions = new FilterConditions();
                request.FilterConditions.Type = new List<string>();
                request.FilterConditions.Type.Add("general");
                request.FilterConditions.Type.Add("team");
                request .FilterConditions.Type.Add("messaging");
                request.FilterConditions.Limit = null;
                request.FilterConditions.Offset = 0;
                request.FilterConditions.Roles = new List<string>();
                request.FilterConditions.Roles.Add("owner");
                request.FilterConditions.Roles.Add("moder");
                request.FilterConditions.Roles.Add("member");
                request.FilterConditions.OtherRoles = new List<string>();
                //requestBodyDto.FilterConditions.OtherRoles.Add("pending");
                request.FilterConditions.Banned = false;
                request.FilterConditions.Blocked = false;
                request.FilterConditions.ProjectId = InternalLowLevelClient.GetProjectId();
                request.Sort = new List<Sort>();
                Sort _sort = new Sort();
                _sort.Field = "last_message_at";
                _sort.Direction = -1;
                request.Sort.Add(_sort);
                request.MessageLimit = 25;
            }    
           
            var channelsResponseDto
                = await InternalLowLevelClient.InternalChannelApi.QueryChannelsAsync(request.TrySaveToDto());
            if (channelsResponseDto.Channels == null || channelsResponseDto.Channels.Count == 0)
            {
                return Enumerable.Empty<ErmisChannel>();
            }
            var result = new List<IErmisChannel>();
            foreach (var channelDto in channelsResponseDto.Channels)
            {
                result.Add(_cache.TryCreateOrUpdate(channelDto));
            }

            return result;
        }

        [Obsolete("This method will be removed in the future. Please use the other overload method that uses " +
                  nameof(IFieldFilterRule) + " type filters")]
        public async Task<IEnumerable<IErmisChannel>> QueryChannelsAsync(IDictionary<string, object> filters,
            ChannelSortObject sort = null, int limit = 30, int offset = 0)
        {
            ErmisAsserts.AssertWithinRange(limit, 0, 30, nameof(limit));
            ErmisAsserts.AssertGreaterThanOrEqualZero(offset, nameof(offset));

            //ErmisTodo: Perhaps MessageLimit and MemberLimit should be configurable
            var requestBodyDto = new QueryChannelsRequestInternalDTO
            {
                FilterConditions = filters?.ToDictionary(x => x.Key, x => x.Value),
                Limit = limit,
                MemberLimit = null,
                MessageLimit = null,
                Offset = offset,
                Presence = true,

                /*
                 * ErmisTodo: Allowing to sort query can potentially lead to mixed sorting in WatchedChannels
                 * But there seems no other choice because its too limiting to force only a global sorting for channels
                 * e.g. user may want to show channels in multiple ways with different sorting which would not work with global only sorting
                 */
                Sort = sort?.ToSortParamRequestList(),
                State = true,
                Watch = true,
            };

            var channelsResponseDto
                = await InternalLowLevelClient.InternalChannelApi.QueryChannelsAsync(requestBodyDto);
            if (channelsResponseDto.Channels == null || channelsResponseDto.Channels.Count == 0)
            {
                return Enumerable.Empty<ErmisChannel>();
            }

            var result = new List<IErmisChannel>();
            foreach (var channelDto in channelsResponseDto.Channels)
            {
                result.Add(_cache.TryCreateOrUpdate(channelDto));
            }

            return result;
        }

        [Obsolete("This method will be removed in the future. Please use the other overload method that uses " +
                  nameof(IFieldFilterRule) + " type filters")]
        public async Task<IEnumerable<IErmisUser>> QueryUsersAsync(IDictionary<string, object> filters = null)
        {
            //ErmisTodo: Missing filter, and stuff like IdGte etc
            var requestBodyDto = new QueryUsersRequestInternalDTO
            {
                FilterConditions = filters?.ToDictionary(x => x.Key, x => x.Value) ?? new Dictionary<string, object>(),
                IdGt = null,
                IdGte = null,
                IdLt = null,
                IdLte = null,
                Limit = null,
                Offset = null,
                Presence = true, //ErmisTodo: research whether user should be allowed to control this
                Sort = null,
            };

            var response = await InternalLowLevelClient.InternalUserApi.QueryUsersAsync(requestBodyDto);
            if (response == null || response.Users == null || response.Users.Count == 0)
            {
                return Enumerable.Empty<IErmisUser>();
            }

            var result = new List<IErmisUser>();
            foreach (var userDto in response.Users)
            {
                result.Add(_cache.TryCreateOrUpdate(userDto));
            }

            return result;
        }

        public async Task<IEnumerable<IErmisUser>> QueryUsersAsync(IEnumerable<IFieldFilterRule> filters = null,
            UsersSortObject sort = null, int offset = 0, int limit = 30)
        {
            ErmisAsserts.AssertWithinRange(limit, 0, QueryUsersLimitMaxValue, nameof(limit));
            ErmisAsserts.AssertWithinRange(offset, 0, QueryUsersOffsetMaxValue, nameof(offset));

            //ErmisTodo: Missing IdGt, IdLt, etc. We could wrap all pagination parameters in a single struct
            var requestBodyDto = new QueryUsersRequestInternalDTO
            {
                FilterConditions
                    = filters?.Select(f => f.GenerateFilterEntry()).ToDictionary(x => x.Key, x => x.Value) ??
                      new Dictionary<string, object>(),
                IdGt = null,
                IdGte = null,
                IdLt = null,
                IdLte = null,
                Limit = limit,
                Offset = offset,
                Presence = true, //ErmisTodo: research whether user should be allowed to control this
                Sort = sort?.ToSortParamInternalDTOs(),
            };

            var response = await InternalLowLevelClient.InternalUserApi.QueryUsersAsync(requestBodyDto);

            if (response == null || response.Users == null || response.Users.Count == 0)
            {
                return Enumerable.Empty<IErmisUser>();
            }

            var result = new List<IErmisUser>();
            foreach (var userDto in response.Users)
            {
                result.Add(_cache.TryCreateOrUpdate(userDto));
            }

            return result;
        }

        public async Task<IEnumerable<IErmisUser>> QueryUsersListAsync()
        {
            var response = await InternalLowLevelClient.InternalUserApi.QueryUsersAsync(null);
            if (response == null || response.Users == null || response.Users.Count == 0)
            {
                return Enumerable.Empty<IErmisUser>();
            }

            var result = new List<IErmisUser>();
            foreach (var userDto in response.Users)
            {
                result.Add(_cache.TryCreateOrUpdate(userDto));
            }

            return result;
        }

        //ErmisTodo: write tests
        public async Task<IEnumerable<ErmisUserBanInfo>> QueryBannedUsersAsync(
            ErmisQueryBannedUsersRequest ermisQueryBannedUsersRequest)
        {
            ErmisAsserts.AssertNotNull(ermisQueryBannedUsersRequest, nameof(ermisQueryBannedUsersRequest));

            var response =
                await InternalLowLevelClient.InternalModerationApi.QueryBannedUsersAsync(ermisQueryBannedUsersRequest
                    .TrySaveToDto());
            if (response.Bans == null || response.Bans.Count == 0)
            {
                return Enumerable.Empty<ErmisUserBanInfo>();
            }

            var result = new List<ErmisUserBanInfo>();
            foreach (var userDto in response.Bans)
            {
                var banInfo = new ErmisUserBanInfo().LoadFromDto(userDto, _cache);
                result.Add(banInfo);
            }

            return result;
        }

        public async Task<IEnumerable<IErmisUser>> UpsertUsers(IEnumerable<ErmisUserUpsertRequest> userRequests)
        {
            ErmisAsserts.AssertNotNullOrEmpty(userRequests, nameof(userRequests));

            //ErmisTodo: items could be null
            var requestDtos = userRequests.Select(_ => _.TrySaveToDto<UserRequestInternalDTO>())
                .ToDictionary(_ => _.Id, _ => _);

            var response = await InternalLowLevelClient.InternalUserApi.UpsertManyUsersAsync(
                new UpdateUsersRequestInternalDTO
                {
                    Users = requestDtos
                });

            var result = new List<IErmisUser>();
            foreach (var userDto in response.Users.Values)
            {
                result.Add(_cache.TryCreateOrUpdate(userDto));
            }

            return result;
        }

        public async Task MuteMultipleChannelsAsync(IEnumerable<IErmisChannel> channels, int? milliseconds = default)
        {
            ErmisAsserts.AssertNotNullOrEmpty(channels, nameof(channels));

            var channelCids = channels.Select(_ => _.Cid).ToList();
            if (channelCids.Count == 0)
            {
                throw new ArgumentException($"{nameof(channels)} is empty");
            }

            var response = await InternalLowLevelClient.InternalChannelApi.MuteChannelAsync(
                new MuteChannelRequestInternalDTO
                {
                    ChannelCids = channelCids,
                    Expiration = milliseconds
                });

            UpdateLocalUser(response.OwnUser);
        }

        public async Task UnmuteMultipleChannelsAsync(IEnumerable<IErmisChannel> channels)
        {
            if (channels == null)
            {
                throw new ArgumentNullException(nameof(channels));
            }

            var channelCids = channels.Select(_ => _.Cid).ToList();
            if (channelCids.Count == 0)
            {
                throw new ArgumentException($"{nameof(channels)} is empty");
            }

            await InternalLowLevelClient.InternalChannelApi.UnmuteChannelAsync(new UnmuteChannelRequestInternalDTO
            {
                ChannelCids = channelCids,
                //ErmisTodo: what is this Expiration here?
            });
        }

        public async Task<ErmisDeleteChannelsResponse> DeleteMultipleChannelsAsync(
            IEnumerable<IErmisChannel> channels,
            bool isHardDelete = false)
        {
            ErmisAsserts.AssertNotNullOrEmpty(channels, nameof(channels));

            var responseDto = await InternalLowLevelClient.InternalChannelApi.DeleteChannelsAsync(
                new DeleteChannelsRequestInternalDTO
                {
                    Cids = channels.Select(_ => _.Cid).ToList(),
                    HardDelete = isHardDelete
                });

            var response = new ErmisDeleteChannelsResponse().UpdateFromDto(responseDto);
            return response;
        }

        public async Task MuteMultipleUsersAsync(IEnumerable<IErmisUser> users, int? timeoutMinutes = default)
        {
            //ErmisAsserts.AssertNotNullOrEmpty(users, nameof(users));

            //var responseDto = await InternalLowLevelClient.InternalModerationApi.MuteUserAsync(
            //    new MuteUserRequestInternalDTO
            //    {
            //        TargetIds = users.Select(_ => _.Id).ToList(),
            //        Timeout = timeoutMinutes
            //    });

            //UpdateLocalUser(responseDto.OwnUser);
        }

        private Task<IEnumerable<IErmisUser>> QueryBannedUsersAsync()
        {
            //ErmisTodo: IMPLEMENT, should we allow for query
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            //ErmisTodo: disconnect current user

            TryCancelWaitingForUserConnection();

            if (InternalLowLevelClient != null)
            {
                UnsubscribeFrom(InternalLowLevelClient);
                InternalLowLevelClient.Dispose();
            }

            _isDisposed = true;
            Disposed?.Invoke();
        }

        void IErmisChatClientEventsListener.Destroy()
        {
            //ErmisTodo: we should probably check: if waiting for connection -> cancel, if connected -> disconnect, etc
            DisconnectUserAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    _logs.Exception(t.Exception);
                    return;
                }

                Dispose();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        void IErmisChatClientEventsListener.Update() => InternalLowLevelClient.Update(_timeService.DeltaTime);

        internal ErmisChatLowLevelClient InternalLowLevelClient { get; }

        // We probably don't want to expose the presence, state, watch params to the public API
        internal async Task<IErmisChannel> InternalCreateChannelWithIdAsync(ChannelType channelType,
            string channelId,
            string name = null, bool presence = true, bool state = true, bool watch = true,
            IEnumerable<IErmisUser> members = null)
        {
            ErmisAsserts.AssertChannelTypeIsValid(channelType);
            ErmisAsserts.AssertChannelIdLength(channelId);

            channelId = InternalLowLevelClient.GetProjectId() + ":" + channelId;
            CreateChannelRequest requestDto = new CreateChannelRequest();
            requestDto.ChannelInfo = new CreateChannelInfo();
            requestDto.ChannelInfo.Members = new List<string>();
            if (members != null)
            {
                foreach (var member in members)
                {
                    requestDto.ChannelInfo.Members.Add(member.Id);
                }
            }
            requestDto.ChannelInfo.Name = name;
            requestDto.ProjectId = InternalLowLevelClient.GetProjectId();
            requestDto.Messages = new CreateChannelMessages();
            requestDto.Messages.Limit = 25;

            var channelResponseDto = await InternalLowLevelClient.InternalChannelApi.CreateChannelAsync(
                channelType,
                channelId, requestDto.TrySaveToDto());
            return _cache.TryCreateOrUpdate(channelResponseDto);
        }

        internal async Task<IErmisChannel> InternalGetOrCreateChannelWithMemberAsync(ChannelType channelType,
            IEnumerable<IErmisUser> members)
        {
            ErmisAsserts.AssertChannelTypeIsValid(channelType);
            ErmisAsserts.AssertNotNullOrEmpty(members, nameof(members));

            CreateChannelRequest requestDto = new CreateChannelRequest();

            requestDto.ChannelInfo = new CreateChannelInfo();
            requestDto.ChannelInfo.Members = new List<string>();
            requestDto.ChannelInfo.Members.Add(_localUserData.UserId);
            foreach (var member in members)
            {

                requestDto.ChannelInfo.Members.Add(member.Id);
            }
            requestDto.ProjectId = InternalLowLevelClient.GetProjectId();
            requestDto.Messages = new CreateChannelMessages();
            requestDto.Messages.Limit = 25;

            var channelResponseDto =
                await InternalLowLevelClient.InternalChannelApi.CreateChannelAsync(channelType, requestDto.TrySaveToDto());
            return _cache.TryCreateOrUpdate(channelResponseDto);
        }


        

        internal IErmisLocalUserData UpdateLocalUser(OwnUserInternalDTO ownUserInternalDto)
        {
            _localUserData = _cache.TryCreateOrUpdate(ownUserInternalDto);

            if (LocalUserData == null)
            {
                _logs.Error("Local User Data is null");
                return _localUserData;
            }

            if (LocalUserData.ChannelMutes != null)
            {
                //ErmisTodo: Can we not rely on whoever called TryCreateOrUpdate to update this but make it more reliable? Better to react to some event
                // This could be solved if ChannelMutes would be an observable collection
                foreach (var channel in _cache.Channels.AllItems)
                {
                    var isMuted = LocalUserData.ChannelMutes.Any(_ => _.Channel == channel);
                    channel.Muted = isMuted;
                }
            }
            else
            {
                _logs.Info("ChannelMutes is null");
            }

            return _localUserData;
        }

        internal Task RefreshChannelState(string cid)
        {
            if (!_cache.Channels.TryGet(cid, out var channel))
            {
                _logs.Error($"Tried to refresh state of channel with {cid} but no such channel was found in the cache");
                return Task.CompletedTask;
            }

            return QueryChannelsAsync();
        }

        private readonly ILogs _logs;
        private readonly ITimeService _timeService;
        private readonly ICache _cache;

        private TaskCompletionSource<IErmisLocalUserData> _connectUserTaskSource;
        private CancellationToken _connectUserCancellationToken;
        private CancellationTokenSource _connectUserCancellationTokenSource;
        private bool _isDisposed;

        /// <summary>
        /// Use the <see cref="CreateDefaultClient"/> to create the default client instance.
        /// <example>
        /// Default example::
        /// <code>
        /// var ermisChatClient = ErmisChatClient.CreateDefaultClient();
        /// </code>
        /// </example>
        /// <example>
        /// Example with custom config:
        /// <code>
        /// var ermisChatClient = ErmisChatClient.CreateDefaultClient(new ErmisClientConfig
        /// {
        ///     LogLevel = ErmisLogLevel.Debug
        /// });
        /// </code>
        /// </example>
        /// In case you want to inject custom dependencies into the chat client you can use the <see cref="CreateClientWithCustomDependencies"/>
        /// </summary>
        private ErmisChatClient(IWebsocketClient websocketClient, IHttpClient httpClient, ISerializer serializer,
            ITimeService timeService, INetworkMonitor networkMonitor, IApplicationInfo applicationInfo, ILogs logs,
            IErmisClientConfig config)
        {
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));

            InternalLowLevelClient = new ErmisChatLowLevelClient(authCredentials: default, websocketClient, httpClient,
                serializer, _timeService, networkMonitor, applicationInfo, logs, config);

            _cache = new Cache(this, serializer, _logs);

            SubscribeTo(InternalLowLevelClient);
        }

        private void InternalDeleteChannel(ErmisChannel channel)
        {
            //ErmisTodo: mark ErmisChannel object as deleted + probably silent clear all internal data?
            _cache.Channels.Remove(channel);
            ChannelDeleted?.Invoke(channel.Cid, channel.Id, channel.Type);
        }

        private void TryCancelWaitingForUserConnection()
        {
            var isConnectTaskRunning = _connectUserTaskSource?.Task != null && !_connectUserTaskSource.Task.IsCompleted;
            var isCancellationRequested = _connectUserCancellationTokenSource?.IsCancellationRequested ?? false;

            if (isConnectTaskRunning && !isCancellationRequested)
            {
#if ERMIS_DEBUG_ENABLED
                _logs.Info($"Try Cancel {_connectUserTaskSource}");
#endif
                _connectUserTaskSource.TrySetCanceled();
            }
        }

        private async Task InternalGetOrCreateChannelAsync(ChannelType channelType, string channelId)
        {
#if ERMIS_TESTS_ENABLED
            const int maxAttempts = 10;
#else
            const int maxAttempts = 1;
#endif

            for (int i = 1; i <= maxAttempts; i++)
            {
                try
                {
                    await CreateChannelWithIdAsync(channelType, channelId);
                }
                catch (ErmisApiException ermisException)
                {
                    if (!ermisException.IsRateLimitExceededError() || i == maxAttempts)
                    {
                        throw;
                    }

                    if (ConnectionState != ConnectionState.Connected)
                    {
                        break;
                    }

                    var delay = 4 * i;
#if ERMIS_TESTS_ENABLED
                    _logs.Warning(
                        $"InternalGetOrCreateChannelAsync attempt failed due to rate limit. Wait {delay} seconds and try again");
#endif
                    //ErmisTodo: pass CancellationToken
                    await Task.Delay(delay * 1000);

                    if (ConnectionState != ConnectionState.Connected)
                    {
                        break;
                    }
                }
            }
        }

        #region Events

        private void OnConnected(HealthCheckEventInternalDTO dto)
        {
            try
            {
                var localUserDto = dto.Me;

                // This can sometimes be null. I think it's when the client lost network and believes he's reconnecting
                // but the healthcheck timeout didn't pass on server and from the server perspective the client never disconnected
                if (localUserDto != null)
                {
                    UpdateLocalUser(localUserDto);
                }
                else
                {
                    _logs.Warning("OnConnected localUserDto was NULL and current LocalUserData is " +
                                  (LocalUserData != null) + " value " + LocalUserData);
                }

                Connected?.Invoke(LocalUserData);
            }
            finally
            {
                // This will be null if the ConnectUserAsync with token provider was used
                if (_connectUserTaskSource != null)
                {
                    _connectUserTaskSource.SetResult(LocalUserData);
                    _connectUserTaskSource = null;
                }
            }

            RestoreStateLostDuringDisconnect().LogIfFailed();
        }

        private Task RestoreStateLostDuringDisconnect()
        {
            if (!WatchedChannels.Any())
            {
                return Task.CompletedTask;
            }

            return LowLevelClient.FetchAndProcessEventsSinceLastReceivedEvent(WatchedChannels.Select(c => c.Cid));
        }

        private void OnDisconnected() => Disconnected?.Invoke();

        private void OnConnectionStateChanged(ConnectionState previous, ConnectionState current)
            => ConnectionStateChanged?.Invoke(previous, current);

        private void OnMessageDeleted(MessageDeletedEventInternalDTO eventMessageDeleted)
        {
            if (_cache.Channels.TryGet(eventMessageDeleted.Cid, out var ermisChannel))
            {
                ermisChannel.HandleMessageDeletedEvent(eventMessageDeleted);
            }
        }

        private void OnMessageUpdated(MessageUpdatedEventInternalDTO eventMessageUpdated)
        {
            if (_cache.Channels.TryGet(eventMessageUpdated.Cid, out var ermisChannel))
            {
                ermisChannel.HandleMessageUpdatedEvent(eventMessageUpdated);
            }
        }

        private void OnMessageReceived(MessageNewEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.HandleMessageNewEvent(eventDto);
            }
        }

        private void OnChannelTruncated(ChannelTruncatedEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.HandleChannelTruncatedEvent(eventDto);
            }
        }

        private void OnChannelDeletedNotification(NotificationChannelDeletedEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                InternalDeleteChannel(ermisChannel);
            }
        }

        private void OnChannelVisible(ChannelVisibleEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.Hidden = false;
            }
        }

        private void OnChannelHidden(ChannelHiddenEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.Hidden = true;
            }
        }

        private void OnChannelDeleted(ChannelDeletedEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                InternalDeleteChannel(ermisChannel);
            }
        }

        private void OnChannelUpdated(ChannelUpdatedEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.HandleChannelUpdatedEvent(eventDto);
            }
        }

        private void OnChannelTruncatedNotification(
            NotificationChannelTruncatedEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.HandleChannelTruncatedEvent(eventDto);
            }
        }

        private void OnChannelMutesUpdatedNotification(NotificationChannelMutesUpdatedEventInternalDTO eventDto)
        {
            UpdateLocalUser(eventDto.Me);
        }

        private void OnMessageReceivedNotification(NotificationNewMessageEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.InternalHandleMessageNewNotification(eventDto);
            }
        }

        private void OnMutesUpdatedNotification(NotificationMutesUpdatedEventInternalDTO eventDto)
        {
            UpdateLocalUser(eventDto.Me);
        }

        private void OnMemberAdded(MemberAddedEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                var member = _cache.TryCreateOrUpdate(eventDto.Member);
                ErmisAsserts.AssertNotNull(member, nameof(member));
                ermisChannel.InternalAddMember(member);
            }
        }

        private void OnMemberUpdated(MemberUpdatedEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                var member = _cache.TryCreateOrUpdate(eventDto.Member);
                ErmisAsserts.AssertNotNull(member, nameof(member));
                ermisChannel.InternalUpdateMember(member);
            }
        }

        private void OnMemberRemoved(MemberRemovedEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                var member = _cache.TryCreateOrUpdate(eventDto.Member);
                ErmisAsserts.AssertNotNull(member, nameof(member));
                ermisChannel.InternalRemoveMember(member);
            }
        }

        private void OnMessageRead(MessageReadEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.InternalHandleMessageReadEvent(eventDto);
            }
        }

        private void OnMarkReadNotification(NotificationMarkReadEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.InternalHandleMessageReadNotification(eventDto);
            }

            _localUserData.InternalHandleMarkReadNotification(eventDto);
        }

        private void OnAddedToChannelNotification(NotificationAddedToChannelEventInternalDTO eventDto)
        {
#if ERMIS_TESTS_ENABLED
            var sb = new StringBuilder();
            sb.AppendLine("OnAddedToChannelNotification");
            sb.AppendLine($"{nameof(eventDto.ChannelType)}: {eventDto.ChannelType}");
            sb.AppendLine($"{nameof(eventDto.Channel.Type)}: {eventDto.Channel.Type}");
            sb.AppendLine($"{nameof(eventDto.Channel.Id)}: {eventDto.Channel.Id}");
            sb.AppendLine($"{nameof(eventDto.Channel.Cid)}: {eventDto.Channel.Cid}");
#endif

            var channel = _cache.TryCreateOrUpdate(eventDto.Channel, out var wasCreated);

#if ERMIS_TESTS_ENABLED
            sb.Length = 0;
            sb.AppendLine("Channel returned from cache:");
            sb.AppendLine($"{nameof(channel.Type)}: {channel.Type}");
            sb.AppendLine($"{nameof(channel.Id)}: {channel.Id}");
            sb.AppendLine($"{nameof(channel.Cid)}: {channel.Cid}");
            _logs.Info(sb.ToString());
#endif

            var member = _cache.TryCreateOrUpdate(eventDto.Member);
            _cache.TryCreateOrUpdate(eventDto.Member.User);

            if (!wasCreated)
            {
                AddedToChannelAsMember?.Invoke(channel, member);
                return;
            }
            string id = channel.Id;
            if (channel.Id.Contains(InternalLowLevelClient.GetProjectId()))
            {
                id = channel.Id.Split(":")[1];
            }
            // Watch channel, otherwise WS events won't be received
            InternalGetOrCreateChannelAsync(channel.Type, id).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    _logs.Error($"Failed to watch channel with type: {channel.Type} & id: {channel.Id} " +
                                $"before triggering the {nameof(AddedToChannelAsMember)} event. Inspect the following exception: " +
                                t.Exception);
                    _logs.Exception(t.Exception);
                    return;
                }

                AddedToChannelAsMember?.Invoke(channel, member);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnRemovedFromChannelNotification(
            NotificationRemovedFromChannelEventInternalDTO eventDto)
        {
#if ERMIS_TESTS_ENABLED
            var sb = new StringBuilder();
            sb.AppendLine("OnRemovedFromChannelNotification BEFORE CACHE");
            sb.AppendLine($"{nameof(eventDto.ChannelType)}: {eventDto.ChannelType}");
            sb.AppendLine($"{nameof(eventDto.Channel.Type)}: {eventDto.Channel.Type}");
            sb.AppendLine($"{nameof(eventDto.Channel.Id)}: {eventDto.Channel.Id}");
            sb.AppendLine($"{nameof(eventDto.Channel.Cid)}: {eventDto.Channel.Cid}");
            _logs.Info(sb.ToString());
#endif
            var channel = _cache.TryCreateOrUpdate(eventDto.Channel, out var wasCreated);

#if ERMIS_TESTS_ENABLED
            sb.Length = 0;
            sb.AppendLine("Channel returned FROM CACHE:");
            sb.AppendLine($"{nameof(channel.Type)}: {channel.Type}");
            sb.AppendLine($"{nameof(channel.Id)}: {channel.Id}");
            sb.AppendLine($"{nameof(channel.Cid)}: {channel.Cid}");
            _logs.Info(sb.ToString());
#endif

            var member = _cache.TryCreateOrUpdate(eventDto.Member);
            _cache.TryCreateOrUpdate(eventDto.Member.User);

            if (!wasCreated)
            {
                RemovedFromChannelAsMember?.Invoke(channel, member);
                return;
            }

            // Watch channel, otherwise WS events won't be received
            InternalGetOrCreateChannelAsync(channel.Type, channel.Id).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    _logs.Error($"Failed to watch channel with type: {channel.Type} & id: {channel.Id} " +
                                $"before triggering the {nameof(RemovedFromChannelAsMember)} event. Inspect the following exception: " +
                                t.Exception);
                    _logs.Exception(t.Exception);
                    return;
                }

                RemovedFromChannelAsMember?.Invoke(channel, member);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnInvitedNotification(NotificationInvitedEventInternalDTO eventDto)
        {
            var channel = _cache.TryCreateOrUpdate(eventDto.Channel, out var wasCreated);
            var user = _cache.TryCreateOrUpdate(eventDto.User);

            if (!wasCreated)
            {
                ChannelInviteReceived?.Invoke(channel, user);
                return;
            }

            // Watch channel, otherwise WS events won't be received
            InternalGetOrCreateChannelAsync(channel.Type, channel.Id).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    _logs.Error($"Failed to watch channel with type: {channel.Type} & id: {channel.Id} " +
                                $"before triggering the {nameof(ChannelInviteReceived)} event. Inspect the following exception: " +
                                t.Exception);
                    _logs.Exception(t.Exception);
                    return;
                }

                ChannelInviteReceived?.Invoke(channel, user);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnInviteAcceptedNotification(NotificationInviteAcceptedEventInternalDTO eventDto)
        {
            var channel = _cache.TryCreateOrUpdate(eventDto.Channel, out var wasCreated);
            var user = _cache.TryCreateOrUpdate(eventDto.User);

            if (!wasCreated)
            {
                ChannelInviteAccepted?.Invoke(channel, user);
                return;
            }

            // Watch channel, otherwise WS events won't be received
            InternalGetOrCreateChannelAsync(channel.Type, channel.Id).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    _logs.Error($"Failed to watch channel with type: {channel.Type} & id: {channel.Id} " +
                                $"before triggering the {nameof(ChannelInviteAccepted)} event. Inspect the following exception: " +
                                t.Exception);
                    _logs.Exception(t.Exception);
                    return;
                }

                ChannelInviteAccepted?.Invoke(channel, user);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnInviteRejectedNotification(NotificationInviteRejectedEventInternalDTO eventDto)
        {
            var channel = _cache.TryCreateOrUpdate(eventDto.Channel, out var wasCreated);
            var user = _cache.TryCreateOrUpdate(eventDto.User);

            if (!wasCreated)
            {
                ChannelInviteRejected?.Invoke(channel, user);
                return;
            }

            // Watch channel, otherwise WS events won't be received
            InternalGetOrCreateChannelAsync(channel.Type, channel.Id).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    _logs.Error($"Failed to watch channel with type: {channel.Type} & id: {channel.Id} " +
                                $"before triggering the {nameof(ChannelInviteRejected)} event. Inspect the following exception: " +
                                t.Exception);
                    _logs.Exception(t.Exception);
                    return;
                }

                ChannelInviteRejected?.Invoke(channel, user);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnReactionReceived(ReactionNewEventInternalDTO eventDto)
        {
            if (!_cache.Channels.TryGet(eventDto.Cid, out var channel))
            {
                return;
            }

            if (_cache.Messages.TryGet(eventDto.Message.Id, out var message))
            {
                var reaction
                    = new ErmisReaction().TryLoadFromDto<ReactionResponseInternalDTO, ErmisReaction>(eventDto.Reaction,
                        _cache);
                message.HandleReactionNewEvent(eventDto, channel, reaction);
                channel.InternalNotifyReactionReceived(message, reaction);
            }
        }

        private void OnReactionUpdated(ReactionUpdatedEventInternalDTO eventDto)
        {
            if (!_cache.Channels.TryGet(eventDto.Cid, out var channel))
            {
                return;
            }

            if (_cache.Messages.TryGet(eventDto.Message.Id, out var message))
            {
                var reaction
                    = new ErmisReaction().TryLoadFromDto<ReactionResponseInternalDTO, ErmisReaction>(eventDto.Reaction,
                        _cache);
                message.HandleReactionUpdatedEvent(eventDto, channel, reaction);
                channel.InternalNotifyReactionUpdated(message, reaction);
            }
        }

        private void OnReactionDeleted(ReactionDeletedEventInternalDTO eventDto)
        {
            if (!_cache.Channels.TryGet(eventDto.Cid, out var channel))
            {
                return;
            }

            if (_cache.Messages.TryGet(eventDto.Message.Id, out var message))
            {
                var reaction
                    = new ErmisReaction().TryLoadFromDto<ReactionResponseInternalDTO, ErmisReaction>(eventDto.Reaction,
                        _cache);
                message.HandleReactionDeletedEvent(eventDto, channel, reaction);
                channel.InternalNotifyReactionDeleted(message, reaction);
            }
        }

        private void OnUserWatchingStop(UserWatchingStopEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.InternalHandleUserWatchingStop(eventDto);
            }
        }

        private void OnUserWatchingStart(UserWatchingStartEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.InternalHandleUserWatchingStartEvent(eventDto);
            }
        }

        private void OnLowLevelClientUserUnbanned(UserUnbannedEventInternalDTO obj)
        {
            //ErmisTodo: IMPLEMENT
        }

        private void OnLowLevelClientUserBanned(UserBannedEventInternalDTO obj)
        {
            //ErmisTodo: IMPLEMENT
        }

        private void OnLowLevelClientUserDeleted(UserDeletedEventInternalDTO obj)
        {
            //ErmisTodo: IMPLEMENT
        }

        private void OnLowLevelUserUpdated(UserUpdatedEventInternalDTO eventDto)
        {
            if (_cache.Users.TryGet(eventDto.User.Id, out var ermisUser))
            {
                _cache.TryCreateOrUpdate(eventDto.User);
            }
        }

        private void OnUserPresenceChanged(UserPresenceChangedEventInternalDTO eventDto)
        {
            if (_cache.Users.TryGet(eventDto.User.Id, out var ermisUser))
            {
                ermisUser.InternalHandlePresenceChanged(eventDto);
            }
        }

        private void OnTypingStopped(TypingStopEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.InternalHandleTypingStopped(eventDto);
            }
        }

        private void OnTypingStarted(TypingStartEventInternalDTO eventDto)
        {
            if (_cache.Channels.TryGet(eventDto.Cid, out var ermisChannel))
            {
                ermisChannel.InternalHandleTypingStarted(eventDto);
            }
        }

        private void SubscribeTo(ErmisChatLowLevelClient lowLevelClient)
        {
            lowLevelClient.InternalConnected += OnConnected;
            lowLevelClient.Disconnected += OnDisconnected;
            lowLevelClient.ConnectionStateChanged += OnConnectionStateChanged;

            lowLevelClient.InternalMessageReceived += OnMessageReceived;
            lowLevelClient.InternalMessageUpdated += OnMessageUpdated;
            lowLevelClient.InternalMessageDeleted += OnMessageDeleted;
            lowLevelClient.InternalMessageRead += OnMessageRead;

            lowLevelClient.InternalChannelUpdated += OnChannelUpdated;
            lowLevelClient.InternalChannelDeleted += OnChannelDeleted;
            lowLevelClient.InternalChannelTruncated += OnChannelTruncated;
            lowLevelClient.InternalChannelVisible += OnChannelVisible;
            lowLevelClient.InternalChannelHidden += OnChannelHidden;

            lowLevelClient.InternalMemberAdded += OnMemberAdded;
            lowLevelClient.InternalMemberRemoved += OnMemberRemoved;
            lowLevelClient.InternalMemberUpdated += OnMemberUpdated;

            lowLevelClient.InternalUserPresenceChanged += OnUserPresenceChanged;
            lowLevelClient.InternalUserUpdated += OnLowLevelUserUpdated;
            lowLevelClient.InternalUserDeleted += OnLowLevelClientUserDeleted;
            lowLevelClient.InternalUserBanned += OnLowLevelClientUserBanned;
            lowLevelClient.InternalUserUnbanned += OnLowLevelClientUserUnbanned;

            lowLevelClient.InternalUserWatchingStart += OnUserWatchingStart;
            lowLevelClient.InternalUserWatchingStop += OnUserWatchingStop;

            lowLevelClient.InternalReactionReceived += OnReactionReceived;
            lowLevelClient.InternalReactionUpdated += OnReactionUpdated;
            lowLevelClient.InternalReactionDeleted += OnReactionDeleted;

            lowLevelClient.InternalTypingStarted += OnTypingStarted;
            lowLevelClient.InternalTypingStopped += OnTypingStopped;

            lowLevelClient.InternalNotificationChannelMutesUpdated += OnChannelMutesUpdatedNotification;

            lowLevelClient.InternalNotificationMutesUpdated += OnMutesUpdatedNotification;
            lowLevelClient.InternalNotificationMessageReceived += OnMessageReceivedNotification;
            lowLevelClient.InternalNotificationMarkRead += OnMarkReadNotification;

            lowLevelClient.InternalNotificationChannelDeleted += OnChannelDeletedNotification;
            lowLevelClient.InternalNotificationChannelTruncated += OnChannelTruncatedNotification;

            lowLevelClient.InternalNotificationAddedToChannel += OnAddedToChannelNotification;
            lowLevelClient.InternalNotificationRemovedFromChannel += OnRemovedFromChannelNotification;

            lowLevelClient.InternalNotificationInvited += OnInvitedNotification;
            lowLevelClient.InternalNotificationInviteAccepted += OnInviteAcceptedNotification;
            lowLevelClient.InternalNotificationInviteRejected += OnInviteRejectedNotification;
        }

        private void UnsubscribeFrom(ErmisChatLowLevelClient lowLevelClient)
        {
            lowLevelClient.InternalConnected -= OnConnected;
            lowLevelClient.Disconnected -= OnDisconnected;
            lowLevelClient.ConnectionStateChanged -= OnConnectionStateChanged;

            lowLevelClient.InternalMessageReceived -= OnMessageReceived;
            lowLevelClient.InternalMessageUpdated -= OnMessageUpdated;
            lowLevelClient.InternalMessageDeleted -= OnMessageDeleted;
            lowLevelClient.InternalMessageRead -= OnMessageRead;

            lowLevelClient.InternalChannelUpdated -= OnChannelUpdated;
            lowLevelClient.InternalChannelDeleted -= OnChannelDeleted;
            lowLevelClient.InternalChannelTruncated -= OnChannelTruncated;
            lowLevelClient.InternalChannelVisible -= OnChannelVisible;
            lowLevelClient.InternalChannelHidden -= OnChannelHidden;

            lowLevelClient.InternalMemberAdded -= OnMemberAdded;
            lowLevelClient.InternalMemberRemoved -= OnMemberRemoved;
            lowLevelClient.InternalMemberUpdated -= OnMemberUpdated;

            lowLevelClient.InternalUserPresenceChanged -= OnUserPresenceChanged;
            lowLevelClient.InternalUserUpdated -= OnLowLevelUserUpdated;
            lowLevelClient.InternalUserDeleted -= OnLowLevelClientUserDeleted;
            lowLevelClient.InternalUserBanned -= OnLowLevelClientUserBanned;
            lowLevelClient.InternalUserUnbanned -= OnLowLevelClientUserUnbanned;

            lowLevelClient.InternalUserWatchingStart -= OnUserWatchingStart;
            lowLevelClient.InternalUserWatchingStop -= OnUserWatchingStop;

            lowLevelClient.InternalReactionReceived -= OnReactionReceived;
            lowLevelClient.InternalReactionUpdated -= OnReactionUpdated;
            lowLevelClient.InternalReactionDeleted -= OnReactionDeleted;

            lowLevelClient.InternalTypingStarted -= OnTypingStarted;
            lowLevelClient.InternalTypingStopped -= OnTypingStopped;

            lowLevelClient.InternalNotificationChannelMutesUpdated -= OnChannelMutesUpdatedNotification;

            lowLevelClient.InternalNotificationMutesUpdated -= OnMutesUpdatedNotification;
            lowLevelClient.InternalNotificationMessageReceived -= OnMessageReceivedNotification;
            lowLevelClient.InternalNotificationMarkRead -= OnMarkReadNotification;

            lowLevelClient.InternalNotificationChannelDeleted -= OnChannelDeletedNotification;
            lowLevelClient.InternalNotificationChannelTruncated -= OnChannelTruncatedNotification;

            lowLevelClient.InternalNotificationAddedToChannel -= OnAddedToChannelNotification;
            lowLevelClient.InternalNotificationRemovedFromChannel -= OnRemovedFromChannelNotification;

            lowLevelClient.InternalNotificationInvited -= OnInvitedNotification;
            lowLevelClient.InternalNotificationInviteAccepted -= OnInviteAcceptedNotification;
            lowLevelClient.InternalNotificationInviteRejected -= OnInviteRejectedNotification;
        }

        #endregion
    }
}