using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class ChannelMuteRequest : RequestObjectBase, ISavableTo<ChannelMuteRequestInternalDTO>
    {

        public bool Mute { get; set; }
        public int Duration { get; set; }

        ChannelMuteRequestInternalDTO ISavableTo<ChannelMuteRequestInternalDTO>.SaveToDto() =>
            new ChannelMuteRequestInternalDTO
            {
                Mute = Mute,
                Duration= Duration
            };

    }
}