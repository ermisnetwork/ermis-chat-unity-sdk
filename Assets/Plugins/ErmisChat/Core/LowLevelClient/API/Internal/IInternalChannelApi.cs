using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal interface IInternalChannelApi
    {
        #region Old
        Task<ChannelStateResponseFieldsInternalDTO> GetOrCreateChannelAsync(string channelType, string channelId,
            ChannelGetOrCreateRequestInternalDTO getOrCreateRequest);
        Task<ShowChannelResponseInternalDTO> ShowChannelAsync(string channelType, string channelId,
            ShowChannelRequestInternalDTO showChannelRequest);

        Task<ChannelStateResponseInternalDTO> QueryChannelsAsync(QueryChannelsRequestInternalDTO queryChannelsRequest);



        Task<ChannelStateResponseFieldsInternalDTO> GetOrCreateChannelAsync(string channelType,
            ChannelGetOrCreateRequestInternalDTO getOrCreateRequest);

        Task<UpdateChannelPartialResponseInternalDTO> UpdateChannelPartialAsync(string channelType, string channelId,
            UpdateChannelPartialRequestInternalDTO updateChannelPartialRequest);

        Task<DeleteChannelsResponseInternalDTO> DeleteChannelsAsync(DeleteChannelsRequestInternalDTO deleteChannelsRequest);

        Task<DeleteChannelResponseInternalDTO> DeleteChannelAsync(string channelType, string channelId, bool isHardDelete);
        Task<MuteChannelResponseInternalDTO> MuteChannelAsync(MuteChannelRequestInternalDTO muteChannelRequest);

        Task<UnmuteResponseInternalDTO> UnmuteChannelAsync(UnmuteChannelRequestInternalDTO unmuteChannelRequest);

        Task<HideChannelResponseInternalDTO> HideChannelAsync(string channelType, string channelId,
            HideChannelRequestInternalDTO hideChannelRequest);

        Task<MembersResponseInternalDTO> QueryMembersAsync(QueryMembersRequestInternalDTO queryMembersRequest);

        Task<StopWatchingResponseInternalDTO> StopWatchingChannelAsync(string channelType, string channelId,
            ChannelStopWatchingRequestInternalDTO channelStopWatchingRequest);






        Task<SyncResponseInternalDTO> SyncAsync(SyncRequestInternalDTO syncRequest);

        Task<WrappedUnreadCountsResponseInternalDTO> GetUnreadCountsAsync();
        #endregion

        #region New

        Task<SimpleResponseInternalDTO> Signal(SignalRequestInternalDTO queryChannelsRequest);

        Task<GetChannelResponseInternalDTO> QueryChannelsAsync(GetChannelsRequestDto queryChannelsRequest);

        Task<SearchPublicChannelResponseInternalDTO> SearchPublicChannelAsync(SearchPublicChannelRequestInternalDTO searchChannelsRequest);
        Task<ChannelStateResponseFieldsInternalDTO> CreateChannelAsync(string channelType, string channelId,
            CreateChannelRequestInternalDto getOrCreateRequest);

        Task<ChannelStateResponseFieldsInternalDTO> CreateChannelAsync(string channelType,
            CreateChannelRequestInternalDto getOrCreateRequest);

        Task<SimpleResponseInternalDTO> InviteAcceptRejectAsync(string channelType, string channelId, string action);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelAddMembersAsync(string channelType, string channelId,
            AddMemberRequestInternalDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelRemoveMembersAsync(string channelType, string channelId,
            RemoveMemberRequestInternalDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelBanMembersAsync(string channelType, string channelId,
            BanMemberRequestInternalDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelUnBanMembersAsync(string channelType, string channelId,
            UnBanMemberRequestInternalDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelPromoteMembersAsync(string channelType, string channelId,
            PromoteMemberRequestInternalDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelDemoteMembersAsync(string channelType, string channelId,
            DemoteMemberRequestInternalDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelPromoteOwnerAndLeaveAsync(string channelType, string channelId,
            PromoteOwnerAndLeaveRequestInternalDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelMemberCapabilitiesAsync(string channelType, string channelId,
            UpdateMemberCapabilitiesRequestDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelCapabilitiesAsync(string channelType, string channelId,
            UpdateChannelCapabilitiesRequestDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> BlockOrUnBlockAsync(string channelType, string channelId,
            BlockUnBlockChannelRequestInternalDTO updateChannelRequest);

        Task<SimpleResponseInternalDTO> ChannelMuteAsync(string channelType, string channelId,
           ChannelMuteRequestInternalDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> UpdateChannelAsync(string channelType, string channelId,
            UpdateChannelRequestInternalDTO updateChannelRequest);

        Task<UpdateChannelResponseInternalDTO> TruncateChannelAsync(string channelType, string channelId);

        Task<UpdateChannelResponseInternalDTO> DeleteChannelAsync(string channelType, string channelId);

        Task<SendEventResponseInternalDTO> SendTypingStartEventAsync(string channelType, string channelId);

        Task<SendEventResponseInternalDTO> SendTypingStopEventAsync(string channelType, string channelId);


        Task<MarkReadResponseInternalDTO> MarkManyReadAsync(MarkChannelsReadRequestInternalDTO markChannelsReadRequest);

        Task<MarkReadResponseInternalDTO> MarkReadAsync(string channelType, string channelId);

        Task<GetAttachmentResponseInternalDTO> GetAttachmentAsync(string channelType, string channelId);
        #endregion

    }
}