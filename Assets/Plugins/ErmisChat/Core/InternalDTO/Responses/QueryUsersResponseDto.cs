using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Responses
{
    public class QueryUsersResponseDto
    {
        [JsonProperty("data")]
        public List<UserInfoDto> Users;
        [JsonProperty("count")]
        public int Count;
        [JsonProperty("total")]
        public int Total;
        [JsonProperty("Page")]
        public int page;
        [JsonProperty("page_count")]
        public int PageCount;
    }
    

}
