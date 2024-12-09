using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisDevice : IStateLoadableFrom<DeviceInternalDTO, ErmisDevice>
    {
        /// <summary>
        /// Date/time of creation
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// Whether device is disabled or not
        /// </summary>
        public bool? Disabled { get; private set; }

        /// <summary>
        /// Reason explaining why device had been disabled
        /// </summary>
        public string DisabledReason { get; private set; }

        /// <summary>
        /// Device ID
        /// </summary>
        public string Id { get; private set; }

        public ErmisPushProviderType? PushProvider { get; private set; }

        public string UserId { get; private set; }

        ErmisDevice IStateLoadableFrom<DeviceInternalDTO, ErmisDevice>.LoadFromDto(DeviceInternalDTO dto, ICache cache)
        {
            CreatedAt = dto.CreatedAt;
            Disabled = dto.Disabled;
            DisabledReason = dto.DisabledReason;
            Id = dto.Id;
            PushProvider = PushProvider.TryLoadNullableStructFromDto(dto.PushProvider);
            UserId = dto.UserId;

            return this;
        }
    }
}