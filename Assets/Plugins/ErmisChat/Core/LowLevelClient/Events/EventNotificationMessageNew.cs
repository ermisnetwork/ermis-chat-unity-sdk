using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Events
{
    public partial class EventNotificationMessageNew : EventBase,
        ILoadableFrom<NotificationNewMessageEventInternalDTO, EventNotificationMessageNew>
    {
        public Channel Channel { get; set; }

        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public MessageResponse Message { get; set; }

        public string Team { get; set; }

        public string Type { get; set; }

        EventNotificationMessageNew ILoadableFrom<NotificationNewMessageEventInternalDTO, EventNotificationMessageNew>.
            LoadFromDto(NotificationNewMessageEventInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Message = Message.TryLoadFromDto(dto.Message);
            Team = dto.Team;
            Type = dto.Type;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}