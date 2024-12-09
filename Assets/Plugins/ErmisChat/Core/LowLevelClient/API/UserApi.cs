using System;
using System.Threading.Tasks;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.API.Internal;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.API
{
    internal class UserApi : IUserApi
    {
        public UserApi(IInternalUserApi internalUserApi)
        {
            _internalUserApi = internalUserApi ?? throw new ArgumentNullException(nameof(internalUserApi));
        }

        public async Task<UsersResponse> QueryUsersAsync(QueryUsersRequest queryUsersRequest)
        {
            var dto = await _internalUserApi.QueryUsersAsync(queryUsersRequest.TrySaveToDto());
            return dto.ToDomain<QueryUsersResponseInternalDTO, UsersResponse>();
        }

        public async Task<GuestResponse> CreateGuestAsync(GuestRequest createGuestRequest)
        {
            var dto = await _internalUserApi.CreateGuestAsync(createGuestRequest.TrySaveToDto());
            return dto.ToDomain<CreateGuestResponseInternalDTO, GuestResponse>();
        }

        public Task<UpdateUsersResponse> UpsertUsersAsync(UpdateUsersRequest updateUsersRequest)
            => UpsertManyUsersAsync(updateUsersRequest);

        public async Task<UpdateUsersResponse> UpsertManyUsersAsync(UpdateUsersRequest updateUsersRequest)
        {
            var dto = await _internalUserApi.UpsertManyUsersAsync(updateUsersRequest.TrySaveToDto());
            return dto.ToDomain<UpdateUsersResponseInternalDTO, UpdateUsersResponse>();
        }

        public async Task<UpdateUsersResponse> UpdateUserPartialAsync(UpdateUserPartialRequest updateUserPartialRequest)
        {
            var dto = await _internalUserApi.UpdateUserPartialAsync(updateUserPartialRequest.TrySaveToDto());
            return dto.ToDomain<UpdateUsersResponseInternalDTO, UpdateUsersResponse>();
        }

        private readonly IInternalUserApi _internalUserApi;
    }
}