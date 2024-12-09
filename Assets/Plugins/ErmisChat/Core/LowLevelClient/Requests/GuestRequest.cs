using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class GuestRequest : RequestObjectBase, ISavableTo<CreateGuestRequestInternalDTO>
    {
        public UserObjectRequest User { get; set; } = new UserObjectRequest();

        CreateGuestRequestInternalDTO ISavableTo<CreateGuestRequestInternalDTO>.SaveToDto() =>
            new CreateGuestRequestInternalDTO
            {
                User = User.TrySaveToDto<UserRequestInternalDTO>(),
                AdditionalProperties = AdditionalProperties,
            };
    }
}