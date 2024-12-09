﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Responses
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    internal partial class ChannelStateResponseFieldsInternalDTO
    {
        [JsonProperty("channel")]
        public ChannelResponseInternalDTO Channel;
        [JsonProperty("messages")]
        public List<MessageResponseInternalDTO> Messages;
        [JsonProperty("watchers")]
        public List<UserIdObjectInternalDTO> Watchers;
        [JsonProperty("read")]
        public List<ReadStateResponseInternalDTO> Read;
        [JsonProperty("membership")]
        public ChannelMemberInternalDTO Membership;

    }
}

