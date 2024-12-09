using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class ChannelMessages : ResponseObjectBase, ILoadableFrom<ChannelMessagesInternalDTO, ChannelMessages>
    {
        public Channel Channel { get; set; }

        public System.Collections.Generic.List<Message> Messages { get; set; }

        ChannelMessages ILoadableFrom<ChannelMessagesInternalDTO, ChannelMessages>.LoadFromDto(ChannelMessagesInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            Messages = Messages.TryLoadFromDtoCollection(dto.Messages);

            return this;
        }
    }
}