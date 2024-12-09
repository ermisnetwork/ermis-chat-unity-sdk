using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class UpdateChannelCapabilitiesRequest : RequestObjectBase, ISavableTo<UpdateChannelCapabilitiesRequestDTO>
    {

        public System.Collections.Generic.List<string> Capabilities { get; set; }

        UpdateChannelCapabilitiesRequestDTO ISavableTo<UpdateChannelCapabilitiesRequestDTO>.SaveToDto() =>
            new UpdateChannelCapabilitiesRequestDTO
            {
                Capabilities = Capabilities,
            };

    }
}