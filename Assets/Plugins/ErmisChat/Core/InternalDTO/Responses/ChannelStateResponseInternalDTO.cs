﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ermis.Core.InternalDTO.Responses
{
    internal partial class ChannelStateResponseInternalDTO
    {
        [JsonProperty("channels")]
        public List<ChannelStateResponseFieldsInternalDTO> Channels;
        [JsonProperty("channel")]
        public ChannelResponseInternalDTO Channel;
        [JsonProperty("messages")]
        public List<MessageResponseInternalDTO> Messages;
        [JsonProperty("watchers")]
        public List<UserResponseInternalDTO> Watchers;
        [JsonProperty("read")]
        public List<ReadStateResponseInternalDTO> Read;
        [JsonProperty("membership")]
        public ChannelMemberInternalDTO Membership;
        [JsonProperty("duration")]
        public string Duration;
    }

}
