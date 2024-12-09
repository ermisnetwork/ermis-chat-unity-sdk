﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Newtonsoft.Json;
using System;

namespace Ermis.Core.InternalDTO.Models
{
    internal class ChannelMemberInternalDTO
    {
        [JsonProperty("user")]
        public UserIdObjectInternalDTO User;
        [JsonProperty("user_id")]
        public string UserId;
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt;
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt;
        [JsonProperty("channel_role")]
        public string ChannelRole;
        [JsonProperty("banned")]
        public bool? Banned;
        [JsonProperty("blocked")]
        public bool? Blocked;

    }

}

