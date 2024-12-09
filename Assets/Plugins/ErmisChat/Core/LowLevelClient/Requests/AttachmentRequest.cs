using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class AttachmentRequest : RequestObjectBase, ISavableTo<AttachmentRequestInternalDTO>
    {
        public string AssetUrl { get; set; }
        public string Title { get; set; }

        public string Type { get; set; }

        public int FileSize { get; set; }

        public string MimeType { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbUrl { get; set; }


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