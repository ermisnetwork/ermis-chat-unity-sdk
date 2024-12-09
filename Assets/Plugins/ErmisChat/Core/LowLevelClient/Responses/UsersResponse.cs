using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class UsersResponse : ResponseObjectBase, ILoadableFrom<QueryUsersResponseInternalDTO, UsersResponse>
    {
        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// List of found users
        /// </summary>
        public List<FullUserResponseInternalDTO> Users { get; set; }

        UsersResponse ILoadableFrom<QueryUsersResponseInternalDTO, UsersResponse>.LoadFromDto(QueryUsersResponseInternalDTO dto)
        {
            //Duration = dto.;
            Users = dto.Users;
            //AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}