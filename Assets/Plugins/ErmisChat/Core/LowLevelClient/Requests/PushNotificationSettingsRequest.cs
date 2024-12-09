using System;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class PushNotificationSettingsRequest : RequestObjectBase, ISavableTo<PushNotificationSettingsRequestInternalDTO>, ISavableTo<PushNotificationSettingsInternalDTO>
    {
        public bool Disabled { get; set; }

        public DateTimeOffset DisabledUntil { get; set; }

        PushNotificationSettingsRequestInternalDTO ISavableTo<PushNotificationSettingsRequestInternalDTO>.SaveToDto() =>
            new PushNotificationSettingsRequestInternalDTO
            {
                Disabled = Disabled,
                DisabledUntil = DisabledUntil,
                AdditionalProperties = AdditionalProperties
            };
        
        PushNotificationSettingsInternalDTO ISavableTo<PushNotificationSettingsInternalDTO>.SaveToDto() =>
            new PushNotificationSettingsInternalDTO
            {
                Disabled = Disabled,
                DisabledUntil = DisabledUntil,
                AdditionalProperties = AdditionalProperties
            };
    }
}