﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;

namespace Ermis.Core.LowLevelClient.Models
{
    public partial class ImageSize  : ModelBase, ILoadableFrom<ImageSizeInternalDTO, ImageSize>
    {
        /// <summary>
        /// Crop mode
        /// </summary>
        public ImageCropType? Crop { get; set; }

        /// <summary>
        /// Target image height
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Resize method
        /// </summary>
        public ImageResizeType? Resize { get; set; }

        /// <summary>
        /// Target image width
        /// </summary>
        public int? Width { get; set; }

        ImageSize ILoadableFrom<ImageSizeInternalDTO, ImageSize>.LoadFromDto(ImageSizeInternalDTO dto)
        {
            Crop = Crop.TryLoadNullableStructFromDto(dto.Crop);
            Height = dto.Height;
            Resize = Resize.TryLoadNullableStructFromDto(dto.Resize);
            Width = dto.Width;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}