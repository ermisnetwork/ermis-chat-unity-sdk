using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventNotificationMutesUpdated : EventBase,
        ILoadableFrom<NotificationMutesUpdatedEventInternalDTO, EventNotificationMutesUpdated>
    {
        public OwnUser Me { get; set; }

        public string Type { get; set; }

        EventNotificationMutesUpdated
            ILoadableFrom<NotificationMutesUpdatedEventInternalDTO, EventNotificationMutesUpdated>.LoadFromDto(
                NotificationMutesUpdatedEventInternalDTO dto)
        {
            CreatedAt = dto.CreatedAt;
            Me = Me.TryLoadFromDto<OwnUserInternalDTO, OwnUser>(dto.Me);
            Type = dto.Type;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}