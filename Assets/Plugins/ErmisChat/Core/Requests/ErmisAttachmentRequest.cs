using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Requests
{
    public class ErmisAttachmentRequest : ISavableTo<AttachmentRequestInternalDTO>
    {
        public List<ErmisActionRequest> Actions { get; set; }

        public string AssetUrl { get; set; }

        public string AuthorIcon { get; set; }

        public string AuthorLink { get; set; }

        public string AuthorName { get; set; }

        public string Color { get; set; }

        public string Fallback { get; set; }

        public List<ErmisFieldRequest> Fields { get; set; }

        public string Footer { get; set; }

        public string FooterIcon { get; set; }

        public ErmisImagesRequest Giphy { get; set; }

        public string ImageUrl { get; set; }

        public string OgScrapeUrl { get; set; }

        public int? OriginalHeight { get; set; }

        public int? OriginalWidth { get; set; }

        public string Pretext { get; set; }

        public string Text { get; set; }

        public string ThumbUrl { get; set; }

        public string Title { get; set; }

        public string TitleLink { get; set; }

        public int FileSize { get; set; }
        public string MimeType { get; set; }

        /// <summary>
        /// Attachment type (e.g. image, video, url)
        /// </summary>
        public string Type { get; set; }

        AttachmentRequestInternalDTO ISavableTo<AttachmentRequestInternalDTO>.SaveToDto() =>
            new AttachmentRequestInternalDTO
            {
                AssetUrl = AssetUrl,
                Title = Title,
                Type = Type,
                FileSize=FileSize,
                MimeType=MimeType,
                ImageUrl=ImageUrl,
                ThumbUrl=ThumbUrl,
            };
    }
}