using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    /// <summary>
    /// Sets thresholds for AI moderation
    /// </summary>
    public class ErmisThresholds : IStateLoadableFrom<ThresholdsInternalDTO, ErmisThresholds>
    {
        /// <summary>
        /// Thresholds for explicit messages
        /// </summary>
        public ErmisLabelThresholds Explicit { get; private set; }

        /// <summary>
        /// Thresholds for spam
        /// </summary>
        public ErmisLabelThresholds Spam { get; private set; }

        /// <summary>
        /// Thresholds for toxic messages
        /// </summary>
        public ErmisLabelThresholds Toxic { get; private set; }

        ErmisThresholds IStateLoadableFrom<ThresholdsInternalDTO, ErmisThresholds>.LoadFromDto(ThresholdsInternalDTO dto, ICache cache)
        {
            Explicit = Explicit.TryLoadFromDto(dto.Explicit, cache);
            Spam = Explicit.TryLoadFromDto(dto.Spam, cache);
            Toxic = Explicit.TryLoadFromDto(dto.Toxic, cache);

            return this;
        }
    }
}