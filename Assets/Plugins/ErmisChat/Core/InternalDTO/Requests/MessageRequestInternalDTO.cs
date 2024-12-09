﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ermis.Core.InternalDTO.Requests
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    internal class MessageRequestInternalDTO
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("attachments")]
        public List<AttachmentRequestInternalDTO> Attachments;

        [JsonProperty("mentioned_all")]
        public bool? MentionedAll;

        [JsonProperty("mentioned_users")]
        public List<string> MentionedUsers;

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("pinned")]
        public bool? Pinned;

        [JsonProperty("quoted_message_id")]
        public string QuotedMessageId;

        [JsonProperty("text")]
        public string Text;

    }

}

