


using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
namespace Ermis.Core.InternalDTO.Requests
{
    
    internal class AttachmentRequestInternalDTO
    {
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("file_size")]
        public int FileSize;
        [JsonProperty("mime_type")]
        public string MimeType;
        [JsonProperty("asset_url")]
        public string AssetUrl;
        [JsonProperty("image_url")]
        public string ImageUrl;
        [JsonProperty("thumb_url")]
        public string ThumbUrl;

    }

}

