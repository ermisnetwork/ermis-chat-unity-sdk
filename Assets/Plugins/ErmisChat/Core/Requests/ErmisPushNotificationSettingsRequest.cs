using System;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Requests
{
    public class ErmisPushNotificationSettingsRequest : ISavableTo<PushNotificationSettingsRequestInternalDTO>
    {
        public bool Disabled { get; set; }

        public DateTimeOffset DisabledUntil { get; set; }

        PushNotificationSettingsRequestInternalDTO ISavableTo<PushNotificationSettingsRequestInternalDTO>.SaveToDto() =>
            new PushNotificationSettingsRequestInternalDTO
            {
                Disabled = Disabled,
                DisabledUntil = DisabledUntil,
            };
    }
}