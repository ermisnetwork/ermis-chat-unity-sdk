using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;
using Ermis.Core.QueryBuilders.Filters;
using Ermis.Core.QueryBuilders.Sort;
using Ermis.Core.Requests;
using Ermis.Core.Responses;
using Ermis.Core.StatefulModels;
using Ermis.Libs.Auth;
using Ermis.Libs.ChatInstanceRunner;

namespace Ermis.Core
{
    /// <summary>
    /// The official Ermis Chat API Client. This client is stateful meaning that the state of:
    /// - <see cref="IErmisChannel"/> 
    /// - <see cref="IErmisChannelMember"/> 
    /// - <see cref="IErmisUser"/> 
    /// - <see cref="IErmisLocalUserData"/>
    /// is automatically updated by the client and they always represent the most updated state.
    /// </summary>
    public interface IErmisChatClient : IDisposable, IErmisChatClientEventsListener
    {
        /// <summary>
        /// Event fired when connection with Ermis Chat server is established
        /// </summary>
        event ConnectionMadeHandler Connected;

        /// <summary>
        /// Event fired when connection with Ermis Chat server is lost
        /// </summary>
        event Action Disconnected;

        /// <summary>
        /// Event fired when connection state with Ermis Chat server has changed
        /// </summary>
        event ConnectionChangeHandler ConnectionStateChanged;

        /// <summary>
        /// Channel was deleted
        /// </summary>
        event ChannelDeleteHandler ChannelDeleted;
        
        /// <summary>
        /// Invite to a <see cref="IErmisChannel"/> was received
        /// </summary>
        event ChannelInviteHandler ChannelInviteReceived;
        
        /// <summary>
        /// Invite to a <see cref="IErmisChannel"/> was accepted
        /// </summary>
        event ChannelInviteHandler ChannelInviteAccepted;
        
        /// <summary>
        /// Invite to a <see cref="IErmisChannel"/> was rejected
        /// </summary>
        event ChannelInviteHandler ChannelInviteRejected;
        
        /// <summary>
        /// Local user was added to a channel as a member. This event fires only for channels that are not tracked locally.
        /// Use this event to get notified when the local user is added to a new channel. For tracked channels, use the <see cref="IErmisChannel.MemberAdded"/> event.
        /// </summary>
        event ChannelMemberAddedHandler AddedToChannelAsMember;
        
        /// <summary>
        /// Local user was removed from a channel as a member. This event fires only for channels that are not tracked locally.
        /// Use this event to get notified when the local user was removed from a channel. For tracked channels use <see cref="IErmisChannel.MemberRemoved"/> event
        /// </summary>
        event ChannelMemberRemovedHandler RemovedFromChannelAsMember;

        /// <summary>
        /// Current connection state
        /// </summary>
        ConnectionState ConnectionState { get; }

        /// <summary>
        /// Is client connected. Subscribe to <see cref="Connected"/> event to get notified when connection is established
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// If true it means that client initiated connection and is waiting for the Ermis server to confirm the connection. Subscribe to <see cref="Connected"/> to get notified when connection is established
        /// </summary>
        bool IsConnecting { get; }

        /// <summary>
        /// Data of the user that is connected to the Ermis Chat using the local device. This property is set after the client connection is established.
        /// YSubscribe to <see cref="Connected"/> to know when the connection is established.
        /// Use <see cref="IErmisLocalUserData.User"/> to access the local <see cref="IErmisUser"/> object
        /// </summary>
        IErmisLocalUserData LocalUserData { get; }

        /// <summary>
        /// Watched channels receive updates on all users activity like new messages, reactions, etc.
        /// Use <see cref="CreateChannelWithIdAsync"/> and <see cref="QueryChannelsAsync"/> to watch channels
        /// </summary>
        IReadOnlyList<IErmisChannel> WatchedChannels { get; }

        /// <summary>
        /// Next time since startup the client will attempt to reconnect to the Ermis Server. 
        /// </summary>
        double? NextReconnectTime { get; }

        /// <summary>
        /// Low level client. Use it if you want to bypass the stateful client and execute low level requests directly.
        /// </summary>
        IErmisChatLowLevelClient LowLevelClient { get; }

