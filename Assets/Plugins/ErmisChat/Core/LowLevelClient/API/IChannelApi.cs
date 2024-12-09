using System;
using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.API
{
    /// <summary>
    /// A client that can be used to retrieve, create and alter channels of a Ermis Chat application.
    /// </summary>
    public interface IChannelApi
    {
        #region Old
        /// <summary>
        /// <para>Queries channels.</para>
        /// You can query channels based on built-in fields as well as any custom field you add to channels.
        /// Multiple filters can be combined using AND, OR logical operators, each filter can use its
        /// comparison (equality, inequality, greater than, greater or equal, etc.).
        /// You can find the complete list of supported operators in the query syntax section of the docs.
        /// </summary>
        Task<ChannelsResponse> QueryChannelsAsync(QueryChannelsRequest queryChannelsRequest);

        /// <summary>
        /// Create or return a channel with a given type for a list of members.
        /// This endpoint requires providing a list of members for which a single channel is maintained.
        /// Please refer to link below for more information
        /// </summary>
        Task<ChannelState> GetOrCreateChannelAsync(string channelType, ChannelGetOrCreateRequest getOrCreateRequest);

        /// <summary>
        /// Create or return a channel with a given type and id
        /// </summary>
        Task<ChannelState> GetOrCreateChannelAsync(string channelType, string channelId,
            ChannelGetOrCreateRequest getOrCreateRequest);


        /// <summary>
        /// Can be used to set and unset specific fields when it is necessary to retain additional custom data fields on the object.
        /// </summary>
        Task<UpdateChannelPartialResponse> UpdateChannelPartialAsync(string channelType, string channelId,
            UpdateChannelPartialRequest updateChannelPartialRequest);

        /// <summary>
        /// <para>Deletes multiple channels.</para>
        /// This is an asynchronous operation and the returned value is a task Id.
        /// </summary>
        Task<DeleteChannelsResponse> DeleteChannelsAsync(DeleteChannelsRequest deleteChannelsRequest);

        //ErmisTodo: deprecate isHardDelete since it's no longer available in client-side SDK
        /// <summary>
        /// Deletes a channel.
        /// </summary>
        Task<DeleteChannelResponse> DeleteChannelAsync(string channelType, string channelId, bool isHardDelete);


        

        /// <summary>
        /// <para>Mutes a channel.</para>
        /// Messages added to a muted channel will not trigger push notifications, nor change the
        /// unread count for the users that muted it. By default, mutes stay in place indefinitely
        /// until the user removes it; however, you can optionally set an expiration time. The list
        /// of muted channels and their expiration time is returned when the user connects.
        /// </summary>
        Task<MuteChannelResponse> MuteChannelAsync(MuteChannelRequest muteChannelRequest);

        /// <summary>
        /// <para>Unmutes a channel.</para>
        /// </summary>
        Task<UnmuteResponse> UnmuteChannelAsync(UnmuteChannelRequest unmuteChannelRequest);

        /// <summary>
        /// <para>Shows a previously hidden channel.</para>
        /// Use <see cref="HideChannelAsync"/> to hide a channel.
        /// </summary>
        Task<ShowChannelResponse> ShowChannelAsync(string channelType, string channelId,
            ShowChannelRequest showChannelRequest);

        /// <summary>
        /// <para>Removes a channel from query channel requests for that user until a new message is added.</para>
        /// Use <see cref="ShowChannelAsync"/> to cancel this operation.
        /// </summary>
        Task<HideChannelResponse> HideChannelAsync(string channelType, string channelId,
            HideChannelRequest hideChannelRequest);

        /// <summary>
        /// <para>Queries members of a channel.</para>
        /// The queryMembers endpoint allows you to list and paginate members for a channel. The
        /// endpoint supports filtering on numerous criteria to efficiently return member information.
        /// This endpoint is useful for channels that have large lists of members and
        /// you want to search members or if you want to display the full list of members for a channel.
        /// </summary>
        Task<MembersResponse> QueryMembersAsync(QueryMembersRequest queryMembersRequest);

        /// <summary>
        /// Stop receiving channel events
        /// </summary>
        Task<StopWatchingResponse> StopWatchingChannelAsync(string channelType, string channelId,
            ChannelStopWatchingRequest channelStopWatchingRequest);

        

        /// <summary>
        /// Mark multiple channels as read. Pass a map of CID to a message ID that is considered last read by client.
        /// If message ID is empty, the whole channel will be considered as read
        /// </summary>
        Task<MarkReadResponse> MarkManyReadAsync(MarkChannelsReadRequest markChannelsReadRequest);

       

        //ErmisTodo: perhaps we can skip this declaration and use the Internal one directly
        Task<SyncResponse> SyncAsync(SyncRequest syncRequest);

        Task<CurrentUnreadCounts> GetUnreadCountsAsync();

        #endregion

        #region New

        Task<SimpleResponse> Signal(SignalRequest signalRequest);
        Task<GetChannelsResponse> QueryChannelsAsync(GetChannelsRequest queryChannelsRequest);

        Task<SearchPublicChannelResponse> SearchPublicChannelAsync(SearchPublicChannelRequest searchChannelsRequest);

        Task<ChannelStateResponseFields> CreateChannelAsync(string channelType, string channelId,
            CreateChannelRequest getOrCreateRequest);

        Task<ChannelStateResponseFields> CreateChannelAsync(string channelType,
            CreateChannelRequest getOrCreateRequest);

        Task<SimpleResponse> InviteAcceptRejectAsync(string channelType, string channelId, string action);

        Task<UpdateChannelResponse> UpdateChannelAddMembersAsync(string channelType, string channelId,
           AddMemberRequest updateChannelRequest);

        Task<UpdateChannelResponse> UpdateChannelRemoveMembersAsync(string channelType, string channelId,
            RemoveMemberRequest updateChannelRequest);

        Task<UpdateChannelResponse> UpdateChannelBanMembersAsync(string channelType, string channelId,
            BanMemberRequest updateChannelRequest);

        Task<UpdateChannelResponse> UpdateChannelUnBanMembersAsync(string channelType, string channelId,
            UpdateChannelUnBanMemberRequest updateChannelRequest);

        Task<UpdateChannelResponse> UpdateChannelPromoteMembersAsync(string channelType, string channelId,
           PromoteMemberRequest updateChannelRequest);

        Task<UpdateChannelResponse> UpdateChannelDemoteMembersAsync(string channelType, string channelId,
            DemoteMemberRequest updateChannelRequest);

        Task<UpdateChannelResponse> UpdateChannelPromoteOwnerAndLeaveAsync(string channelType, string channelId,
            PromoteOwnerAndLeaveRequest updateChannelRequest);

        Task<UpdateChannelResponse> UpdateChannelMemberCapabilitiesAsync(string channelType, string channelId,
            UpdateMemberCapabilities updateChannelRequest);

        Task<UpdateChannelResponse> UpdateChannelCapabilitiesAsync(string channelType, string channelId,
            UpdateChannelCapabilitiesRequest updateChannelRequest);

        Task<UpdateChannelResponse> BlockOrUnBlockAsync(string channelType, string channelId,
           BlockUnBlockChannelRequest updateChannelRequest);
        Task<SimpleResponse> ChannelMuteAsync(string channelType, string channelId,
           ChannelMuteRequest updateChannelRequest);

        Task<UpdateChannelResponse> UpdateChannelAsync(string channelType, string channelId,
            UpdateChannelRequest updateChannelRequest);

        Task<UpdateChannelResponse> DeleteChannelAsync(string channelType, string channelId);

        Task<UpdateChannelResponse> TruncateChannelAsync(string channelType, string channelId);

        Task<SendEventResponse> SendTypingStartEventAsync(string channelType, string channelId);

        Task<SendEventResponse> SendTypingStopEventAsync(string channelType, string channelId);

        
        Task<MarkReadResponse> MarkReadAsync(string channelType, string channelId);

        Task<GetAttachmentResponse> GetAttachmentAsync(string channelType, string channelId);
        #endregion  
    }
}