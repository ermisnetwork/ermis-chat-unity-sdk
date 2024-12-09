using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.State;
using Ermis.Core.State.Caches;
using Ermis.Core.Models;
using Ermis.Core.Requests;
using Ermis.Core.Responses;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.StatefulModels
{
    public delegate void ErmisChannelVisibilityHandler(IErmisChannel channel, bool isHidden);

    public delegate void ErmisChannelMuteHandler(IErmisChannel channel, bool isMuted);

    public delegate void ErmisChannelMessageHandler(IErmisChannel channel, IErmisMessage message);

    public delegate void ErmisMessageDeleteHandler(IErmisChannel channel, IErmisMessage message, bool isHardDelete);

    public delegate void ErmisChannelChangeHandler(IErmisChannel channel);

    public delegate void ErmisChannelUserChangeHandler(IErmisChannel channel, IErmisUser user);

    public delegate void ErmisChannelMemberChangeHandler(IErmisChannel channel, IErmisChannelMember member);

    public delegate void ErmisChannelMemberAnyChangeHandler(IErmisChannel channel, IErmisChannelMember member,
        OperationType operationType);

    public delegate void ErmisMessageReactionHandler(IErmisChannel channel, IErmisMessage message,
        ErmisReaction reaction);

    internal sealed class ErmisChannel : ErmisStatefulModelBase<ErmisChannel>,
        IUpdateableFrom<ChannelStateResponseInternalDTO, ErmisChannel>,
        IUpdateableFrom<ChannelResponseInternalDTO, ErmisChannel>,
        IUpdateableFrom<ChannelStateResponseFieldsInternalDTO, ErmisChannel>,
        IUpdateableFrom<UpdateChannelResponseInternalDTO, ErmisChannel>,
        IErmisChannel
    {
        public event ErmisChannelMessageHandler MessageReceived;

        public event ErmisChannelMessageHandler MessageUpdated;

        public event ErmisMessageDeleteHandler MessageDeleted;

        public event ErmisMessageReactionHandler ReactionAdded;

        public event ErmisMessageReactionHandler ReactionRemoved;

        public event ErmisMessageReactionHandler ReactionUpdated;

        public event ErmisChannelMemberChangeHandler MemberAdded;

        public event ErmisChannelMemberChangeHandler MemberRemoved;

        public event ErmisChannelMemberChangeHandler MemberUpdated;

        public event ErmisChannelMemberAnyChangeHandler MembersChanged;

        public event ErmisChannelVisibilityHandler VisibilityChanged;

        public event ErmisChannelMuteHandler MuteChanged;

        public event ErmisChannelChangeHandler Truncated;

        public event ErmisChannelChangeHandler Updated;

        public event ErmisChannelUserChangeHandler WatcherAdded;

        public event ErmisChannelUserChangeHandler WatcherRemoved;

        public event ErmisChannelUserChangeHandler UserStartedTyping;

        public event ErmisChannelUserChangeHandler UserStoppedTyping;

        public event ErmisChannelChangeHandler TypingUsersChanged;

        #region Channel

        public bool AutoTranslationEnabled { get; private set; }

        public string AutoTranslationLanguage { get; private set; }

        public string Cid { get; private set; }

        public ErmisChannelConfig Config { get; private set; }

        public int? Cooldown { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public IErmisUser CreatedBy { get; private set; }

        public DateTimeOffset? DeletedAt { get; private set; }

        public bool Disabled { get; private set; }

        public bool Frozen { get; private set; }

        public bool Hidden
        {
            get => _hidden;
            internal set
            {
                if (TrySet(ref _hidden, value))
                {
                    VisibilityChanged?.Invoke(this, Hidden);
                }

                _hidden = value;
            }
        }

        public DateTimeOffset? HideMessagesBefore { get; private set; }

        public string Id { get; private set; }

        public DateTimeOffset? LastMessageAt { get; private set; }

        public int MemberCount { get; private set; }

        public IReadOnlyList<IErmisChannelMember> Members => _members;

        public DateTimeOffset? MuteExpiresAt { get; private set; }

        public bool Muted
        {
            get => _muted;
            internal set
            {
                if (_muted == value)
                {
                    return;
                }

                _muted = value;
                MuteChanged?.Invoke(this, value);
            }
        }

        public IReadOnlyList<string> OwnCapabilities => _ownCapabilities;

        public string Team { get; private set; }

        public DateTimeOffset? TruncatedAt { get; private set; }

        public IErmisUser TruncatedBy { get; private set; }

        public ChannelType Type { get; private set; }

        public DateTimeOffset? UpdatedAt { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }
        public int MemberMessageCooldown { get; private set; }
        public bool Public { get; private set; }
        public List<string> FilterWords { get; private set; }
        public string image { get; private set; }

        #endregion

        #region ChannelState

        public IErmisChannelMember Membership { get; private set; }

        public IReadOnlyList<IErmisMessage> Messages => _messages;

        public IReadOnlyList<ErmisPendingMessage> PendingMessages => _pendingMessages;

        public IReadOnlyList<IErmisMessage> PinnedMessages => _pinnedMessages;

        public IReadOnlyList<ErmisRead> Read => _read;

        public int WatcherCount { get; private set; }

        public IReadOnlyList<IErmisUser> Watchers => _watchers;

        public IReadOnlyList<IErmisUser> TypingUsers => _typingUsers;

        #endregion

        public bool IsDirectMessage => Members.Count == 2 && Members.Any(m => m.User == Client.LocalUserData.User);

        public Task<IErmisMessage> SendNewMessageAsync(string message)
            => SendNewMessageAsync(new ErmisSendMessageRequest
            {
                Text = message
            });

        public async Task<IErmisMessage> SendNewMessageAsync(ErmisSendMessageRequest sendMessageRequest)
        {
            ErmisAsserts.AssertNotNull(sendMessageRequest, nameof(sendMessageRequest));

            var response = await LowLevelClient.InternalMessageApi.SendNewMessageAsync(Type, Id,
                sendMessageRequest.TrySaveToDto());

            //ErmisTodo: we update internal cache message without server confirmation that message got accepted. e.g. message could be rejected
            //It's ok to update the cache "in good faith" to not introduce update delay but we should handle if message got rejected
            var ermisMessage = InternalAppendOrUpdateMessage(response.Message);
            return ermisMessage;
        }

        public async Task LoadOlderMessagesAsync()
        {
            var oldestMessage = _messages.OrderBy(_ => _.CreatedAt).FirstOrDefault();

            var request = new ChannelGetOrCreateRequestInternalDTO
            {
                //ErmisTodo: presence could be optional in config
                Presence = true,
                State = true,
                Watch = true,
            };

            if (oldestMessage != null)
            {
                request.Messages = new MessagePaginationParamsRequestInternalDTO
                {
                    IdLt = oldestMessage.Id,
                };
            }

            var response = await LowLevelClient.InternalChannelApi.GetOrCreateChannelAsync(Type, Id, request);
            Cache.TryCreateOrUpdate(response);
        }

        public async Task UpdateOverwriteAsync(ErmisUpdateOverwriteChannelRequest updateOverwriteRequest)
        {
            ErmisAsserts.AssertNotNull(updateOverwriteRequest, nameof(updateOverwriteRequest));

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelAsync(Type, Id,
                updateOverwriteRequest.TrySaveToDto());

            Cache.TryCreateOrUpdate(response.Channel);
        }

        public async Task UpdatePartialAsync(IDictionary<string, object> setFields = null,
            IEnumerable<string> unsetFields = null)
        {
            if (setFields == null && unsetFields == null)
            {
                throw new ArgumentNullException(
                    $"{nameof(setFields)} and {nameof(unsetFields)} cannot be both null");
            }

            if (unsetFields != null && !unsetFields.Any())
            {
                throw new ArgumentException($"{nameof(unsetFields)} cannot be empty");
            }

            if (setFields != null && !setFields.Any())
            {
                throw new ArgumentException($"{nameof(setFields)} cannot be empty");
            }

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelPartialAsync(Type, Id,
                new UpdateChannelPartialRequestInternalDTO
                {
                    Set = setFields?.ToDictionary(p => p.Key, p => p.Value),
                    Unset = unsetFields?.ToList(),
                });

            Cache.TryCreateOrUpdate(response.Channel);
        }

        public async Task<ErmisFileUploadResponse> UploadFileAsync(byte[] fileContent, string fileName)
        {
            ErmisAsserts.AssertNotNullOrEmpty(fileContent, nameof(fileContent));
            ErmisAsserts.AssertNotNullOrEmpty(fileName, nameof(fileName));

            var response = await LowLevelClient.InternalMessageApi.UploadFileAsync(Type, Id, fileContent, fileName);
            return new ErmisFileUploadResponse(response.File);
        }

        public Task DeleteFileOrImageAsync(string fileUrl)
        {
            ErmisAsserts.AssertNotNullOrEmpty(fileUrl, nameof(fileUrl));
            return LowLevelClient.InternalMessageApi.DeleteFileAsync(Type, Id, fileUrl);
        }

        public async Task<ErmisImageUploadResponse> UploadImageAsync(byte[] imageContent, string imageName)
        {
            ErmisAsserts.AssertNotNullOrEmpty(imageContent, nameof(imageContent));
            ErmisAsserts.AssertNotNullOrEmpty(imageName, nameof(imageName));

            var response = await LowLevelClient.InternalMessageApi.UploadImageAsync(Type, Id, imageContent, imageName);
            return new ErmisImageUploadResponse().LoadFromDto(response, Cache);
        }

        //ErmisTodo: IMPLEMENT, this should probably work like LoadNextMembers, LoadPreviousMembers? what about sorting - in config?
        //Perhaps we should have both, maybe user wants to search members and not only paginate joined
        public async Task<IEnumerable<IErmisChannelMember>> QueryMembersAsync(
            IDictionary<string, object> filters = null, int limit = 30, int offset = 0)
        {
            // filter_conditions is required by API but empty object is accepted
            if (filters == null)
            {
                filters = new Dictionary<string, object>();
            }

            var response = await LowLevelClient.InternalChannelApi.QueryMembersAsync(new QueryMembersRequestInternalDTO
            {
                CreatedAtAfter = null,
                CreatedAtAfterOrEqual = null,
                CreatedAtBefore = null,
                CreatedAtBeforeOrEqual = null,
                FilterConditions = filters.ToDictionary(k => k.Key, v => v.Value),
                Id = Id,
                Limit = limit,
                Members = null, //ErmisTodo: test & implement distinct members querying + consider exposing rest of params
                Offset = offset,
                Sort = null,
                Type = Type,
                //User = null, //ErmisTodo: server-side only, remove from DTO
                //UserId = null,
                UserIdGt = null,
                UserIdGte = null,
                UserIdLt = null,
                UserIdLte = null,
            });

            if (response.Members == null || response.Members.Count == 0)
            {
                return Enumerable.Empty<IErmisChannelMember>();
            }

            var result = new List<IErmisChannelMember>();
            foreach (var member in response.Members)
            {
                result.Add(Cache.TryCreateOrUpdate(member));
            }

            return result;
        }

        public Task<IEnumerable<IErmisChannelMember>> QueryMembers(IDictionary<string, object> filters = null,
            int limit = 30, int offset = 0)
            => QueryMembersAsync(filters, limit, offset);

        //ErmisTodo: IMPLEMENT, perhap Load Prev/Next Watchers? sorting in config?
        public void QueryWatchers()
        {
            throw new NotImplementedException(
                "This feature is not implemented yet, please raise GH issue to have this implement asap");
        }

        //ErmisTodo: Write tests for banning and unbanning, test also shadow ban message being marked
        public Task BanUserAsync(IErmisUser user, string reason = "",
            int? timeoutMinutes = default, bool isIpBan = false)
            => InternalBanUserAsync(user, isShadowBan: false, reason, timeoutMinutes, isIpBan);

        public Task BanMemberAsync(IErmisChannelMember member, string reason = "",
            int? timeoutMinutes = default, bool isIpBan = false)
        {
            ErmisAsserts.AssertNotNull(member, nameof(member));
            return InternalBanUserAsync(member.User, isShadowBan: false, reason, timeoutMinutes, isIpBan);
        }

        public Task ShadowBanUserAsync(IErmisUser user, string reason = "",
            int? timeoutMinutes = default, bool isIpBan = false)
            => InternalBanUserAsync(user, isShadowBan: true, reason, timeoutMinutes, isIpBan);

        public Task ShadowBanMemberAsync(IErmisChannelMember member, string reason = "",
            int? timeoutMinutes = default, bool isIpBan = false)
        {
            ErmisAsserts.AssertNotNull(member, nameof(member));
            return InternalBanUserAsync(member.User, isShadowBan: true, reason, timeoutMinutes, isIpBan);
        }

        //ErmisTodo: check what happens if user doesn't belong to this channel
        public Task UnbanUserAsync(IErmisUser user)
        {
            ErmisAsserts.AssertNotNull(user, nameof(user));
            return LowLevelClient.InternalModerationApi.UnbanUserAsync(user.Id, Type, Id);
        }

        //ErmisTodo: remove empty request object
        public Task MarkChannelReadAsync()
        {
            return LowLevelClient.InternalChannelApi.MarkReadAsync(Type, Id);
        }

        //ErmisTodo: remove empty request object
        public Task ShowAsync()
            => LowLevelClient.InternalChannelApi.ShowChannelAsync(Type, Id, new ShowChannelRequestInternalDTO());

        //ErmisTodo: write test
        public Task HideAsync(bool? clearHistory = default)
            => LowLevelClient.InternalChannelApi.HideChannelAsync(Type, Id, new HideChannelRequestInternalDTO
            {
                ClearHistory = clearHistory
            });

        public Task AddMembersAsync(IEnumerable<IErmisUser> users, bool? hideHistory = default,
            ErmisMessageRequest optionalMessage = default)
        {
            ErmisAsserts.AssertNotNull(users, nameof(users));
            return AddMembersAsync(users.Select(u => u.Id), hideHistory, optionalMessage);
        }

        public Task AddMembersAsync(bool? hideHistory = default, ErmisMessageRequest optionalMessage = default,
            params IErmisUser[] users)
            => AddMembersAsync(users, hideHistory, optionalMessage);

        public async Task AddMembersAsync(IEnumerable<string> userIds, bool? hideHistory = default,
            ErmisMessageRequest optionalMessage = default)
        {
            ErmisAsserts.AssertNotNull(userIds, nameof(userIds));

            var membersRequest = new List<ChannelMemberInternalDTO>();


            AddMemberRequest addMember = new AddMemberRequest();

            foreach (var u in userIds)
            {
                addMember.AddMembers.Add(u);
            }

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelAddMembersAsync(Type, Id,
               addMember.TrySaveToDto());
            Cache.TryCreateOrUpdate(response);
        }

        public Task AddMembersAsync(bool? hideHistory = default, ErmisMessageRequest optionalMessage = default,
            params string[] users)
            => AddMembersAsync(users, hideHistory, optionalMessage);

        public Task RemoveMembersAsync(IEnumerable<IErmisChannelMember> members)
        {
            ErmisAsserts.AssertNotNull(members, nameof(members));
            return RemoveMembersAsync(members.Select(_ => _.User.Id));
        }

        public Task RemoveMembersAsync(params IErmisChannelMember[] members)
            => RemoveMembersAsync(members as IEnumerable<IErmisChannelMember>);

        public Task RemoveMembersAsync(IEnumerable<IErmisUser> members)
        {
            ErmisAsserts.AssertNotNull(members, nameof(members));
            return RemoveMembersAsync(members.Select(_ => _.Id));
        }

        public Task RemoveMembersAsync(params IErmisUser[] members)
            => RemoveMembersAsync(members as IEnumerable<IErmisUser>);

        public async Task RemoveMembersAsync(IEnumerable<string> userIds)
        {
            ErmisAsserts.AssertNotNull(userIds, nameof(userIds));

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelRemoveMembersAsync(Type, Id,
                new RemoveMemberRequestInternalDTO
                {
                    RemoveMembers = userIds.ToList()

                });
            Cache.TryCreateOrUpdate(response);
        }

        public async Task PromoteMembersAsync(IEnumerable<string> userIds)
        {
            ErmisAsserts.AssertNotNull(userIds, nameof(userIds));

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelPromoteMembersAsync(Type, Id,
                new PromoteMemberRequestInternalDTO
                {
                    PromoteMembers = userIds.ToList()

                });
            Cache.TryCreateOrUpdate(response);
        }

        public async Task DemoteMembersAsync(IEnumerable<string> userIds)
        {
            ErmisAsserts.AssertNotNull(userIds, nameof(userIds));

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelDemoteMembersAsync(Type, Id,
                new DemoteMemberRequestInternalDTO
                {
                    DemoteMembers = userIds.ToList()

                });
            Cache.TryCreateOrUpdate(response);
        }

        public async Task UpdateMemberCapabilitiesAsync(UpdateMemberCapabilities capabilities)
        {
            ErmisAsserts.AssertNotNull(capabilities, nameof(capabilities));

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelMemberCapabilitiesAsync(Type, Id, capabilities.TrySaveToDto());
            Cache.TryCreateOrUpdate(response);
        }

        public async Task UpdateChannelCapabilitiesAsync(UpdateChannelCapabilitiesRequest capabilities)
        {
            ErmisAsserts.AssertNotNull(capabilities, nameof(capabilities));

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelCapabilitiesAsync(Type, Id, capabilities.TrySaveToDto());
            Cache.TryCreateOrUpdate(response);
        }

        public async Task<GetAttachmentResponse> GetAttachmentAsync()
        {
            var response = await LowLevelClient.InternalChannelApi.GetAttachmentAsync(Type, Id);
            GetAttachmentResponse getAttachmentResponse=null;
            return getAttachmentResponse.TryLoadFromDto(response);
           
        }

        public async Task BlockOrUnBlockAsync(BlockUnBlockChannelRequest blockUnblockRequest)
        {
            var response = await LowLevelClient.InternalChannelApi.BlockOrUnBlockAsync(Type, Id, blockUnblockRequest.TrySaveToDto());
            Cache.TryCreateOrUpdate(response);
        }

        public async Task BanMembersAsync(IEnumerable<string> userIds)
        {
            ErmisAsserts.AssertNotNull(userIds, nameof(userIds));

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelBanMembersAsync(Type, Id,
                new BanMemberRequestInternalDTO
                {
                    BanMembers = userIds.ToList()
                });
            Cache.TryCreateOrUpdate(response);
        }

        public async Task UnBanMembersAsync(IEnumerable<string> userIds)
        {
            ErmisAsserts.AssertNotNull(userIds, nameof(userIds));

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelUnBanMembersAsync(Type, Id,
                new UnBanMemberRequestInternalDTO
                {
                    UnBanMembers = userIds.ToList()
                });
            Cache.TryCreateOrUpdate(response);
        }

        public Task RemoveMembersAsync(params string[] userIds) => RemoveMembersAsync(userIds as IEnumerable<string>);

        public Task JoinAsMemberAsync() => AddMembersAsync(hideHistory: default, optionalMessage: default, Client.LocalUserData.User);

        public Task LeaveAsMemberChannelAsync() => RemoveMembersAsync(Client.LocalUserData.User);

        public async Task InviteMembersAsync(IEnumerable<string> userIds)
        {
            ErmisAsserts.AssertNotNull(userIds, nameof(userIds));

            AddMemberRequestInternalDTO addMembers = new AddMemberRequestInternalDTO();
            addMembers.AddMembers = new List<string>();
            foreach (var uid in userIds)
            {

                addMembers.AddMembers.Add(uid);
            }

            var response = await LowLevelClient.InternalChannelApi.UpdateChannelAddMembersAsync(Type, Id, addMembers);

            Cache.TryCreateOrUpdate(response.Channel);
            foreach (var member in response.Channel.Members)
            {
                Cache.TryCreateOrUpdate(member);
            }
        }

        public Task InviteMembersAsync(params string[] userIds) => InviteMembersAsync(userIds as IEnumerable<string>);

        public Task InviteMembersAsync(IEnumerable<IErmisUser> users)
        {
            ErmisAsserts.AssertNotNull(users, nameof(users));
            return InviteMembersAsync(users.Select(_ => _.Id));
        }

        public Task InviteMembersAsync(params IErmisUser[] users)
            => InviteMembersAsync(users as IEnumerable<IErmisUser>);

        public async Task AcceptInviteAsync()
        {
            var response = await LowLevelClient.InternalChannelApi.InviteAcceptRejectAsync(Type, Id, "accept");

        }

        public async Task RejectInviteAsync()
        {
            var response = await LowLevelClient.InternalChannelApi.InviteAcceptRejectAsync(Type, Id, "reject");
        }

        //ErmisTodo: write test
        public async Task MuteChannelAsync(int? milliseconds = default)
        {
            var response = await LowLevelClient.InternalChannelApi.MuteChannelAsync(new MuteChannelRequestInternalDTO
            {
                ChannelCids = new List<string>
                {
                    Cid
                },
                Expiration = milliseconds,
            });
            Client.UpdateLocalUser(response.OwnUser);
            //ErmisTodo: handle channel mute and mutes from response
        }

        public Task UnmuteChannelAsync()
            => LowLevelClient.InternalChannelApi.UnmuteChannelAsync(new UnmuteChannelRequestInternalDTO
            {
                ChannelCids = new List<string>
                {
                    Cid
                },
            });

        public async Task TruncateAsync(DateTimeOffset? truncatedAt = default, string systemMessage = "",
            bool skipPushNotifications = false, bool isHardDelete = false)
        {
            var response = await LowLevelClient.InternalChannelApi.TruncateChannelAsync(Type, Id);

            Cache.TryCreateOrUpdate(response.Channel);
        }

        //ErmisTodo: write test and check Client.WatchedChannels
        public Task StopWatchingAsync()
            => LowLevelClient.InternalChannelApi.StopWatchingChannelAsync(Type, Id,
                new ChannelStopWatchingRequestInternalDTO());

        public async Task FreezeAsync()
        {
            var response = await LowLevelClient.InternalChannelApi.UpdateChannelPartialAsync(Type, Id,
                new UpdateChannelPartialRequestInternalDTO()
                {
                    Set = new Dictionary<string, object>
                    {
                        { "frozen", true }
                    }
                });
            Cache.TryCreateOrUpdate(response.Channel);
        }

        public async Task UnfreezeAsync()
        {
            var response = await LowLevelClient.InternalChannelApi.UpdateChannelPartialAsync(Type, Id,
                new UpdateChannelPartialRequestInternalDTO()
                {
                    Set = new Dictionary<string, object>
                    {
                        { "frozen", false }
                    }
                });
            Cache.TryCreateOrUpdate(response.Channel);
        }

        public Task DeleteAsync()
            => LowLevelClient.InternalChannelApi.DeleteChannelAsync(Type, Id, isHardDelete: false);

        //ErmisTodo: auto send TypingStopped after timeout + timeout received typing users in case they've lost connection and never sent the stop event
        public Task SendTypingStartedEventAsync()
            => LowLevelClient.InternalChannelApi.SendTypingStartEventAsync(Type, Id);

        public Task SendTypingStoppedEventAsync()
            => LowLevelClient.InternalChannelApi.SendTypingStopEventAsync(Type, Id);

        public override string ToString() => $"Channel - Id: {Id}, Name: {Name}";

        internal ErmisChannel(string uniqueId, ICacheRepository<ErmisChannel> repository,
            IStatefulModelContext context)
            : base(uniqueId, repository, context)
        {
        }

        void IUpdateableFrom<ChannelStateResponseInternalDTO, ErmisChannel>.UpdateFromDto(
            ChannelStateResponseInternalDTO dto, ICache cache)
        {
            UpdateChannelFieldsFromDto(dto.Channel, cache);

            #region ChannelState

            //Hidden = dto.Hidden.GetValueOrDefault(); Updated from Channel
            //HideMessagesBefore = dto.HideMessagesBefore; Updated from Channel

            //This is needed because Channel.Members can be null while this is filled
            _members.TryAppendUniqueTrackedObjects(dto.Channel.Members, cache.ChannelMembers);
            Membership = cache.TryCreateOrUpdate(dto.Membership);
            _messages.TryAppendUniqueTrackedObjects(dto.Messages, cache.Messages);
            _read.TryReplaceRegularObjectsFromDto(dto.Read, cache);
            WatcherCount = GetOrDefault(dto.Watchers.Count, WatcherCount);
            _watchers.TryAppendUniqueTrackedObjects(dto.Watchers, cache.Users);

            #endregion

            SortMessagesByCreatedAt();

            //ErmisTodo should every UpdateFromDto trigger Updated event?
        }

        void IUpdateableFrom<ChannelStateResponseFieldsInternalDTO, ErmisChannel>.UpdateFromDto(
            ChannelStateResponseFieldsInternalDTO dto, ICache cache)
        {
            UpdateChannelFieldsFromDto(dto.Channel, cache);

            #region ChannelState

            _members.TryReplaceTrackedObjects(dto.Channel.Members, cache.ChannelMembers);
            Membership = cache.TryCreateOrUpdate(dto.Membership);
            _messages.TryAppendUniqueTrackedObjects(dto.Messages, cache.Messages);
            _read.TryReplaceRegularObjectsFromDto(dto.Read, cache);
            WatcherCount = GetOrDefault(dto.Watchers.Count, WatcherCount);
            _watchers.TryAppendUniqueTrackedObjects(dto.Watchers, cache.Users);

            SortMessagesByCreatedAt();

            #endregion
        }

        void IUpdateableFrom<ChannelResponseInternalDTO, ErmisChannel>.UpdateFromDto(ChannelResponseInternalDTO dto,
            ICache cache)
            => UpdateChannelFieldsFromDto(dto, cache);

        void IUpdateableFrom<UpdateChannelResponseInternalDTO, ErmisChannel>.UpdateFromDto(
            UpdateChannelResponseInternalDTO dto, ICache cache)
        {
            UpdateChannelFieldsFromDto(dto.Channel, cache);

            #region ChannelState

            _members.TryAppendUniqueTrackedObjects(dto.Channel.Members, cache.ChannelMembers);

            #endregion
        }

        internal void HandleMessageNewEvent(MessageNewEventInternalDTO dto)
        {
            AssertCid(dto.Cid);
            InternalAppendOrUpdateMessage(dto.Message);

            //ErmisTodo: how can user react to this change? WatcherCount could internally fire WatchCountChanged event
            WatcherCount = GetOrDefault(dto.WatcherCount, WatcherCount);
        }

        internal void InternalHandleMessageNewNotification(NotificationNewMessageEventInternalDTO dto)
        {
            AssertCid(dto.Cid);
            InternalAppendOrUpdateMessage(dto.Message);

            MemberCount = dto.ChannelMemberCount;
        }

        internal void HandleMessageUpdatedEvent(MessageUpdatedEventInternalDTO dto)
        {
            AssertCid(dto.Cid);
            if (!Cache.Messages.TryGet(dto.Message.Id, out var message))
            {
                return;
            }

            message.TryUpdateFromDto<MessageInternalDTO, ErmisMessage>(dto.Message, Cache);
            MessageUpdated?.Invoke(this, message);
        }

        internal void HandleMessageDeletedEvent(MessageDeletedEventInternalDTO dto)
        {
            AssertCid(dto.Cid);
            if (!Cache.Messages.TryGet(dto.Message.Id, out var message))
            {
                return;
            }

            Cache.TryCreateOrUpdate(dto.Message);

            //ErmisTodo: consider moving this logic into ErmisMessage.HandleMessageDeletedEvent
            var isHardDelete = dto.HardDelete;
            if (isHardDelete)
            {
                Cache.Messages.Remove(message);
                _messages.Remove(message);
            }
            else
            {
                message.InternalHandleSoftDelete();
            }

            MessageDeleted?.Invoke(this, message, isHardDelete);
        }

        internal void HandleChannelUpdatedEvent(ChannelUpdatedEventInternalDTO eventDto)
        {
            // Skip normal update. Channel Update is an overwrite operation. If something was not present in the request it was removed
            // Cache.TryCreateOrUpdate(eventDto.Channel);

            UpdateChannelFieldsFromDtoOverwrite(eventDto.Channel, Cache);
            MemberCount = eventDto.ChannelMemberCount;
            Updated?.Invoke(this);
        }

        internal void HandleChannelTruncatedEvent(ChannelTruncatedEventInternalDTO eventDto)
        {
            AssertCid(eventDto.Cid);
            //InternalTruncateMessages(eventDto.Channel.TruncatedAt, eventDto.Message);
            MemberCount = eventDto.ChannelMemberCount;
        }

        internal void HandleChannelTruncatedEvent(NotificationChannelTruncatedEventInternalDTO eventDto)
        {
            AssertCid(eventDto.Cid);
            //InternalTruncateMessages(eventDto.Channel.TruncatedAt);
            MemberCount = eventDto.ChannelMemberCount;
        }

        internal void InternalAddMember(ErmisChannelMember member)
        {
            if (_members.ContainsNoAlloc(member))
            {
                return;
            }

            _members.Add(member);
            MemberAdded?.Invoke(this, member);
            MembersChanged?.Invoke(this, member, OperationType.Added);
        }

        internal void InternalRemoveMember(ErmisChannelMember member)
        {
            if (!_members.ContainsNoAlloc(member))
            {
                return;
            }

            _members.Remove(member);
            MemberRemoved?.Invoke(this, member);
            MembersChanged?.Invoke(this, member, OperationType.Removed);
        }

        internal void InternalUpdateMember(ErmisChannelMember member)
        {
            if (!_members.ContainsNoAlloc(member))
            {
                _members.Add(member);
            }

            MemberUpdated?.Invoke(this, member);
            MembersChanged?.Invoke(this, member, OperationType.Updated);
        }

        protected override ErmisChannel Self => this;

        protected override string InternalUniqueId
        {
            get => Cid;
            set => Cid = value;
        }

        private readonly List<ErmisChannelMember> _members = new List<ErmisChannelMember>();
        private readonly List<ErmisMessage> _messages = new List<ErmisMessage>();
        private readonly List<ErmisMessage> _pinnedMessages = new List<ErmisMessage>();
        private readonly List<ErmisUser> _watchers = new List<ErmisUser>();
        private readonly List<ErmisRead> _read = new List<ErmisRead>();
        private readonly List<string> _ownCapabilities = new List<string>();
        private readonly List<ErmisPendingMessage> _pendingMessages = new List<ErmisPendingMessage>();

        private bool _muted;
        private bool _hidden;

        private void AssertCid(string cid)
        {
            if (cid != Cid)
            {
                throw new InvalidOperationException($"Cid mismatch, received: `{cid}` but current channel is: {Cid}");
            }
        }

        private ErmisMessage InternalAppendOrUpdateMessage(MessageResponseInternalDTO dto)
        {
            var ermisMessage = Cache.TryCreateOrUpdate(dto, out var wasCreated);
            if (wasCreated)
            {
                if (!_messages.ContainsNoAlloc(ermisMessage))
                {
                    var lastMessage = _messages.LastOrDefault();

                    _messages.Add(ermisMessage);

                    // If local user sends message during the sync operation.
                    // It is possible that the locally sent message will be added before the /sync endpoint returns past message events
                    if (lastMessage != null && ermisMessage.CreatedAt < lastMessage.CreatedAt)
                    {
                        //ErmisTodo: test this more. A good way was to toggle Ethernet on PC and send messages on Android
                        _messages.Sort(_messageCreateAtComparer);
                    }

                    MessageReceived?.Invoke(this, ermisMessage);
                }
            }

            return ermisMessage;
        }

        //ErmisTodo: move this to the right place
        private MessageCreateAtComparer _messageCreateAtComparer = new MessageCreateAtComparer();

        //ErmisTodo: move outside and change to internal
        private class MessageCreateAtComparer : IComparer<IErmisMessage>
        {
            public int Compare(IErmisMessage x, IErmisMessage y)
            {
                return x.CreatedAt.CompareTo(y.CreatedAt);
            }
        }

        //ErmisTodo: This deleteBeforeCreatedAt date is the date of event, it does not equal the passed TruncatedAt
        //Therefore the only way to detect partial truncate in the past would be to query the history
        private void InternalTruncateMessages(DateTimeOffset? deleteBeforeCreatedAt = null,
            MessageResponseInternalDTO systemMessageDto = null)
        {
            if (deleteBeforeCreatedAt.HasValue)
            {
                for (int i = _messages.Count - 1; i >= 0; i--)
                {
                    var msg = _messages[i];
                    if (msg.CreatedAt < deleteBeforeCreatedAt)
                    {
                        _messages.RemoveAt(i);
                        Cache.Messages.Remove(msg);
                    }
                }
            }
            else
            {
                for (int i = _messages.Count - 1; i >= 0; i--)
                {
                    _messages.RemoveAt(i);
                    Cache.Messages.Remove(_messages[i]);
                }
            }

            if (systemMessageDto != null)
            {
                InternalAppendOrUpdateMessage(systemMessageDto);
            }

            Truncated?.Invoke(this);
        }

        private void UpdateChannelFieldsFromDto(ChannelResponseInternalDTO dto, ICache cache)
        {
            #region Channel

            //ErmisTodo: we need to tell if something is purposely null or not available in json, check the nullable ref types or wrap reference type in custom nullable type
            Cid = GetOrDefault(dto.Cid, Cid);
            CreatedAt = GetOrDefault(dto.CreatedAt, CreatedAt);
            CreatedBy = cache.TryCreateOrUpdate(dto.CreatedBy);
            Id = GetOrDefault(dto.Id, Id);
            MemberCount = GetOrDefault(dto.MemberCount, MemberCount);
            _members.TryAppendUniqueTrackedObjects(dto.Members, cache.ChannelMembers);
            Type = new ChannelType(GetOrDefault(dto.Type, Type));
            UpdatedAt = GetOrDefault(dto.UpdatedAt, UpdatedAt);
            //Not in API spec
            Name = GetOrDefault(dto.Name, Name);

            #endregion
        }

        private void UpdateChannelFieldsFromDtoOverwrite(ChannelResponseInternalDTO dto, ICache cache)
        {
            #region Channel

            Cid = GetOrDefault(dto.Cid, Cid);
            CreatedAt = GetOrDefault(dto.CreatedAt, CreatedAt);
            CreatedBy = cache.TryCreateOrUpdate(dto.CreatedBy);
            Id = GetOrDefault(dto.Id, Id);
            MemberCount = GetOrDefault(dto.MemberCount, MemberCount);
            _members.TryAppendUniqueTrackedObjects(dto.Members, cache.ChannelMembers);
            Type = new ChannelType(GetOrDefault(dto.Type, Type));
            UpdatedAt = GetOrDefault(dto.UpdatedAt, UpdatedAt);

            //Not in API spec
            Name = GetOrDefault(dto.Name, string.Empty);

            #endregion
        }

        internal void InternalHandleMessageReadEvent(MessageReadEventInternalDTO eventDto)
        {
            AssertCid(eventDto.Cid);
            HandleMessageRead(eventDto.User, eventDto.CreatedAt);
        }

        internal void InternalHandleMessageReadNotification(NotificationMarkReadEventInternalDTO eventDto)
        {
            AssertCid(eventDto.Cid);
            HandleMessageRead(eventDto.User, eventDto.CreatedAt);
            //ErmisTodo: update eventDto.Channel as well?
        }

        internal void InternalHandleUserWatchingStartEvent(UserWatchingStartEventInternalDTO eventDto)
        {
            AssertCid(eventDto.Cid);

            var user = Cache.TryCreateOrUpdate(eventDto.User, out var wasCreated);
            if (wasCreated || !_watchers.ContainsNoAlloc(user))
            {
                WatcherCount += 1;
                _watchers.Add(user);
                WatcherAdded?.Invoke(this, user);
            }
        }

        internal void InternalHandleUserWatchingStop(UserWatchingStopEventInternalDTO eventDto)
        {
            AssertCid(eventDto.Cid);

            //We always reduce because watchers are paginated so our partial _watchers state may not contain the removed one but count reflects all
            WatcherCount -= 1;

            for (int i = _watchers.Count - 1; i >= 0; i--)
            {
                if (_watchers[i].Id == eventDto.User.Id)
                {
                    var user = Cache.TryCreateOrUpdate(eventDto.User, out var wasCreated);
                    _watchers.RemoveAt(i);
                    WatcherRemoved?.Invoke(this, user);
                    return;
                }
            }
        }

        internal void InternalHandleTypingStopped(TypingStopEventInternalDTO eventDto)
        {
            AssertCid(eventDto.Cid);
            var user = Cache.TryCreateOrUpdate(eventDto.User);
            ErmisAsserts.AssertNotNull(user, nameof(user));

            for (int i = _typingUsers.Count - 1; i >= 0; i--)
            {
                if (_typingUsers[i].Id == eventDto.User.Id)
                {
                    _typingUsers.RemoveAt(i);
                    UserStoppedTyping?.Invoke(this, user);
                    TypingUsersChanged?.Invoke(this);
                    return;
                }
            }
        }

        internal void InternalHandleTypingStarted(TypingStartEventInternalDTO eventDto)
        {
            AssertCid(eventDto.Cid);
            var user = Cache.TryCreateOrUpdate(eventDto.User);
            ErmisAsserts.AssertNotNull(user, nameof(user));

            if (!_typingUsers.ContainsNoAlloc(user))
            {
                _typingUsers.Add(user);
                UserStartedTyping?.Invoke(this, user);
                TypingUsersChanged?.Invoke(this);
            }
        }

        internal void InternalNotifyReactionReceived(ErmisMessage message, ErmisReaction reaction)
            => ReactionAdded?.Invoke(this, message, reaction);

        internal void InternalNotifyReactionUpdated(ErmisMessage message, ErmisReaction reaction)
            => ReactionUpdated?.Invoke(this, message, reaction);

        public void InternalNotifyReactionDeleted(ErmisMessage message, ErmisReaction reaction)
            => ReactionRemoved?.Invoke(this, message, reaction);

        //ErmisTodo: implement some timeout for typing users in case we dont' receive, this could be configurable
        private readonly List<IErmisUser> _typingUsers = new List<IErmisUser>();

        private void HandleMessageRead(UserIdObjectInternalDTO userDto, DateTimeOffset createAt)
        {
            //we can only mark messages based on created_at
            //we mark this per user

            var userRead = _read.FirstOrDefault(_ => _.User.Id == userDto.Id);
            if (userRead == null)
            {
                return; //ErmisTodo: do we add this user? We don't have his UnreadMessages count
            }

            userRead.Update(createAt);
            //ErmisTodo: IMPLEMENT we need to recalculate the unread counts and raise some event
        }

        private Task InternalBanUserAsync(IErmisUser user, bool isShadowBan = false, string reason = "",
            int? timeoutMinutes = default, bool isIpBan = false)
        {
            ErmisAsserts.AssertNotNull(user, nameof(user));

            if (timeoutMinutes.HasValue)
            {
                ErmisAsserts.AssertGreaterThanZero(timeoutMinutes, nameof(timeoutMinutes));
            }

            return LowLevelClient.InternalModerationApi.BanUserAsync(new BanRequestInternalDTO
            {
                ChannelCid = Cid,
                IpBan = isIpBan,
                Reason = reason,
                Shadow = isShadowBan,
                TargetUserId = user.Id,
                Timeout = timeoutMinutes,
            });
        }

        private void SortMessagesByCreatedAt()
        {
            _messages.Sort((msg1, msg2) => msg1.CreatedAt.CompareTo(msg2.CreatedAt));
        }

        private UpdateChannelRequestInternalDTO GetUpdateRequestWithCurrentData()
            => new UpdateChannelRequestInternalDTO
            {
                Data = new ChannelRequestInternalDTO
                {
                    Name = Name,
                    Description = Description,
                    MemberMessageCooldown = MemberMessageCooldown,
                    Public = Public,
                    FilterWords = FilterWords,
                    image = image
                },
            };
    }
}