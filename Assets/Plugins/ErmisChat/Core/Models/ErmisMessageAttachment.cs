using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisMessageAttachment : IStateLoadableFrom<AttachmentResponseInternalDTO, ErmisMessageAttachment>
    {
        public System.Collections.Generic.List<ErmisAttachmentAction> Actions { get; private set; }

        public string AssetUrl { get; private set; }

        public string AuthorIcon { get; private set; }

        public string AuthorLink { get; private set; }

        public string AuthorName { get; private set; }

        public string Color { get; private set; }

        public string Fallback { get; private set; }

        public System.Collections.Generic.List<ErmisAttachmentField> Fields { get; private set; }

        public string Footer { get; private set; }

        public string FooterIcon { get; private set; }

        public ErmisAttachmentImages Giphy { get; private set; }

        public string ImageUrl { get; private set; }

        public string OgScrapeUrl { get; private set; }

        public int? OriginalHeight { get; private set; }

        public int? OriginalWidth { get; private set; }

        public string Pretext { get; private set; }

        public string Text { get; private set; }

        public string ThumbUrl { get; private set; }

        public string Title { get; private set; }

        public string TitleLink { get; private set; }

        /// <summary>
        /// Attachment type (e.g. image, video, url)
        /// </summary>
        public string Type { get; private set; }

        public long FileSize { get; private set; }
        public string MimeType { get; private set; }

        ErmisMessageAttachment IStateLoadableFrom<AttachmentResponseInternalDTO, ErmisMessageAttachment>.LoadFromDto(AttachmentResponseInternalDTO dto, ICache cache)
        {
            AssetUrl = dto.AssetUrl;
            Title = dto.Title;
            Type = dto.Type;
            FileSize = dto.FileSize;
            MimeType = dto.MimeType;
            ImageUrl= dto.ImageUrl;
            ThumbUrl= dto.ThumbUrl;
            return this;
        }
    }
}