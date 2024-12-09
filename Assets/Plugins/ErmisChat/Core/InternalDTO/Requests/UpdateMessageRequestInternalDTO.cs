


using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Requests
{
    internal class UpdateMessageRequestInternalDTO
    {
        [JsonProperty("message")]
        public MessageUpdateRequestInternalDTO Message;
    }

    internal class MessageUpdateRequestInternalDTO
    {
        [JsonProperty("text")]
        public string Text;
    }

}

