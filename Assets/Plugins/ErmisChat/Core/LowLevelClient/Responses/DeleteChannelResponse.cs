using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class DeleteChannelResponse : ResponseObjectBase, ILoadableFrom<DeleteChannelResponseInternalDTO, DeleteChannelResponse>
    {
        public Channel Channel { get; set; }

        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        DeleteChannelResponse ILoadableFrom<DeleteChannelResponseInternalDTO, DeleteChannelResponse>.LoadFromDto(DeleteChannelResponseInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            Duration = dto.Duration;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}