using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class UpdateUsersRequest : RequestObjectBase, ISavableTo<UpdateUsersRequestInternalDTO>
    {
        /// <summary>
        /// Object containing users
        /// </summary>
        /// ErmisTodo: Check if this could be list
        public System.Collections.Generic.Dictionary<string, UserObjectRequest> Users { get; set; }

        UpdateUsersRequestInternalDTO ISavableTo<UpdateUsersRequestInternalDTO>.SaveToDto()
        {
            var dto = new UpdateUsersRequestInternalDTO
            {
                // Users = Users.TrySaveToDtoDictionary<UserObjectRequestInternalDTO, UserObjectRequest, string>(),
                AdditionalProperties = AdditionalProperties,
            };

            // Ticket#38178 TrySaveToDtoDictionary caused crashes on old Android versions with IL2CPP
            // Perhaps this due to IL2CPP not handling well complex generic signatures
            if (Users != null)
            {
                var dict = new Dictionary<string, UserRequestInternalDTO>();
            
                foreach (var sourceKeyValue in Users)
                {
                    if (sourceKeyValue.Value == null)
                    {
                        continue;
                    }

                    var serialized = sourceKeyValue.Value.TrySaveToDto<UserRequestInternalDTO>();

                    if (serialized != null)
                    {
                        dict.Add(sourceKeyValue.Key,serialized);
                    }
                }
            
                dto.Users = dict;
            }

            return dto;
        }
    }
}