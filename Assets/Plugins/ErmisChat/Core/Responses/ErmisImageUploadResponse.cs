using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Responses
{
    public sealed class ErmisImageUploadResponse
        : IStateLoadableFrom<ImageUploadResponseInternalDTO, ErmisImageUploadResponse>
    {
        /// <summary>
        /// URL to the uploaded asset. Should be used to put to `asset_url` attachment field
        /// </summary>
        public string FileUrl { get; private set; }

        /// <summary>
        /// URL of the file thumbnail for supported file formats. Should be put to `thumb_url` attachment field
        /// </summary>
        public string ThumbUrl { get; private set; }

        public System.Collections.Generic.List<ErmisImageSize> UploadSizes { get; private set; }

        ErmisImageUploadResponse IStateLoadableFrom<ImageUploadResponseInternalDTO, ErmisImageUploadResponse>.
            LoadFromDto(ImageUploadResponseInternalDTO dto, ICache cache)
        {
            FileUrl = dto.File;
            ThumbUrl = dto.ThumbUrl;
            UploadSizes = UploadSizes.TryLoadFromDtoCollection(dto.UploadSizes, cache);

            return this;
        }
    }
}