

using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json;
namespace Ermis.Core.InternalDTO.Responses
{
    internal class MarkReadResponseInternalDTO
    {
        [JsonProperty("event")]
        public EventResponseInternalDTO Event;
        [JsonProperty("duration")]
        public string Duration;

    }

}

