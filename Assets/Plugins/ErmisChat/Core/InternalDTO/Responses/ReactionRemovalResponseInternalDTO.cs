
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Responses
{
    
    internal class ReactionRemovalResponseInternalDTO
    {
        [JsonProperty("message")]
        public MessageResponseInternalDTO Message;
        [JsonProperty("reaction")]
        public ReactionResponseInternalDTO Reaction;
        [JsonProperty("duration")]
        public string Duration;
    }

}

