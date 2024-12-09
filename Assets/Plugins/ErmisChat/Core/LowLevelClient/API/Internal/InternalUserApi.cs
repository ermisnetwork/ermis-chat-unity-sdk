using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.Web;
using Ermis.Libs.Http;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal class InternalUserApi : InternalApiClientBase, IInternalUserApi
    {
        public InternalUserApi(IHttpClient httpClient, ISerializer serializer, ILogs logs,
            IRequestUriFactory requestUriFactory, IErmisChatLowLevelClient lowLevelClient)
            : base(httpClient, serializer, logs, requestUriFactory, lowLevelClient)
        {
        }

        public Task<QueryUsersResponseInternalDTO> QueryUsersAsync(QueryUsersRequestInternalDTO queryUsersRequest)
            => GetWithCustomHeader<QueryUsersRequestInternalDTO, QueryUsersResponseInternalDTO>("/uss/v1/users", queryUsersRequest);

        //public Task<QueryUsersResponseDto> QueryUsersAsync(QueryUsersRequestInternalDTO queryUsersRequest)
        //   => GetWithCustomHeader<QueryUsersRequestInternalDTO, QueryUsersResponseDto>("/uss/v1/users", queryUsersRequest);

        public Task<CreateGuestResponseInternalDTO> CreateGuestAsync(CreateGuestRequestInternalDTO createGuestRequest)
            => Post<CreateGuestRequestInternalDTO, CreateGuestResponseInternalDTO>("/guest", createGuestRequest);

        public Task<UpdateUsersResponseInternalDTO>
            UpsertManyUsersAsync(UpdateUsersRequestInternalDTO updateUsersRequest)
            => Post<UpdateUsersRequestInternalDTO, UpdateUsersResponseInternalDTO>("/users", updateUsersRequest);

        public Task<UpdateUsersResponseInternalDTO>
            UpdateUserPartialAsync(UpdateUserPartialRequestInternalDTO updateUserPartialRequest)
            => Patch<UpdateUserPartialRequestInternalDTO, UpdateUsersResponseInternalDTO>("/users",
                updateUserPartialRequest);
    }
}