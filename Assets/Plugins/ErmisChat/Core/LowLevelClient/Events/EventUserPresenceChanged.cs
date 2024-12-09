using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventUserPresenceChanged : EventBase,
        ILoadableFrom<UserPresenceChangedEventInternalDTO, EventUserPresenceChanged>
    {
        public string Type { get; set; }

        public User User { get; set; }

        EventUserPresenceChanged ILoadableFrom<UserPresenceChangedEventInternalDTO, EventUserPresenceChanged>.
            LoadFromDto(UserPresenceChangedEventInternalDTO dto)
        {
            CreatedAt = dto.CreatedAt;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}