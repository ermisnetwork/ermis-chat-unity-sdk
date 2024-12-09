using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using System;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Responses
{
    internal partial class ReactionResponseInternalDTO
    {

        [JsonProperty("message_id")]
        public string MessageId;
        [JsonProperty("user_id")]
        public string UserId;
        [JsonProperty("user")]
        public UserIdObjectInternalDTO User;
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt;
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt;

    }

}

