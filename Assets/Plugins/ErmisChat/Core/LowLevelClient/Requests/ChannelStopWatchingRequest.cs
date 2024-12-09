using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class ChannelStopWatchingRequest : RequestObjectBase, ISavableTo<ChannelStopWatchingRequestInternalDTO>
    {
        ChannelStopWatchingRequestInternalDTO ISavableTo<ChannelStopWatchingRequestInternalDTO>.SaveToDto() =>
            new ChannelStopWatchingRequestInternalDTO
            {
                AdditionalProperties = AdditionalProperties,
            };
    }
}