        /// <summary>
        /// Connect user to Ermis Chat server.
        /// User authentication credentials:
        /// ApiKey - Your application API KEY. You can get it from dashboard
        /// UserId - Create it in Ermis Dashboard or with server-side SDK or with <see cref="UpsertUsers"/>
        /// </summary>
        /// <param name="userAuthCredentials">User authentication credentials</param>
        /// <param name="cancellationToken">Cancellation token that will abort the login request when cancelled</param>
        Task<IErmisLocalUserData> ConnectUserAsync(AuthCredentials userAuthCredentials,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Connect user to Ermis Chat server.
        /// </summary>
        /// <param name="apiKey">Your application API KEY. You can get it from dashboard</param>
        /// <param name="userId">ID of a user that will be connected to the Ermis Chat</param>
        /// <param name="userAuthToken">User authentication token.</param>
        /// <param name="cancellationToken">Cancellation token that will abort the login request when cancelled</param>
        Task<IErmisLocalUserData> ConnectUserAsync(string apiKey, string userId, string userAuthToken,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey">Your application API KEY. You can get it from dashboard</param>
        /// <param name="userId">ID of a user that will be connected to the Ermis Chat</param>
        /// <param name="tokenProvider">Service that will provide authorization token for a given user id. Use <see cref="TokenProvider"/></param>
        /// <param name="cancellationToken">Cancellation token that will abort the login request when cancelled</param>
        /// <returns></returns>
        Task<IErmisLocalUserData> ConnectUserAsync(string apiKey, string userId, ITokenProvider tokenProvider,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Create or get a channel with a given type and id
        /// Use this to create general purpose channel for unspecified group of users
        /// If you want to create a channel for a dedicated group of users e.g. private conversation use the <see cref="CreateChannelWithIdAsync"/> overload
        /// </summary>
      
        Task<IErmisChannel> CreateChannelWithIdAsync(ChannelType channelType, string channelId,
            string name = null,
            IEnumerable<IErmisUser> members = null);

        Task<SearchPublicChannelResponse> SearchPublicChannelAsync(SearchPublicChannelRequest searchChannelsRequest);

        /// <summary>
        /// Create or get a channel for a given groups of users.
        /// Use this to create channel for direct messages. This will return the same channel per unique group of users regardless of their order.
        /// If you wish to create channels with ID for users to join use the <see cref="CreateChannelWithIdAsync"/>
        /// </summary>
        /// <param name="channelType">Type of channel determines permissions and default settings.
        ///     Use predefined ones:
        ///     <see cref="ChannelType.Messaging"/>,
        ///     <see cref="ChannelType.Livestream"/>,
        ///     <see cref="ChannelType.Team"/>,
        ///     <see cref="ChannelType.Commerce"/>,
        ///     <see cref="ChannelType.Gaming"/>,
        ///     or create a custom type in your dashboard and use <see cref="ChannelType.Custom"/></param>
        /// <param name="members">Users for which a channel will be created. If channel </param>
        /// <param name="optionalCustomData">[Optional] Custom data to attach to this channel</param>
        Task<IErmisChannel> CreateChannelWithMembersAsync(ChannelType channelType,
            IEnumerable<IErmisUser> members);

        /// <summary>
        /// Query <see cref="IErmisChannel"/> with optional: filters, sorting, and pagination
        /// </summary>
        /// <param name="filters">[Optional] Filters</param>
        /// <param name="sort">[Optional] Sort object. You can chain multiple sorting fields</param>
        /// <param name="limit">[Optional] How many records to return. Think about it as "records per page"</param>
        /// <param name="offset">[Optional] How many records to skip. Example: if Limit is 30, the offset for 2nd page is 30, for 3rd page is 60, etc.</param>
        [Obsolete("This method will be removed in the future. Please use the other overload method that uses " +
                  nameof(IFieldFilterRule) + " type filters")]
        Task<IEnumerable<IErmisChannel>> QueryChannelsAsync(IDictionary<string, object> filters,
            ChannelSortObject sort = null, int limit = 30, int offset = 0);


        Task<IEnumerable<IErmisChannel>> QueryChannelsAsync(GetChannelsRequest request = null);

        /// <summary>
        /// Query <see cref="IErmisUser"/>
        /// </summary>
        /// <param name="filters">[Optional] filter object</param>
        /// <returns></returns>
        [Obsolete("This method will be removed in the future. Please use the other overload method that uses " +
                  nameof(IFieldFilterRule) + " type filters")]
        Task<IEnumerable<IErmisUser>> QueryUsersAsync(IDictionary<string, object> filters = null);
        
        /// <summary>
        /// Query <see cref="IErmisUser"/>
        /// </summary>
        /// <param name="filters">[Optional] filter rules</param>
        /// <returns></returns>
        Task<IEnumerable<IErmisUser>> QueryUsersAsync(IEnumerable<IFieldFilterRule> filters = null, UsersSortObject sort = null, int offset = 0, int limit = 30);

        Task<IEnumerable<IErmisUser>> QueryUsersListAsync();

        /// <summary>
        /// Query banned users based on provided parameters
        /// </summary>
        /// <param name="ermisQueryBannedUsersRequest">Request parameters object</param>
        Task<IEnumerable<ErmisUserBanInfo>> QueryBannedUsersAsync(
            ErmisQueryBannedUsersRequest ermisQueryBannedUsersRequest);

        /// <summary>
        /// Upsert users. Upsert means update this user or create if not found
        /// </summary>
        /// <param name="userRequests">Collection of user upsert requests</param>
        Task<IEnumerable<IErmisUser>> UpsertUsers(IEnumerable<ErmisUserUpsertRequest> userRequests);

        /// <summary>
        /// Mute channels with optional duration in milliseconds
        /// </summary>
        /// <param name="channels">Channels to mute</param>
        /// <param name="milliseconds">[Optional] Duration in milliseconds</param>
        Task MuteMultipleChannelsAsync(IEnumerable<IErmisChannel> channels, int? milliseconds = default);

        Task UnmuteMultipleChannelsAsync(IEnumerable<IErmisChannel> channels);

        /// <summary>
        /// Delete multiple channels. This in an asynchronous server operation meaning it may still be executing when this method Task is completed.
        /// Response contains <see cref="ErmisDeleteChannelsResponse.TaskId"/> which can be used to check the status of the delete operation
        /// </summary>
        /// <param name="channels">Collection of <see cref="IErmisChannel"/> to delete</param>
        /// <param name="isHardDelete">Hard delete removes channels entirely whereas Soft Delete deletes them from client but still allows to access them from the server-side SDK</param>
        Task<ErmisDeleteChannelsResponse> DeleteMultipleChannelsAsync(IEnumerable<IErmisChannel> channels,
            bool isHardDelete = false);

        /// <summary>
        /// You mute single user by using <see cref="IErmisUser.MuteAsync"/>
        /// </summary>
        /// <param name="users">Users to mute</param>
        /// <param name="timeoutMinutes">Optional timeout. Without timeout users will stay muted indefinitely</param>
        Task MuteMultipleUsersAsync(IEnumerable<IErmisUser> users, int? timeoutMinutes = default);

        Task DisconnectUserAsync();

        bool IsLocalUser(IErmisUser messageUser);

        /// <summary>
        /// Get current state of unread counts for the user. Unread counts mean how many messages and threads are unread in the channels and threads the user is participating in.
        ///
        /// This method can be used in offline mode as well to poll the latest unread counts without establishing a connection.
        /// To use it this way, you need to call the <see cref="SetAuthorizationCredentials"/> method first to set the authorization credentials for the API call.
        /// </summary>
        /// <returns><see cref="ErmisCurrentUnreadCounts"/>Contains information about unread counts in channels and threads</returns>
        Task<ErmisCurrentUnreadCounts> GetLatestUnreadCountsAsync();
        
        //ErmisTodo: create unit test that tests GetLatestUnreadCountsAsync in offline mode.

        /// <summary>
        /// Set authorization credentials for the client to use when connecting to the API
        /// </summary>
        /// <param name="authCredentials">Credentials containing: api key, user ID, and a user Token</param>
        void SetAuthorizationCredentials(AuthCredentials authCredentials);
    }
}