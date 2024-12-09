
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Requests
{
    internal class GetChannelsRequestDto
    {
        [JsonProperty("filter_conditions")]
        public FilterConditions FilterConditions;
        [JsonProperty("sort")]
        public List<Sort> Sort;
        [JsonProperty("message_limit")]
        public int MessageLimit;
    }

    public class FilterConditions
    {
        [JsonProperty("type")]
        public List<string> Type;
        [JsonProperty("limit")]
        public int? Limit ;
        [JsonProperty("offset")]
        public int Offset ;
        [JsonProperty("roles")]
        public List<string> Roles ;
        [JsonProperty("other_roles")]
        public List<string> OtherRoles ;
        [JsonProperty("banned")]
        public bool Banned ;
        [JsonProperty("blocked")]
        public bool Blocked ;
        [JsonProperty("project_id")]
        public string ProjectId ;
    }

    public class Sort
    {
        [JsonProperty("field")]
        public string Field ;
        [JsonProperty("direction")]
        public int Direction ;
    }

}
