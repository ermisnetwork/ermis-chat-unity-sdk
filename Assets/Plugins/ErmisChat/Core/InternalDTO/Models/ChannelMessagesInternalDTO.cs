﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;

namespace Ermis.Core.InternalDTO.Models
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    internal partial class ChannelMessagesInternalDTO
    {
        [Newtonsoft.Json.JsonProperty("channel", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ChannelResponseInternalDTO Channel { get; set; }

        [Newtonsoft.Json.JsonProperty("messages", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<MessageInternalDTO> Messages { get; set; } = new System.Collections.Generic.List<MessageInternalDTO>();

        private System.Collections.Generic.Dictionary<string, object> _additionalProperties;

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.Dictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

}

