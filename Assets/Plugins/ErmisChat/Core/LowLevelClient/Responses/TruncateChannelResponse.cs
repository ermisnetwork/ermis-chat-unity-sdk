using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class TruncateChannelResponse : ResponseObjectBase, ILoadableFrom<TruncateChannelResponseInternalDTO, TruncateChannelResponse>
    {
        public Channel Channel { get; set; }

        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        public Message Message { get; set; }

        TruncateChannelResponse ILoadableFrom<TruncateChannelResponseInternalDTO, TruncateChannelResponse>.LoadFromDto(TruncateChannelResponseInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            Duration = dto.Duration;
            Message = Message.TryLoadFromDto<MessageInternalDTO, Message>(dto.Message);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}