﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class ImageSizeRequest : RequestObjectBase, ISavableTo<ImageSizeRequestInternalDTO>
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

        ImageSizeRequestInternalDTO ISavableTo<ImageSizeRequestInternalDTO>.SaveToDto() =>
            new ImageSizeRequestInternalDTO()
            {
                Crop = Crop.TrySaveNullableStructToDto<ImageCropType, ImageCropTypeInternalDTO>(),
                Height = Height,
                Resize = Resize.TrySaveNullableStructToDto<ImageResizeType, ImageResizeTypeInternalDTO>(),
                Width = Width,
                AdditionalProperties = AdditionalProperties
            };
    }
}