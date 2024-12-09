using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal interface IInternalUserApi
    {
        Task<QueryUsersResponseInternalDTO> QueryUsersAsync(QueryUsersRequestInternalDTO queryUsersRequest);

        Task<CreateGuestResponseInternalDTO> CreateGuestAsync(CreateGuestRequestInternalDTO createGuestRequest);

        /// <summary>
        /// <para>Creates or updates users.</para>
        /// Any user present in the payload will have its data replaced with the new version.
        /// For partial updates, use <see cref="UpdateUserPartialAsync"/> method.
        /// You can send up to 100 users per API request in both upsert and partial update API.
        /// </summary>
        Task<UpdateUsersResponseInternalDTO> UpsertManyUsersAsync(UpdateUsersRequestInternalDTO updateUsersRequest);

        Task<UpdateUsersResponseInternalDTO> UpdateUserPartialAsync(UpdateUserPartialRequestInternalDTO updateUserPartialRequest);
    }
}