using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    /// <summary>
    /// Trigger: when a channel is marked as read
    /// Recipients: clients watching the channel
    /// </summary>
    public partial class EventMessageRead : EventBase, ILoadableFrom<MessageReadEventInternalDTO, EventMessageRead>
    {
        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public string Team { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        EventMessageRead ILoadableFrom<MessageReadEventInternalDTO, EventMessageRead>.LoadFromDto(MessageReadEventInternalDTO dto)
        {
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Team = dto.Team;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}