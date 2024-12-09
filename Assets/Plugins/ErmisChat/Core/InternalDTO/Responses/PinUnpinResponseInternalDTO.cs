using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Responses
{
    internal  class PinUnpinResponseInternalDTO
    {
        [JsonProperty("message")]
        public MessageResponseInternalDTO Message;
        [JsonProperty("duration")]
        public string Duration;
    }
}
