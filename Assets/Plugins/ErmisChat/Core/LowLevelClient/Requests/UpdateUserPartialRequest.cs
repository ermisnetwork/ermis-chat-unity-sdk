using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class UpdateUserPartialRequest : RequestObjectBase, ISavableTo<UpdateUserPartialRequestInternalDTO>
    {
        public System.Collections.Generic.List<UpdateUserPartialRequestEntry> Users { get; set; }

        UpdateUserPartialRequestInternalDTO ISavableTo<UpdateUserPartialRequestInternalDTO>.SaveToDto()
        {
            return new UpdateUserPartialRequestInternalDTO
            {
                Users = Users.TrySaveToDtoCollection<UpdateUserPartialRequestEntry, UpdateUserPartialRequestEntryInternalDTO>(),
                AdditionalProperties = AdditionalProperties,
            };
        }
    }
}