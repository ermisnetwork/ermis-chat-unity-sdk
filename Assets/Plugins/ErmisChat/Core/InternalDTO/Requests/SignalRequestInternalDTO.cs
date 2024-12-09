


using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
namespace Ermis.Core.InternalDTO.Requests
{
    
    internal class SignalRequestInternalDTO
    {
        [JsonProperty("cid")]
        public string Cid;
        [JsonProperty("connection_id")]
        public string ConnectionId;
        [JsonProperty("action")]
        public int Action;
        [JsonProperty("signal")]
        public SignalInternalDTO Signal;

    }

    internal class SignalInternalDTO
    {
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("sdp")]
        public string Sdp;
    }

}

