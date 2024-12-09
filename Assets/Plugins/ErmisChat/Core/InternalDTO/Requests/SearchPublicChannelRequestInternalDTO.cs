
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Requests
{
    internal class SearchPublicChannelRequestInternalDTO
    {
        [JsonProperty("project_id")]
        public string ProjectId;
        [JsonProperty("search_term")]
        public string SearchTerm;
        [JsonProperty("limit")]
        public int Limit;
        [JsonProperty("offset")]
        public int Offset;
    }

}
