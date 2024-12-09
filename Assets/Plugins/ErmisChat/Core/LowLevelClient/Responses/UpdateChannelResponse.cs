using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class UpdateChannelResponse : ResponseObjectBase, ILoadableFrom<UpdateChannelResponseInternalDTO, UpdateChannelResponse>
    {
        public Channel Channel { get; set; }

        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        public System.Collections.Generic.List<ChannelMember> Members { get; set; }

        public Message Message { get; set; }

        UpdateChannelResponse ILoadableFrom<UpdateChannelResponseInternalDTO, UpdateChannelResponse>.LoadFromDto(UpdateChannelResponseInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            Duration = dto.Duration;
            Members = Members.TryLoadFromDtoCollection(dto.Channel.Members);

            return this;
        }
    }
}