using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisAttachmentImages : IStateLoadableFrom<ImagesInternalDTO, ErmisAttachmentImages>
    {
        public ErmisImageData FixedHeight { get; private set; }

        public ErmisImageData FixedHeightDownsampled { get; private set; }

        public ErmisImageData FixedHeightStill { get; private set; }

        public ErmisImageData FixedWidth { get; private set; }

        public ErmisImageData FixedWidthDownsampled { get; private set; }

        public ErmisImageData FixedWidthStill { get; private set; }

        public ErmisImageData Original { get; private set; }

        ErmisAttachmentImages IStateLoadableFrom<ImagesInternalDTO, ErmisAttachmentImages>.LoadFromDto(ImagesInternalDTO dto, ICache cache)
        {
            FixedHeight = FixedHeight.TryLoadFromDto(dto.FixedHeight, cache);
            FixedHeightDownsampled = FixedHeightDownsampled.TryLoadFromDto(dto.FixedHeightDownsampled, cache);
            FixedHeightStill = FixedHeightStill.TryLoadFromDto(dto.FixedHeightStill, cache);
            FixedWidth = FixedWidth.TryLoadFromDto(dto.FixedWidth, cache);
            FixedWidthDownsampled = FixedWidthDownsampled.TryLoadFromDto(dto.FixedWidthDownsampled, cache);
            FixedWidthStill = FixedWidthStill.TryLoadFromDto(dto.FixedWidthStill, cache);
            Original = Original.TryLoadFromDto(dto.Original, cache);

            return this;
        }
    }
}