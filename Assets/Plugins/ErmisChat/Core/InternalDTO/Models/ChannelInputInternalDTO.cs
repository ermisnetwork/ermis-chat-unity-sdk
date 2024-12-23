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
    internal partial class ChannelInputInternalDTO
    {
        /// <summary>
        /// Enable or disable auto translation
        /// </summary>
        [Newtonsoft.Json.JsonProperty("auto_translation_enabled", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? AutoTranslationEnabled { get; set; }

        /// <summary>
        /// Switch auto translation language
        /// </summary>
        [Newtonsoft.Json.JsonProperty("auto_translation_language", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AutoTranslationLanguage { get; set; }

        [Newtonsoft.Json.JsonProperty("config_overrides", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ChannelConfigInternalDTO ConfigOverrides { get; set; }

        [Newtonsoft.Json.JsonProperty("created_by", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public UserIdObjectInternalDTO CreatedBy { get; set; }

        [Newtonsoft.Json.JsonProperty("created_by_id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CreatedById { get; set; }

        [Newtonsoft.Json.JsonProperty("custom", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.Dictionary<string, object> Custom { get; set; }

        [Newtonsoft.Json.JsonProperty("disabled", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Disabled { get; set; }

        /// <summary>
        /// Freeze or unfreeze the channel
        /// </summary>
        [Newtonsoft.Json.JsonProperty("frozen", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Frozen { get; set; }

        [Newtonsoft.Json.JsonProperty("invites", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<ChannelMemberInternalDTO> Invites { get; set; }

        [Newtonsoft.Json.JsonProperty("members", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<ChannelMemberInternalDTO> Members { get; set; }

        /// <summary>
        /// Team the channel belongs to (if multi-tenant mode is enabled)
        /// </summary>
        [Newtonsoft.Json.JsonProperty("team", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Team { get; set; }

        [Newtonsoft.Json.JsonProperty("truncated_by_id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string TruncatedById { get; set; }

        private System.Collections.Generic.Dictionary<string, object> _additionalProperties;

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.Dictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

}

