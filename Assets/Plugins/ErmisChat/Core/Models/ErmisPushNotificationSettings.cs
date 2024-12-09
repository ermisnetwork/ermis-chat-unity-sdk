using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisPushNotificationSettings :
        IStateLoadableFrom<PushNotificationSettingsInternalDTO, ErmisPushNotificationSettings>,
        IStateLoadableFrom<PushNotificationSettingsResponseInternalDTO, ErmisPushNotificationSettings>
    {
        public bool Disabled { get; private set; }

        public System.DateTimeOffset DisabledUntil { get; private set; }

        ErmisPushNotificationSettings
            IStateLoadableFrom<PushNotificationSettingsInternalDTO, ErmisPushNotificationSettings>.LoadFromDto(
                PushNotificationSettingsInternalDTO dto, ICache cache)
        {
            Disabled = dto.Disabled.GetValueOrDefault();
            DisabledUntil = dto.DisabledUntil.GetValueOrDefault();

            return this;
        }

        ErmisPushNotificationSettings
            IStateLoadableFrom<PushNotificationSettingsResponseInternalDTO, ErmisPushNotificationSettings>.LoadFromDto(
                PushNotificationSettingsResponseInternalDTO dto, ICache cache)
        {
            Disabled = dto.Disabled.GetValueOrDefault();
            DisabledUntil = dto.DisabledUntil.GetValueOrDefault();

            return this;
        }
    }
}