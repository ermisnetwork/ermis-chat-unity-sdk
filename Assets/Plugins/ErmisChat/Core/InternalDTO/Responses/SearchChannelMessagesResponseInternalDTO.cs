
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Requests
{
    internal class SearchChannelMessagesResponseInternalDTO
    {
        [JsonProperty("search_result")]
        public SearchMessagesResultResponseInternalDTO SearchResult;
        [JsonProperty("duration")]
        public string Duration;
    }

    internal class SearchMessagesResultResponseInternalDTO
    {
        [JsonProperty("limit")]
        public int Limit;
        [JsonProperty("offset")]
        public int Offset;
        [JsonProperty("total")]
        public int Total;
        [JsonProperty("messages")]
        public List<MessageResultResponseInternalDTO> Messages;
    }

    internal class MessageResultResponseInternalDTO
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("text")]
        public string Text;
        [JsonProperty("user_id")]
        public string UserId;
        [JsonProperty("created_at")]
        public string CreatedAt;
    }
}

