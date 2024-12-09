using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.Models;
using Ermis.Core.Requests;
using Ermis.Core.Responses;
using Ermis.Core.LowLevelClient.Responses;
namespace Ermis.Core.StatefulModels
{
    /// <summary>
    /// Channel is where group of <see cref="IErmisChannelMember"/>s can send messages.
    /// Default permissions and configuration is based on <see cref="ErmisChannel.Type"/>
    /// </summary>
    public interface IErmisChannel : IErmisStatefulModel
    {
        /// <summary>
        /// Event fired when a new <see cref="IErmisMessage"/> was received in this channel
        /// </summary>
        event ErmisChannelMessageHandler MessageReceived;

        /// <summary>
        /// Event fired when a <see cref="IErmisMessage"/> was updated in this channel
        /// </summary>
        event ErmisChannelMessageHandler MessageUpdated;

        /// <summary>
        /// Event fired when a <see cref="IErmisMessage"/> was deleted from this channel
        /// </summary>
        event ErmisMessageDeleteHandler MessageDeleted;

        /// <summary>
        /// Event fired when a new <see cref="ErmisReaction"/> was added to <see cref="IErmisMessage"/>
        /// </summary>
        event ErmisMessageReactionHandler ReactionAdded;

        /// <summary>
        /// Event fired when a <see cref="ErmisReaction"/> was removed from a <see cref="IErmisMessage"/>
        /// </summary>
        event ErmisMessageReactionHandler ReactionRemoved;

        /// <summary>
        /// Event fired when a <see cref="ErmisReaction"/> was updated on a <see cref="IErmisMessage"/>
        /// </summary>
        event ErmisMessageReactionHandler ReactionUpdated;

        /// <summary>
        /// Event fired when a new <see cref="IErmisChannelMember"/> joined this channel
        /// </summary>
        event ErmisChannelMemberChangeHandler MemberAdded;

        /// <summary>
        /// Event fired when a <see cref="IErmisChannelMember"/> left this channel
        /// </summary>
        event ErmisChannelMemberChangeHandler MemberRemoved;

        /// <summary>
        /// Event fired when a <see cref="IErmisChannelMember"/> was updated
        /// </summary>
        event ErmisChannelMemberChangeHandler MemberUpdated;

        /// <summary>
        /// Event fired when a <see cref="IErmisChannelMember"/> was added, updated, or removed.
        /// </summary>
        event ErmisChannelMemberAnyChangeHandler MembersChanged; //ErmisTodo: typo, this should be MemberChanged

        /// <summary>
        /// Event fired when visibility of this channel changed. Check <see cref="IErmisChannel.Hidden"/> to know if channel is hidden
        /// </summary>
        
        event ErmisChannelVisibilityHandler VisibilityChanged;

        /// <summary>
        /// Event fired when channel got muted on unmuted. Check <see cref="IErmisChannel.Muted"/> and <see cref="IErmisChannel.MuteExpiresAt"/>
        /// </summary>
        event ErmisChannelMuteHandler MuteChanged;

        /// <summary>
        /// Event fired when this channel was truncated meaning that all or some of the messages were removed
        /// </summary>
        event ErmisChannelChangeHandler Truncated;

        /// <summary>
        /// Event fired when this channel data was updated
        /// </summary>
        event ErmisChannelChangeHandler Updated;

        /// <summary>
        /// Event fired when a <see cref="IErmisUser"/> started watching this channel
        /// See also <see cref="WatcherCount"/> and <see cref="Watchers"/>
        /// </summary>
        event ErmisChannelUserChangeHandler WatcherAdded;

        /// <summary>
        /// Event fired when a <see cref="IErmisUser"/> stopped watching this channel
        /// See also <see cref="WatcherCount"/> and <see cref="Watchers"/>
        /// </summary>
        event ErmisChannelUserChangeHandler WatcherRemoved;

        /// <summary>
        /// Event fired when a <see cref="IErmisUser"/> in this channel starts typing
        /// </summary>
        event ErmisChannelUserChangeHandler UserStartedTyping;

        /// <summary>
        /// Event fired when a <see cref="IErmisUser"/> in this channel stops typing
        /// </summary>
        event ErmisChannelUserChangeHandler UserStoppedTyping;

        /// <summary>
        /// Event fired when a <see cref="TypingUsers"/> the list of typing users has changed.
        /// If you want to exactly know when a users started or stopped typing subscribe to <see cref="UserStartedTyping"/> and <see cref="UserStoppedTyping"/>
        /// </summary>
        event ErmisChannelChangeHandler TypingUsersChanged;

