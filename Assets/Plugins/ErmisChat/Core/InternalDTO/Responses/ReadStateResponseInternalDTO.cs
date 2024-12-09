﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Responses
{
    internal partial class ReadStateResponseInternalDTO
    {
        [JsonProperty("user")]
        public UserIdObjectInternalDTO User;
        [JsonProperty("last_read")]
        public System.DateTimeOffset LastRead;
        [JsonProperty("last_read_message_id")]
        public string LastReadMessageId;
        [JsonProperty("last_send")]
        public System.DateTimeOffset LastSend;
        [JsonProperty("unread_messages")]
        public int UnreadMessages;

    }

}
