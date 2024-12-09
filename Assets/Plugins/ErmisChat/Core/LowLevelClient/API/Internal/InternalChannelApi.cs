using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.Web;
using Ermis.Libs.Http;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal class InternalChannelApi : InternalApiClientBase, IInternalChannelApi
    {
        internal InternalChannelApi(IHttpClient httpClient, ISerializer serializer, ILogs logs,
            IRequestUriFactory requestUriFactory, IErmisChatLowLevelClient lowLevelClient)
            : base(httpClient, serializer, logs, requestUriFactory, lowLevelClient)
        {
        }
        #region Old
        public Task<ChannelStateResponseInternalDTO> QueryChannelsAsync(QueryChannelsRequestInternalDTO queryChannelsRequest)
        {
            var endpoint = ChannelEndpoints.QueryChannels();
            return PostWithCustomHeader<QueryChannelsRequestInternalDTO, ChannelStateResponseInternalDTO>(endpoint, queryChannelsRequest);
        }

        public Task<ChannelStateResponseFieldsInternalDTO> GetOrCreateChannelAsync(string channelType,
            ChannelGetOrCreateRequestInternalDTO getOrCreateRequest)
        {
            var endpoint = ChannelEndpoints.GetOrCreate(channelType);
            return PostWithCustomHeader<ChannelGetOrCreateRequestInternalDTO, ChannelStateResponseFieldsInternalDTO>(endpoint, getOrCreateRequest);
        }

        public Task<ChannelStateResponseFieldsInternalDTO> GetOrCreateChannelAsync(string channelType, string channelId,
            ChannelGetOrCreateRequestInternalDTO getOrCreateRequest)
        {
            var endpoint = ChannelEndpoints.GetOrCreate(channelType, channelId);
            return PostWithCustomHeader<ChannelGetOrCreateRequestInternalDTO, ChannelStateResponseFieldsInternalDTO>(endpoint, getOrCreateRequest);
        }

        public Task<UpdateChannelPartialResponseInternalDTO> UpdateChannelPartialAsync(string channelType, string channelId,
          UpdateChannelPartialRequestInternalDTO updateChannelPartialRequest)
        {
            var endpoint = ChannelEndpoints.UpdatePartial(channelType, channelId);
            return Patch<UpdateChannelPartialRequestInternalDTO, UpdateChannelPartialResponseInternalDTO>(endpoint,
                updateChannelPartialRequest);
        }

        public Task<DeleteChannelsResponseInternalDTO> DeleteChannelsAsync(DeleteChannelsRequestInternalDTO deleteChannelsRequest)
        {
            var endpoint = ChannelEndpoints.DeleteChannels();
            return PostWithCustomHeader<DeleteChannelsRequestInternalDTO, DeleteChannelsResponseInternalDTO>(endpoint, deleteChannelsRequest);
        }

        public Task<DeleteChannelResponseInternalDTO> DeleteChannelAsync(string channelType, string channelId, bool isHardDelete = false)
        {
            var endpoint = ChannelEndpoints.DeleteChannel(channelType, channelId);
            var parameters = QueryParameters.Default.Append("hard_delete", isHardDelete);
            return Delete<DeleteChannelResponseInternalDTO>(endpoint, parameters);
        }



        public Task<MuteChannelResponseInternalDTO> MuteChannelAsync(MuteChannelRequestInternalDTO muteChannelRequest)
        {
            var endpoint = ChannelEndpoints.MuteChannel();
            return PostWithCustomHeader<MuteChannelRequestInternalDTO, MuteChannelResponseInternalDTO>(endpoint, muteChannelRequest);
        }

        public Task<UnmuteResponseInternalDTO> UnmuteChannelAsync(UnmuteChannelRequestInternalDTO unmuteChannelRequest)
        {
            var endpoint = ChannelEndpoints.UnmuteChannel();
            return PostWithCustomHeader<UnmuteChannelRequestInternalDTO, UnmuteResponseInternalDTO>(endpoint, unmuteChannelRequest);
        }

        public Task<ShowChannelResponseInternalDTO> ShowChannelAsync(string channelType, string channelId,
            ShowChannelRequestInternalDTO showChannelRequest)
        {
            var endpoint = ChannelEndpoints.ShowChannel(channelType, channelId);
            return PostWithCustomHeader<ShowChannelRequestInternalDTO, ShowChannelResponseInternalDTO>(endpoint, showChannelRequest);
        }

        public Task<HideChannelResponseInternalDTO> HideChannelAsync(string channelType, string channelId,
            HideChannelRequestInternalDTO hideChannelRequest)
        {
            var endpoint = ChannelEndpoints.HideChannel(channelType, channelId);
            return PostWithCustomHeader<HideChannelRequestInternalDTO, HideChannelResponseInternalDTO>(endpoint, hideChannelRequest);
        }

        public Task<MembersResponseInternalDTO> QueryMembersAsync(QueryMembersRequestInternalDTO queryMembersRequest)
            => GetWithCustomHeader<QueryMembersRequestInternalDTO, MembersResponseInternalDTO>("/members", queryMembersRequest);

        public Task<StopWatchingResponseInternalDTO> StopWatchingChannelAsync(string channelType, string channelId,
            ChannelStopWatchingRequestInternalDTO channelStopWatchingRequest)
            => PostWithCustomHeader<ChannelStopWatchingRequestInternalDTO, StopWatchingResponseInternalDTO>(
                $"/channels/{channelType}/{channelId}/stop-watching", channelStopWatchingRequest);

        public Task<MarkReadResponseInternalDTO> MarkReadAsync(string channelType, string channelId)
            => PostWithCustomHeader<MarkReadResponseInternalDTO>($"/channels/{channelType}/{channelId}/read",
                null);

        public Task<MarkReadResponseInternalDTO> MarkManyReadAsync(MarkChannelsReadRequestInternalDTO markChannelsReadRequest)
            => PostWithCustomHeader<MarkChannelsReadRequestInternalDTO, MarkReadResponseInternalDTO>($"/channels/read", markChannelsReadRequest);



        public Task<SyncResponseInternalDTO> SyncAsync(SyncRequestInternalDTO syncRequest)
            => PostWithCustomHeader<SyncRequestInternalDTO, SyncResponseInternalDTO>($"/sync", syncRequest);

        public Task<WrappedUnreadCountsResponseInternalDTO> GetUnreadCountsAsync()
            => GetWithCustomHeader<WrappedUnreadCountsResponseInternalDTO>("/unread");

        #endregion

        #region New

        public Task<SimpleResponseInternalDTO> Signal(SignalRequestInternalDTO signalRequest)
        {
            var endpoint = ChannelEndpoints.Signal();
            return PostWithCustomHeader<SignalRequestInternalDTO, SimpleResponseInternalDTO>(endpoint, signalRequest);
        }

        public Task<GetChannelResponseInternalDTO> QueryChannelsAsync(GetChannelsRequestDto queryChannelsRequest)
        {
            var endpoint = ChannelEndpoints.QueryChannels();
            return PostWithCustomHeader<GetChannelsRequestDto, GetChannelResponseInternalDTO>(endpoint, queryChannelsRequest);
        }
        public Task<SearchPublicChannelResponseInternalDTO> SearchPublicChannelAsync(SearchPublicChannelRequestInternalDTO searchChannelsRequest)
        {
            var endpoint = ChannelEndpoints.SearchPublicChannelName();
            return PostWithCustomHeader<SearchPublicChannelRequestInternalDTO, SearchPublicChannelResponseInternalDTO>(endpoint, searchChannelsRequest);
        }
        public Task<ChannelStateResponseFieldsInternalDTO> CreateChannelAsync(string channelType, string channelId,
            CreateChannelRequestInternalDto getOrCreateRequest)
        {
            var endpoint = ChannelEndpoints.GetOrCreate(channelType, channelId);
            return PostWithCustomHeader<CreateChannelRequestInternalDto, ChannelStateResponseFieldsInternalDTO>(endpoint, getOrCreateRequest);
        }

        public Task<ChannelStateResponseFieldsInternalDTO> CreateChannelAsync(string channelType,
           CreateChannelRequestInternalDto getOrCreateRequest)
        {
            var endpoint = ChannelEndpoints.GetOrCreate(channelType);
            return PostWithCustomHeader<CreateChannelRequestInternalDto, ChannelStateResponseFieldsInternalDTO>(endpoint, getOrCreateRequest);
        }

        public Task<SimpleResponseInternalDTO> InviteAcceptRejectAsync(string channelType, string channelId, string action)
        {
            var endpoint = ChannelEndpoints.Invite(channelType, channelId, action);
            return PostWithCustomHeader<SimpleResponseInternalDTO>(endpoint, null);
        }

        public Task<UpdateChannelResponseInternalDTO> UpdateChannelAddMembersAsync(string channelType, string channelId,
            AddMemberRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<AddMemberRequestInternalDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> UpdateChannelRemoveMembersAsync(string channelType, string channelId,
            RemoveMemberRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<RemoveMemberRequestInternalDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> UpdateChannelBanMembersAsync(string channelType, string channelId,
            BanMemberRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<BanMemberRequestInternalDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> UpdateChannelUnBanMembersAsync(string channelType, string channelId,
            UnBanMemberRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<UnBanMemberRequestInternalDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> UpdateChannelPromoteMembersAsync(string channelType, string channelId,
            PromoteMemberRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<PromoteMemberRequestInternalDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> UpdateChannelDemoteMembersAsync(string channelType, string channelId,
            DemoteMemberRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<DemoteMemberRequestInternalDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> UpdateChannelPromoteOwnerAndLeaveAsync(string channelType, string channelId,
           PromoteOwnerAndLeaveRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<PromoteOwnerAndLeaveRequestInternalDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> UpdateChannelMemberCapabilitiesAsync(string channelType, string channelId,
           UpdateMemberCapabilitiesRequestDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<UpdateMemberCapabilitiesRequestDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> UpdateChannelCapabilitiesAsync(string channelType, string channelId,
            UpdateChannelCapabilitiesRequestDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<UpdateChannelCapabilitiesRequestDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> BlockOrUnBlockAsync(string channelType, string channelId,
           BlockUnBlockChannelRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<BlockUnBlockChannelRequestInternalDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<SimpleResponseInternalDTO> ChannelMuteAsync(string channelType, string channelId,
           ChannelMuteRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.ChannelMute(channelType, channelId);
            return PostWithCustomHeader<ChannelMuteRequestInternalDTO, SimpleResponseInternalDTO>(endpoint, updateChannelRequest);
        }
        public Task<UpdateChannelResponseInternalDTO> UpdateChannelAsync(string channelType, string channelId,
            UpdateChannelRequestInternalDTO updateChannelRequest)
        {
            var endpoint = ChannelEndpoints.Update(channelType, channelId);
            return PostWithCustomHeader<UpdateChannelRequestInternalDTO, UpdateChannelResponseInternalDTO>(endpoint, updateChannelRequest);
        }

        public Task<UpdateChannelResponseInternalDTO> DeleteChannelAsync(string channelType, string channelId)
        {
            var endpoint = ChannelEndpoints.DeleteChannel(channelType, channelId);
            return DeleteWithCustomHeader<UpdateChannelResponseInternalDTO>(endpoint);
        }


        public Task<UpdateChannelResponseInternalDTO> TruncateChannelAsync(string channelType, string channelId)
        {
            var endpoint = ChannelEndpoints.TruncateChannel(channelType, channelId);
            return Delete<UpdateChannelResponseInternalDTO>(endpoint);
        }

        public Task<SendEventResponseInternalDTO> SendTypingStartEventAsync(string channelType, string channelId)
         => PostEventAsync<SendEventRequestInternalDTO, SendEventResponseInternalDTO>(channelType, channelId, new SendEventRequestInternalDTO
         {
             Event = new EventRequestInternalDTO() { Type = WSEventType.TypingStart },
         });

        public Task<SendEventResponseInternalDTO> SendTypingStopEventAsync(string channelType, string channelId)
            => PostEventAsync<SendEventRequestInternalDTO, SendEventResponseInternalDTO>(channelType, channelId, new SendEventRequestInternalDTO
            {
                Event = new EventRequestInternalDTO() { Type = WSEventType.TypingStop },
            });
        public Task<GetAttachmentResponseInternalDTO> GetAttachmentAsync(string channelType, string channelId)
        {
            var endpoint = ChannelEndpoints.GetAttachment(channelType, channelId);
            return PostWithCustomHeader<GetAttachmentResponseInternalDTO>(endpoint, null);
        }
        #endregion
    }

}