        /// <summary>
        /// Whether auto translation is enabled or not
        /// </summary>
        bool AutoTranslationEnabled { get; }

        /// <summary>
        /// Language to translate to when auto translation is active
        /// </summary>
        string AutoTranslationLanguage { get; }

        /// <summary>
        /// Channel CID (type:id)
        /// </summary>
        string Cid { get; }

        /// <summary>
        /// Channel configuration
        /// </summary>
        ErmisChannelConfig Config { get; }

        /// <summary>
        /// Cooldown period after sending each message
        /// </summary>
        int? Cooldown { get; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Creator of the channel
        /// </summary>
        IErmisUser CreatedBy { get; }

        /// <summary>
        /// Date/time of deletion
        /// </summary>
        DateTimeOffset? DeletedAt { get; }

        bool Disabled { get; }

        /// <summary>
        /// Whether channel is frozen or not
        /// </summary>
        bool Frozen { get; }

        /// <summary>
        /// Whether this channel is hidden by the local user or not. Subscribe to <see cref="VisibilityChanged"/> to get notified when this property changes
        /// </summary>
        bool Hidden { get; }

        /// <summary>
        /// Date since when the message history is accessible
        /// </summary>
        DateTimeOffset? HideMessagesBefore { get; }

        /// <summary>
        /// Channel unique ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Date of the last message sent
        /// </summary>
        DateTimeOffset? LastMessageAt { get; }

        /// <summary>
        /// Number of members in the channel
        /// </summary>
        int MemberCount { get; }

        /// <summary>
        /// List of channel members (max 100)
        /// </summary>
        IReadOnlyList<IErmisChannelMember> Members { get; }

        /// <summary>
        /// Date of mute expiration
        /// </summary>
        DateTimeOffset? MuteExpiresAt { get; }

        /// <summary>
        /// Whether this channel is muted or not. Subscribe to <see cref="MuteChanged"/> to get notified when this property changes
        /// </summary>
        bool Muted { get; }

        /// <summary>
        /// List of channel capabilities of authenticated user
        /// </summary>
        IReadOnlyList<string> OwnCapabilities { get; }

        /// <summary>
        /// Team the channel belongs to (multi-tenant only)
        /// </summary>
        string Team { get; }

        DateTimeOffset? TruncatedAt { get; }
        IErmisUser TruncatedBy { get; }

        /// <summary>
        /// Type of the channel
        /// </summary>
        ChannelType Type { get; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        DateTimeOffset? UpdatedAt { get; }

        string Name { get; }

        /// <summary>
        /// Local user membership object
        /// </summary>
        IErmisChannelMember Membership { get; }

        /// <summary>
        /// List of channel messages. By default only latest messages are loaded. If you wish to load older messages use the <see cref="LoadOlderMessagesAsync"/>
        /// </summary>
        IReadOnlyList<IErmisMessage> Messages { get; }

        /// <summary>
        /// Pending messages that this user has sent
        /// </summary>
        IReadOnlyList<ErmisPendingMessage> PendingMessages { get; }

        /// <summary>
        /// List of pinned messages in the channel
        /// </summary>
        IReadOnlyList<IErmisMessage> PinnedMessages { get; }

        /// <summary>
        /// List of read states
        /// </summary>
        IReadOnlyList<ErmisRead> Read { get; } //ErmisTodo: perhaps rename to ReadStatus or ReadState

        /// <summary>
        /// Number of channel watchers
        /// </summary>
        int WatcherCount { get; }

        /// <summary>
        /// Paginated list of user who are watching this channel
        /// Subscribe to <see cref="WatcherAdded"/> and <see cref="WatcherRemoved"/> events to know when this list changes.
        /// </summary>
        IReadOnlyList<IErmisUser> Watchers { get; }

        /// <summary>
        /// List of currently typing users
        /// Subscribe to <see cref="TypingUsersChanged"/> or <see cref="UserStartedTyping"/> and <see cref="UserStoppedTyping"/> events to know when this list changes.
        /// </summary>
        IReadOnlyList<IErmisUser> TypingUsers { get; }

        /// <summary>
        /// Is this a direct message channel between the local and some other user
        /// </summary>
        bool IsDirectMessage { get; }

        /// <summary>
        /// Basic send message method. If you want to set additional parameters use the <see cref="SendNewMessageAsync(ErmisSendMessageRequest)"/> overload
        /// </summary>
        Task<IErmisMessage> SendNewMessageAsync(string message);

        /// <summary>
        /// Advanced send message method. Check out the <see cref="ErmisSendMessageRequest"/> to see all of the possible parameters
        /// </summary>
        Task<IErmisMessage> SendNewMessageAsync(ErmisSendMessageRequest sendMessageRequest);

        /// <summary>
        /// Load next portion of older messages. Older messages will be prepended to the <see cref="Messages"/> list.
        /// Note that loading older messages does NOT trigger the <see cref="MessageReceived"/> event
        /// </summary>
        Task LoadOlderMessagesAsync();

        /// <summary>
        /// Update channel in a complete overwrite mode.
        /// Important! Any data that is present on the channel and not included in a full update will be deleted.
        /// If you want to update only some fields of the channel use the <see cref="IErmisChannel.UpdatePartialAsync"/>
        /// </summary>
        /// <param name="updateOverwriteRequest"></param>
        Task UpdateOverwriteAsync(ErmisUpdateOverwriteChannelRequest updateOverwriteRequest);

        /// <summary>
        /// Update channel in a partial mode. You can selectively set and unset fields of the channel
        /// If you want to completely overwrite the channel use the <see cref="IErmisChannel.UpdateOverwriteAsync"/>
        /// </summary>
        /// ErmisTodo: this should be more high level, maybe use enum with predefined field names?
        Task UpdatePartialAsync(IDictionary<string, object> setFields = null,
            IEnumerable<string> unsetFields = null);

        /// <summary>
        /// Upload file to the Ermis CDN. Returned file URL can be used as a message attachment.
        /// For image files use <see cref="IErmisChannel.UploadImageAsync"/> as it will generate the thumbnail and allow for image resize and crop operations.
        /// If you wish to delete this file, user <see cref="DeleteFileOrImageAsync"/>
        /// </summary>
        /// <param name="fileContent">File bytes content (e.g. returned from <see cref="System.IO.File.ReadAllBytes"/></param>
        /// <param name="fileName">Name of the file</param>
       
        Task<ErmisFileUploadResponse> UploadFileAsync(byte[] fileContent, string fileName);

        /// <summary>
        /// Delete file of any type that was send to the Ermis CDN.
        /// This handles both files sent via <see cref="IErmisChannel.UploadFileAsync"/> and images sent via <see cref="IErmisChannel.UploadImageAsync"/>
        /// </summary>
        Task DeleteFileOrImageAsync(string fileUrl);

        /// <summary>
        /// Upload image file to the Ermis CDN. The returned image URL can be injected into <see cref="ErmisAttachmentRequest"/> when sending new message.
        /// For regular files use <see cref="IErmisChannel.UploadFileAsync"/>
        /// If you wish to delete this file, user <see cref="DeleteFileOrImageAsync"/>
        /// </summary>
        /// <param name="imageContent"></param>
        /// <param name="imageName"></param>
        Task<ErmisImageUploadResponse> UploadImageAsync(byte[] imageContent, string imageName);

        /// <summary>
        /// Query channel members based on provided criteria. Results will not be
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IErmisChannelMember>> QueryMembersAsync(IDictionary<string, object> filters = null,
            int limit = 30, int offset = 0);

        [Obsolete(
            "This method was renamed to QueryMembersAsync. Please use the QueryMembersAsync. The QueryMembers will be removed in a future release.")]
        Task<IEnumerable<IErmisChannelMember>> QueryMembers(IDictionary<string, object> filters = null, int limit = 30,
            int offset = 0);

        void QueryWatchers(); //ErmisTodo: IMPLEMENT

        /// <summary>
        /// Ban user from this channel.
        /// If you wish to ban user completely from all of the channels, this can be done only by a server-side SDKs.
        /// </summary>
        /// <param name="user">User to ban from channel</param>
        /// <param name="reason">[Optional] reason description why user got banned</param>
        /// <param name="timeoutMinutes">[Optional] timeout in minutes after which ban is automatically expired</param>
        /// <param name="isIpBan">[Optional] Should ban apply to user's IP address</param>
        Task BanUserAsync(IErmisUser user, string reason = "",
            int? timeoutMinutes = default, bool isIpBan = false);

        /// <summary>
        /// Ban member from this channel.
        /// If you wish to ban user completely from all of the channels, this can be done only by a server-side SDKs.
        /// </summary>
        /// <param name="member">Channel member to ban from channel</param>
        /// <param name="reason">[Optional] reason description why user got banned</param>
        /// <param name="timeoutMinutes">[Optional] timeout in minutes after which ban is automatically expired</param>
        /// <param name="isIpBan">[Optional] Should ban apply to user's IP address</param>
        Task BanMemberAsync(IErmisChannelMember member, string reason = "",
            int? timeoutMinutes = default, bool isIpBan = false);

        /// <summary>
        /// Shadow Ban user from this channel. Shadow banned user does not know about being banned.
        /// If you wish to ban user completely from all of the channels, this can be done only by a server-side SDKs.
        /// </summary>
        /// <param name="user">User to ban from channel</param>
        /// <param name="reason">[Optional] reason description why user got banned</param>
        /// <param name="timeoutMinutes">[Optional] timeout in minutes after which ban is automatically expired</param>
        /// <param name="isIpBan">[Optional] Should ban apply to user's IP address</param>
        Task ShadowBanUserAsync(IErmisUser user, string reason = "",
            int? timeoutMinutes = default, bool isIpBan = false);

        /// <summary>
        /// Shadow Ban member from this channel. Shadow banned member does not know about being banned.
        /// If you wish to ban user completely from all of the channels, this can be done only by a server-side SDKs.
        /// </summary>
        /// <param name="member">Channel member to ban from channel</param>
        /// <param name="reason">[Optional] reason description why user got banned</param>
        /// <param name="timeoutMinutes">[Optional] timeout in minutes after which ban is automatically expired</param>
        /// <param name="isIpBan">[Optional] Should ban apply to user's IP address</param>
        Task ShadowBanMemberAsync(IErmisChannelMember member, string reason = "",
            int? timeoutMinutes = default, bool isIpBan = false);

        /// <summary>
        /// Remove ban from the user on this channel
        /// </summary>
        Task UnbanUserAsync(IErmisUser user);

        /// <summary>
        /// Mark this channel completely as read
        /// If you want to mark specific message as read use the <see cref="IErmisMessage.MarkMessageAsLastReadAsync"/>
        ///
        /// This feature allows to track to which message users have read the channel
        /// </summary>
        Task MarkChannelReadAsync();

        /// <summary>
        /// <para>Shows a previously hidden channel.</para>
        /// Use <see cref="IErmisChannel.HideAsync"/> to hide a channel.
        /// </summary>
        Task ShowAsync();

        /// <summary>
        /// <para>Removes a channel from query channel requests for that user until a new message is added.</para>
        /// Use <see cref="IErmisChannel.ShowAsync"/> to cancel this operation.
        /// </summary>
        /// <param name="clearHistory">Whether to clear message history of the channel or not</param>
        Task HideAsync(bool? clearHistory = default);

        /// <summary>
        /// Add users as members to this channel
        /// </summary>
        /// <param name="users">Users to become members of this channel</param>
        /// <param name="hideHistory"></param>
        /// <param name="optionalMessage"></param>
        Task AddMembersAsync(IEnumerable<IErmisUser> users, bool? hideHistory = default,
            ErmisMessageRequest optionalMessage = default);

        /// <inheritdoc cref="AddMembersAsync(System.Collections.Generic.IEnumerable{string},System.Nullable{bool},Ermis.Core.Requests.ErmisMessageRequest)"/>
        Task AddMembersAsync(bool? hideHistory = default, ErmisMessageRequest optionalMessage = default,
            params IErmisUser[] users);

        /// <summary>
        /// Add users as members to this channel
        /// </summary>
        /// <param name="userIds">User IDs to become members of this channel</param>
        /// <param name="hideHistory">Hide history for the new members</param>
        /// <param name="optionalMessage"></param>
        Task AddMembersAsync(IEnumerable<string> userIds, bool? hideHistory = default,
            ErmisMessageRequest optionalMessage = default);

        /// <inheritdoc cref="AddMembersAsync(System.Collections.Generic.IEnumerable{string},System.Nullable{bool},Ermis.Core.Requests.ErmisMessageRequest)"/>
        Task AddMembersAsync(bool? hideHistory = default, ErmisMessageRequest optionalMessage = default,
            params string[] users);

        /// <summary>
        /// Remove members from this channel
        /// </summary>
        /// <param name="members">Members to remove</param>
        Task RemoveMembersAsync(IEnumerable<IErmisChannelMember> members);

        /// <inheritdoc cref="RemoveMembersAsync(IEnumerable{Ermis.Core.StatefulModels.IErmisChannelMember})}"/>
        Task RemoveMembersAsync(params IErmisChannelMember[] members);

        /// <summary>
        /// Remove members from this channel
        /// </summary>
        /// <param name="members">Members to remove</param>
        Task RemoveMembersAsync(IEnumerable<IErmisUser> members);

        /// <inheritdoc cref="RemoveMembersAsync(IEnumerable{Ermis.Core.StatefulModels.IErmisUser})}"/>
        Task RemoveMembersAsync(params IErmisUser[] members);


        /// <inheritdoc cref="RemoveMembersAsync(IEnumerable{Ermis.Core.StatefulModels.IErmisChannelMember})}"/>
        Task RemoveMembersAsync(params string[] userIds);

        Task PromoteMembersAsync(IEnumerable<string> userIds);

        Task DemoteMembersAsync(IEnumerable<string> userIds);

        Task UpdateMemberCapabilitiesAsync(UpdateMemberCapabilities capabilities);

        Task UpdateChannelCapabilitiesAsync(UpdateChannelCapabilitiesRequest capabilities);

        Task<GetAttachmentResponse> GetAttachmentAsync();

        Task BlockOrUnBlockAsync(BlockUnBlockChannelRequest blockUnblockRequest);

        /// <summary>
        /// Mute channel with optional duration in milliseconds
        /// </summary>
        /// <param name="milliseconds">[Optional] Duration in milliseconds</param>
        Task MuteChannelAsync(int? milliseconds = default); // ErmisTodo: change to seconds, milliseconds is pointless

        /// <summary>
        /// Unmute channel
        /// </summary>
        Task UnmuteChannelAsync();

        /// <summary>
        /// Truncate removes all of the messages but does not affect the channel data or channel members. If you want to delete both messages and channel data then use the <see cref="IErmisChannel.DeleteAsync"/> method instead.
        /// </summary>
        /// <param name="truncatedAt">[Optional]truncate channel up to given time. If not set then all messages are truncated</param>
        /// <param name="systemMessage">A system message to be added via truncation.</param>
        /// <param name="skipPushNotifications">Don't send a push notification for <param name="systemMessage"/>.</param>
        /// <param name="isHardDelete">if truncation should delete messages instead of hiding</param>
        Task TruncateAsync(DateTimeOffset? truncatedAt = default, string systemMessage = "",
            bool skipPushNotifications = false, bool isHardDelete = false);

        /// <summary>
        /// Stop watching this channel meaning you will no longer receive any updates and it will be removed from <see cref="IErmisChatClient.WatchedChannels"/>
        /// </summary>
        Task StopWatchingAsync();

        /// <summary>
        /// Freezing a channel will disallow sending new messages and sending / deleting reactions
        /// </summary>
        Task FreezeAsync();
        
        /// <summary>
        /// Unfreeze this channel
        /// </summary>
        Task UnfreezeAsync();

        /// <summary>
        /// Delete this channel. By default channel is soft deleted. You can hard delete it only by using a server-side SDK due to security
        /// </summary>
        Task DeleteAsync();

        /// <summary>
        /// Send a notification that the local user started typing in this channel. You can access currently typing users via <see cref="TypingUsers"/>
        /// </summary>
        Task SendTypingStartedEventAsync();

        /// <summary>
        /// Send a notification that the local user stopped typing in this channel. You can access currently typing users via <see cref="TypingUsers"/>
        /// </summary>
        Task SendTypingStoppedEventAsync();

        /// <summary>
        /// Joins this channel as a a member (<see cref="IErmisChannelMember"/>). Only possible if local user has the `Join Own Channel` permission
        /// </summary>
        Task JoinAsMemberAsync();

        /// <summary>
        /// Stop being a member (<see cref="IErmisChannelMember"/>) of this channel. Only possible if local user has the `Leave Own Channel` permission
        /// </summary>
        Task LeaveAsMemberChannelAsync();

        /// <summary>
        /// Invite new members to this channel.
        /// </summary>
        Task InviteMembersAsync(IEnumerable<string> userIds);

        /// <summary>
        /// remove members to this channel.
        /// </summary>
        Task RemoveMembersAsync(IEnumerable<string> userIds);

        /// <summary>
        /// Ban members to this channel.
        /// </summary>
        Task BanMembersAsync(IEnumerable<string> userIds);

        /// <summary>
        /// Unban members to this channel.
        /// </summary>
        Task UnBanMembersAsync(IEnumerable<string> userIds);

        /// <summary>
        /// Invite new members to this channel.
        /// </summary>
        Task InviteMembersAsync(params string[] userIds);

        /// <summary>
        /// Invite new members to this channel.
        /// </summary>
        Task InviteMembersAsync(IEnumerable<IErmisUser> users);

        /// <summary>
        /// Invite new members to this channel.
        /// </summary>
        Task InviteMembersAsync(params IErmisUser[] users);

        /// <summary>
        /// Accept an invite to this channel
        /// </summary>
        Task AcceptInviteAsync();

        /// <summary>
        /// Reject an invite to this channel
        /// </summary>
        Task RejectInviteAsync();
    }
}