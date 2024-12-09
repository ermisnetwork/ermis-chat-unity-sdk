using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class ChannelsResponse : ResponseObjectBase, ILoadableFrom<ChannelStateResponseInternalDTO, ChannelsResponse>
    {
        /// <summary>
        /// List of channels
        /// </summary>
        public System.Collections.Generic.List<ChannelState> Channels { get; set; }

        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        ChannelsResponse ILoadableFrom<ChannelStateResponseInternalDTO, ChannelsResponse>.LoadFromDto(ChannelStateResponseInternalDTO dto)
        {
            Channels = Channels.TryLoadFromDtoCollection(dto.Channels);
            Duration = dto.Duration;

            return this;
        }
    }
}