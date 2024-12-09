using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal interface IInternalModerationApi
    {
        Task<SimpleResponseInternalDTO> MuteUserAsync(ChannelMuteRequestInternalDTO muteUserRequest);

        Task<SimpleResponseInternalDTO> UnmuteUserAsync(ChannelMuteRequestInternalDTO unmuteUserRequest);

        Task<ResponseInternalDTO> BanUserAsync(BanRequestInternalDTO banRequest);

        Task<ResponseInternalDTO> UnbanUserAsync(string targetUserId, string type, string id);

        Task<ResponseInternalDTO> ShadowBanUserAsync(BanRequestInternalDTO shadowBanRequest);

        Task<ResponseInternalDTO> RemoveUserShadowBanAsync(string targetUserId, string type, string id);

        Task<QueryBannedUsersResponseInternalDTO> QueryBannedUsersAsync(QueryBannedUsersRequestInternalDTO queryBannedUsersRequest);

        Task<FlagResponseInternalDTO> FlagUserAsync(string targetUserId);

        Task<FlagResponseInternalDTO> FlagMessageAsync(string targetMessageId);

        Task<QueryMessageFlagsResponseInternalDTO> QueryMessageFlagsAsync(QueryMessageFlagsRequestInternalDTO queryMessageFlagsRequest);
    }
}