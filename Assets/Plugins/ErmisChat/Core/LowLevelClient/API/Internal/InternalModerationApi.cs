using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.Web;
using Ermis.Libs.Http;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal class InternalModerationApi : InternalApiClientBase, IInternalModerationApi
    {
        public InternalModerationApi(IHttpClient httpClient, ISerializer serializer, ILogs logs,
            IRequestUriFactory requestUriFactory, IErmisChatLowLevelClient lowLevelClient)
            : base(httpClient, serializer, logs, requestUriFactory, lowLevelClient)
        {
        }

        public Task<SimpleResponseInternalDTO> MuteUserAsync(ChannelMuteRequestInternalDTO muteUserRequest)
        {
            var endpoint = ModerationEndpoints.MuteUser();
            return PostWithCustomHeader<ChannelMuteRequestInternalDTO, SimpleResponseInternalDTO>(endpoint, muteUserRequest);
        }

        public Task<SimpleResponseInternalDTO> UnmuteUserAsync(ChannelMuteRequestInternalDTO unmuteUserRequest)
        {
            var endpoint = ModerationEndpoints.UnmuteUser();
            return PostWithCustomHeader<ChannelMuteRequestInternalDTO, SimpleResponseInternalDTO>(endpoint, unmuteUserRequest);
        }

        public Task<ResponseInternalDTO> BanUserAsync(BanRequestInternalDTO banRequest)
        {
            const string endpoint = "/moderation/ban";
            return Post<BanRequestInternalDTO, ResponseInternalDTO>(endpoint, banRequest);
        }

        public Task<ResponseInternalDTO> UnbanUserAsync(string targetUserId, string type, string id)
        {
            const string endpoint = "/moderation/ban";

            var parameters = QueryParameters.Default
                .Append("target_user_id", targetUserId)
                .Append("type", type)
                .Append("id", id);

            return Delete<ResponseInternalDTO>(endpoint, parameters);
        }

        public Task<ResponseInternalDTO> ShadowBanUserAsync(BanRequestInternalDTO shadowBanRequest)
        {
            const string endpoint = "/moderation/ban";
            return Post<BanRequestInternalDTO, ResponseInternalDTO>(endpoint, shadowBanRequest);
        }

        public Task<ResponseInternalDTO> RemoveUserShadowBanAsync(string targetUserId, string type, string id)
            => UnbanUserAsync(targetUserId, type, id);

        public Task<QueryBannedUsersResponseInternalDTO> QueryBannedUsersAsync(QueryBannedUsersRequestInternalDTO queryBannedUsersRequest)
        {
            const string endpoint = "/query_banned_users";
            return Get<QueryBannedUsersRequestInternalDTO, QueryBannedUsersResponseInternalDTO>(endpoint, queryBannedUsersRequest);
        }

        public Task<FlagResponseInternalDTO> FlagUserAsync(string targetUserId)
        {
            const string endpoint = "/moderation/flag";

            var request = new FlagRequestInternalDTO
            {
                TargetUserId = targetUserId
            };

            return Post<FlagRequestInternalDTO, FlagResponseInternalDTO>(endpoint, request);
        }

        public Task<FlagResponseInternalDTO> FlagMessageAsync(string targetMessageId)
        {
            const string endpoint = "/moderation/flag";

            var request = new FlagRequestInternalDTO
            {
                TargetMessageId = targetMessageId
            };

            return Post<FlagRequestInternalDTO, FlagResponseInternalDTO>(endpoint, request);
        }

        public Task<QueryMessageFlagsResponseInternalDTO> QueryMessageFlagsAsync(QueryMessageFlagsRequestInternalDTO queryMessageFlagsRequest)
        {
            const string endpoint = "/moderation/flags/message";
            return Get<QueryMessageFlagsRequestInternalDTO, QueryMessageFlagsResponseInternalDTO>(endpoint, queryMessageFlagsRequest);
        }
    }
}