using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Responses
{
    public sealed class ErmisImageSize : IStateLoadableFrom<ImageSizeInternalDTO, ErmisImageSize>
    {
        /// <summary>
        /// Crop mode
        /// </summary>
        public ErmisImageCropType? Crop { get; private set; }

        /// <summary>
        /// Target image height
        /// </summary>
        public int? Height { get; private set; }

        /// <summary>
        /// Resize method
        /// </summary>
        public ErmisImageResizeType? Resize { get; private set; }

        /// <summary>
        /// Target image width
        /// </summary>
        public int? Width { get; private set; }

        ErmisImageSize IStateLoadableFrom<ImageSizeInternalDTO, ErmisImageSize>.LoadFromDto(ImageSizeInternalDTO dto, ICache cache)
        {
            Crop = Crop.TryLoadNullableStructFromDto(dto.Crop);
            Height = dto.Height;
            Resize = Resize.TryLoadNullableStructFromDto(dto.Resize);
            Width = dto.Width;

            return this;
        }
    }
}