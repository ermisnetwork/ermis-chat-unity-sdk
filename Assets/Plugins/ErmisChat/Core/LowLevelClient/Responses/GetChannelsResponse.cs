using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public class GetChannelsResponse : ResponseObjectBase, ILoadableFrom<GetChannelResponseInternalDTO, GetChannelsResponse>
    {
        /// <summary>
        /// List of channels
        /// </summary>
        public System.Collections.Generic.List<ChannelStateResponseFields> Channels { get; set; }

        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }
        GetChannelsResponse ILoadableFrom<GetChannelResponseInternalDTO, GetChannelsResponse>.LoadFromDto(GetChannelResponseInternalDTO dto)
        {
            Channels = Channels.TryLoadFromDtoCollection(dto.Channels);
            Duration = dto.Duration;
            return this;
        }
    }
}
