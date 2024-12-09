using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public partial class EventNotificationChannelMutesUpdated : EventBase,
        ILoadableFrom<NotificationChannelMutesUpdatedEventInternalDTO, EventNotificationChannelMutesUpdated>
    {
        public OwnUser Me { get; set; }

        public string Type { get; set; }

        EventNotificationChannelMutesUpdated
            ILoadableFrom<NotificationChannelMutesUpdatedEventInternalDTO, EventNotificationChannelMutesUpdated>.
            LoadFromDto(NotificationChannelMutesUpdatedEventInternalDTO dto)
        {
            CreatedAt = dto.CreatedAt;
            Me = Me.TryLoadFromDto<OwnUserInternalDTO, OwnUser>(dto.Me);
            Type = dto.Type;

            return this;
        }
    }
}