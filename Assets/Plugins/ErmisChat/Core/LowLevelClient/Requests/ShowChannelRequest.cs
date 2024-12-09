using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class ShowChannelRequest : RequestObjectBase, ISavableTo<ShowChannelRequestInternalDTO>
    {
        ShowChannelRequestInternalDTO ISavableTo<ShowChannelRequestInternalDTO>.SaveToDto() =>
            new ShowChannelRequestInternalDTO
            {
                AdditionalProperties = AdditionalProperties,
            };
    }
}