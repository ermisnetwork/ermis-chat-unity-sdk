﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Responses
{
    
    internal class ChannelResponseInternalDTO
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("image")]
        public string Image;
        [JsonProperty("description")]
        public string Description;
        [JsonProperty("public")]
        public bool Public;
        [JsonProperty("cid")]
        public string Cid;
        [JsonProperty("created_by")]
        public UserIdObjectInternalDTO CreatedBy;
        [JsonProperty("member_count")]
        public int MemberCount;
        [JsonProperty("members")]
        public List<ChannelMemberInternalDTO> Members;
        [JsonProperty("member_capabilities")]
        public List<string> MemberCapabilities;
        [JsonProperty("filter_words")]
        public List<string> FilterWords;
        [JsonProperty("last_message_at")]
        public DateTimeOffset LastMessageAt;
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt;
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt;

    }

}

