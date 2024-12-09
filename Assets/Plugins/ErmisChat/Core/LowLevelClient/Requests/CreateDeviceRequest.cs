using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class CreateDeviceRequest : RequestObjectBase, ISavableTo<CreateDeviceRequestInternalDTO>
    {
        public string Id { get; set; }

        public PushProviderType PushProvider { get; set; }


        CreateDeviceRequestInternalDTO ISavableTo<CreateDeviceRequestInternalDTO>.SaveToDto()
            => new CreateDeviceRequestInternalDTO
            {
                Id = Id,
                PushProvider = PushProvider.TrySaveToDto(),
            };
    }
}