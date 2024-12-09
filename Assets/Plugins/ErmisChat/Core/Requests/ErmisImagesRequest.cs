using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Requests
{
    public class ErmisImagesRequest : ISavableTo<ImagesRequestInternalDTO>
    {
        public ErmisImageDataRequest FixedHeight { get; set; }

        public ErmisImageDataRequest FixedHeightDownsampled { get; set; }

        public ErmisImageDataRequest FixedHeightStill { get; set; }

        public ErmisImageDataRequest FixedWidth { get; set; }

        public ErmisImageDataRequest FixedWidthDownsampled { get; set; }

        public ErmisImageDataRequest FixedWidthStill { get; set; }

        public ErmisImageDataRequest Original { get; set; }

        ImagesRequestInternalDTO ISavableTo<ImagesRequestInternalDTO>.SaveToDto() =>
            new ImagesRequestInternalDTO
            {
                FixedHeight = FixedHeight.TrySaveToDto(),
                FixedHeightDownsampled = FixedHeightDownsampled.TrySaveToDto(),
                FixedHeightStill = FixedHeightStill.TrySaveToDto(),
                FixedWidth = FixedWidth.TrySaveToDto(),
                FixedWidthDownsampled = FixedWidthDownsampled.TrySaveToDto(),
                FixedWidthStill = FixedWidthStill.TrySaveToDto(),
                Original = Original.TrySaveToDto(),
            };
    }
}