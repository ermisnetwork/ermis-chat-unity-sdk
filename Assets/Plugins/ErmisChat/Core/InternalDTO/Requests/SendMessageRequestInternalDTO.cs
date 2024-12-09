
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Requests
{
   
    internal class SendMessageRequestInternalDTO
    {
        [JsonProperty("message")]
        public MessageRequestInternalDTO Message;
    }

}

