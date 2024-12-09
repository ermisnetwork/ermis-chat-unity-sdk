using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Events
{
    public class EventMessageNew : EventBase, ILoadableFrom<MessageNewEventInternalDTO, EventMessageNew>
    {
        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public MessageResponse Message { get; set; }

        public string Team { get; set; }

        public System.Collections.Generic.List<User> ThreadParticipants { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        public int? WatcherCount { get; set; }

        EventMessageNew ILoadableFrom<MessageNewEventInternalDTO, EventMessageNew>.LoadFromDto(MessageNewEventInternalDTO dto)
        {
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Message = Message.TryLoadFromDto(dto.Message);
            Team = dto.Team;
            ThreadParticipants = ThreadParticipants.TryLoadFromDtoCollection(dto.ThreadParticipants);
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            WatcherCount = dto.WatcherCount;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}