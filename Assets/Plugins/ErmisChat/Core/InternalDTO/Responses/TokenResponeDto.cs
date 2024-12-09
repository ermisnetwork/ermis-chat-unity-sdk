using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Responses
{
    public class TokenResponeDto
    {
        [JsonProperty("token")]
        public string Token;
        [JsonProperty("refresh_token")]
        public string RefreshToken;
        [JsonProperty("user_id")]
        public string UserId;
        [JsonProperty("project_id")]
        public string ProjectId;

    }
}