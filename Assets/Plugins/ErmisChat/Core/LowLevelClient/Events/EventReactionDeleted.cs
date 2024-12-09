using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public class EventReactionDeleted : EventBase, ILoadableFrom<ReactionDeletedEventInternalDTO, EventReactionDeleted>
    {
        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public Message Message { get; set; }

        public Reaction Reaction { get; set; }

        public string Team { get; set; }

        public System.Collections.Generic.List<User> ThreadParticipants { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        EventReactionDeleted ILoadableFrom<ReactionDeletedEventInternalDTO, EventReactionDeleted>.LoadFromDto(ReactionDeletedEventInternalDTO dto)
        {
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Message = Message.TryLoadFromDto<MessageInternalDTO, Message>(dto.Message);
            Reaction = Reaction.TryLoadFromDto(dto.Reaction);
            Team = dto.Team;
            ThreadParticipants = ThreadParticipants.TryLoadFromDtoCollection(dto.ThreadParticipants);
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}