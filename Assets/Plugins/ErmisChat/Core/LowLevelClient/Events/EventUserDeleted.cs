using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventUserDeleted : EventBase,
        ILoadableFrom<UserDeletedEventInternalDTO, EventUserDeleted>
    {
        public bool? DeleteConversationChannels { get; set; }

        public bool? HardDelete { get; set; }

        public bool? MarkMessagesDeleted { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        EventUserDeleted ILoadableFrom<UserDeletedEventInternalDTO, EventUserDeleted>.LoadFromDto(
            UserDeletedEventInternalDTO dto)
        {
            CreatedAt = dto.CreatedAt;
            DeleteConversationChannels = dto.DeleteConversationChannels;
            HardDelete = dto.HardDelete;
            MarkMessagesDeleted = dto.MarkMessagesDeleted;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}