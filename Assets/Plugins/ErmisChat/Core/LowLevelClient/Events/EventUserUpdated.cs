using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventUserUpdated : EventBase,
        ILoadableFrom<UserUpdatedEventInternalDTO, EventUserUpdated>
    {
        public string Type { get; set; }

        public User User { get; set; }

        EventUserUpdated ILoadableFrom<UserUpdatedEventInternalDTO, EventUserUpdated>.LoadFromDto(
            UserUpdatedEventInternalDTO dto)
        {
            CreatedAt = dto.CreatedAt;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserEventPayloadInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}