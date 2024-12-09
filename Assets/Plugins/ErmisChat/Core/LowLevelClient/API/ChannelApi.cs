using System;
using System.Threading.Tasks;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.API.Internal;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;
using Ermis.Core.Responses;

namespace Ermis.Core.LowLevelClient.API
{
    internal class ChannelApi : IChannelApi
    {
        private readonly IInternalChannelApi _internalChannelApi;
        internal ChannelApi(IInternalChannelApi internalChannelApi)
        {
            _internalChannelApi = internalChannelApi ?? throw new ArgumentNullException(nameof(internalChannelApi));
        }
        #region Old
        public async Task<ChannelsResponse> QueryChannelsAsync(QueryChannelsRequest queryChannelsRequest)
        {
            var dto = await _internalChannelApi.QueryChannelsAsync(queryChannelsRequest.TrySaveToDto());
            return dto.ToDomain<ChannelStateResponseInternalDTO, ChannelsResponse>();
        }

        public async Task<ChannelState> GetOrCreateChannelAsync(string channelType,
           ChannelGetOrCreateRequest getOrCreateRequest)
        {
            var dto = await _internalChannelApi.GetOrCreateChannelAsync(channelType, getOrCreateRequest.TrySaveToDto());
            return dto.ToDomain<ChannelStateResponseFieldsInternalDTO, ChannelState>();
        }

        public async Task<ChannelState> GetOrCreateChannelAsync(string channelType, string channelId,
            ChannelGetOrCreateRequest getOrCreateRequest)
        {
            var dto = await _internalChannelApi.GetOrCreateChannelAsync(channelType, channelId,
                getOrCreateRequest.TrySaveToDto());
            return dto.ToDomain<ChannelStateResponseFieldsInternalDTO, ChannelState>();
        }

