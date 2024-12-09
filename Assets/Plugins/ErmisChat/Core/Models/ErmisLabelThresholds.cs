using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisLabelThresholds : IStateLoadableFrom<LabelThresholdsInternalDTO, ErmisLabelThresholds>
    {
        /// <summary>
        /// Threshold for automatic message block
        /// </summary>
        public float? Block { get; private set; }

        /// <summary>
        /// Threshold for automatic message flag
        /// </summary>
        public float? Flag { get; private set; }

        ErmisLabelThresholds IStateLoadableFrom<LabelThresholdsInternalDTO, ErmisLabelThresholds>.LoadFromDto(LabelThresholdsInternalDTO dto, ICache cache)
        {
            Block = dto.Block;
            Flag = dto.Flag;

            return this;
        }
    }
}