
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json;
namespace Ermis.Core.InternalDTO.Requests
{
    internal partial class EventRequestInternalDTO
    {
        [JsonProperty("type")]
        public string Type;
        
    }

}