        public async Task<UpdateChannelPartialResponse> UpdateChannelPartialAsync(string channelType, string channelId,
            UpdateChannelPartialRequest updateChannelPartialRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelPartialAsync(channelType, channelId,
                updateChannelPartialRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelPartialResponseInternalDTO, UpdateChannelPartialResponse>();
        }

        public async Task<DeleteChannelsResponse> DeleteChannelsAsync(DeleteChannelsRequest deleteChannelsRequest)
        {
            var dto = await _internalChannelApi.DeleteChannelsAsync(deleteChannelsRequest.TrySaveToDto());
            return dto.ToDomain<DeleteChannelsResponseInternalDTO, DeleteChannelsResponse>();
        }

        public async Task<DeleteChannelResponse> DeleteChannelAsync(string channelType, string channelId, bool isHardDelete)
        {
            var dto = await _internalChannelApi.DeleteChannelAsync(channelType, channelId, isHardDelete);
            return dto.ToDomain<DeleteChannelResponseInternalDTO, DeleteChannelResponse>();
        }





        public async Task<MuteChannelResponse> MuteChannelAsync(MuteChannelRequest muteChannelRequest)
        {
            var dto = await _internalChannelApi.MuteChannelAsync(muteChannelRequest.TrySaveToDto());
            return dto.ToDomain<MuteChannelResponseInternalDTO, MuteChannelResponse>();
        }

        public async Task<UnmuteResponse> UnmuteChannelAsync(UnmuteChannelRequest unmuteChannelRequest)
        {
            var dto = await _internalChannelApi.UnmuteChannelAsync(unmuteChannelRequest.TrySaveToDto());
            return dto.ToDomain<UnmuteResponseInternalDTO, UnmuteResponse>();
        }

        public async Task<ShowChannelResponse> ShowChannelAsync(string channelType, string channelId,
            ShowChannelRequest showChannelRequest)
        {
            var dto = await _internalChannelApi.ShowChannelAsync(channelType, channelId,
                showChannelRequest.TrySaveToDto());
            return dto.ToDomain<ShowChannelResponseInternalDTO, ShowChannelResponse>();
        }

        public async Task<HideChannelResponse> HideChannelAsync(string channelType, string channelId,
            HideChannelRequest hideChannelRequest)
        {
            var dto = await _internalChannelApi.HideChannelAsync(channelType, channelId,
                hideChannelRequest.TrySaveToDto());
            return dto.ToDomain<HideChannelResponseInternalDTO, HideChannelResponse>();
        }

        public async Task<MembersResponse> QueryMembersAsync(QueryMembersRequest queryMembersRequest)
        {
            var dto = await _internalChannelApi.QueryMembersAsync(queryMembersRequest.TrySaveToDto());
            return dto.ToDomain<MembersResponseInternalDTO, MembersResponse>();
        }

        public async Task<StopWatchingResponse> StopWatchingChannelAsync(string channelType, string channelId,
            ChannelStopWatchingRequest channelStopWatchingRequest)
        {
            var dto = await _internalChannelApi.StopWatchingChannelAsync(channelType, channelId,
                channelStopWatchingRequest.TrySaveToDto());
            return dto.ToDomain<StopWatchingResponseInternalDTO, StopWatchingResponse>();
        }


        public async Task<MarkReadResponse> MarkManyReadAsync(MarkChannelsReadRequest markChannelsReadRequest)
        {
            var dto = await _internalChannelApi.MarkManyReadAsync(markChannelsReadRequest.TrySaveToDto());
            return dto.ToDomain<MarkReadResponseInternalDTO, MarkReadResponse>();
        }



        public async Task<SyncResponse> SyncAsync(SyncRequest syncRequest)
        {
            var dto = await _internalChannelApi.SyncAsync(syncRequest.TrySaveToDto());
            return dto.ToDomain<SyncResponseInternalDTO, SyncResponse>();
        }

        public async Task<CurrentUnreadCounts> GetUnreadCountsAsync()
        {
            var dto = await _internalChannelApi.GetUnreadCountsAsync();
            return dto.ToDomain<WrappedUnreadCountsResponseInternalDTO, CurrentUnreadCounts>();
        }
        #endregion

        #region New

       public async Task<SimpleResponse> Signal(SignalRequest signalRequest)
        {
            var dto = await _internalChannelApi.Signal(signalRequest.TrySaveToDto());
            return dto.ToDomain<SimpleResponseInternalDTO, SimpleResponse>();
        }
        public async Task<GetChannelsResponse> QueryChannelsAsync(GetChannelsRequest getChannelsRequest)
        {
            var dto = await _internalChannelApi.QueryChannelsAsync(getChannelsRequest.TrySaveToDto());
            return dto.ToDomain<GetChannelResponseInternalDTO, GetChannelsResponse>();
        }

        public async Task<SearchPublicChannelResponse> SearchPublicChannelAsync(SearchPublicChannelRequest searchChannelsRequest)
        {
            var dto = await _internalChannelApi.SearchPublicChannelAsync(searchChannelsRequest.TrySaveToDto());
            return dto.ToDomain<SearchPublicChannelResponseInternalDTO, SearchPublicChannelResponse>();
        }

        public async Task<ChannelStateResponseFields> CreateChannelAsync(string channelType, string channelId,
            CreateChannelRequest createRequest)
        {
            var dto = await _internalChannelApi.CreateChannelAsync(channelType, channelId, createRequest.TrySaveToDto());
            return dto.ToDomain<ChannelStateResponseFieldsInternalDTO, ChannelStateResponseFields>();

        }

        public async Task<ChannelStateResponseFields> CreateChannelAsync(string channelType,
            CreateChannelRequest createRequest)
        {
            var dto = await _internalChannelApi.CreateChannelAsync(channelType, createRequest.TrySaveToDto());
            return dto.ToDomain<ChannelStateResponseFieldsInternalDTO, ChannelStateResponseFields>();

        }

        public async Task<SimpleResponse> InviteAcceptRejectAsync(string channelType, string channelId, string action)
        {
            var dto = await _internalChannelApi.InviteAcceptRejectAsync(channelType, channelId, action);
            return dto.ToDomain<SimpleResponseInternalDTO, SimpleResponse>();
        }

        public async Task<UpdateChannelResponse> UpdateChannelAddMembersAsync(string channelType, string channelId,
           AddMemberRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelAddMembersAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> UpdateChannelRemoveMembersAsync(string channelType, string channelId,
            RemoveMemberRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelRemoveMembersAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> UpdateChannelBanMembersAsync(string channelType, string channelId,
            BanMemberRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelBanMembersAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> UpdateChannelUnBanMembersAsync(string channelType, string channelId,
            UpdateChannelUnBanMemberRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelUnBanMembersAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }
        public async Task<UpdateChannelResponse> UpdateChannelPromoteMembersAsync(string channelType, string channelId,
           PromoteMemberRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelPromoteMembersAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> UpdateChannelDemoteMembersAsync(string channelType, string channelId,
            DemoteMemberRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelDemoteMembersAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> UpdateChannelPromoteOwnerAndLeaveAsync(string channelType, string channelId,
           PromoteOwnerAndLeaveRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelPromoteOwnerAndLeaveAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> UpdateChannelMemberCapabilitiesAsync(string channelType, string channelId,
           UpdateMemberCapabilities updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelMemberCapabilitiesAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> UpdateChannelCapabilitiesAsync(string channelType, string channelId,
           UpdateChannelCapabilitiesRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelCapabilitiesAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> BlockOrUnBlockAsync(string channelType, string channelId,
          BlockUnBlockChannelRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.BlockOrUnBlockAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<SimpleResponse> ChannelMuteAsync(string channelType, string channelId,
          ChannelMuteRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.ChannelMuteAsync(channelType, channelId,
               updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<SimpleResponseInternalDTO, SimpleResponse>();
        }

        public async Task<UpdateChannelResponse> UpdateChannelAsync(string channelType, string channelId,
           UpdateChannelRequest updateChannelRequest)
        {
            var dto = await _internalChannelApi.UpdateChannelAsync(channelType, channelId,
                updateChannelRequest.TrySaveToDto());
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> DeleteChannelAsync(string channelType, string channelId)
        {
            var dto = await _internalChannelApi.DeleteChannelAsync(channelType, channelId);
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<UpdateChannelResponse> TruncateChannelAsync(string channelType, string channelId)
        {
            var dto = await _internalChannelApi.TruncateChannelAsync(channelType, channelId);
            return dto.ToDomain<UpdateChannelResponseInternalDTO, UpdateChannelResponse>();
        }

        public async Task<SendEventResponse> SendTypingStartEventAsync(string channelType, string channelId)
        {
            var dto = await _internalChannelApi.SendTypingStartEventAsync(channelType, channelId);
            return dto.ToDomain<SendEventResponseInternalDTO, SendEventResponse>();
        }

        public async Task<SendEventResponse> SendTypingStopEventAsync(string channelType, string channelId)
        {
            var dto = await _internalChannelApi.SendTypingStopEventAsync(channelType, channelId);
            return dto.ToDomain<SendEventResponseInternalDTO, SendEventResponse>();
        }

        public async Task<MarkReadResponse> MarkReadAsync(string channelType, string channelId)
        {
            var dto = await _internalChannelApi.MarkReadAsync(channelType, channelId);
            return dto.ToDomain<MarkReadResponseInternalDTO, MarkReadResponse>();
        }

        public async Task<GetAttachmentResponse> GetAttachmentAsync(string channelType, string channelId)
        {
            var dto = await _internalChannelApi.GetAttachmentAsync(channelType, channelId);
            return dto.ToDomain<GetAttachmentResponseInternalDTO, GetAttachmentResponse>();
        }

        #endregion
    }
}