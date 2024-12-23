﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;

namespace Ermis.Core.InternalDTO.Responses
{
    using System = global::System;

    /// <summary>
    /// Result of the message moderation
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    internal partial class MessageModerationResultInternalDTO
    {
        /// <summary>
        /// Action taken by automod
        /// </summary>
        [Newtonsoft.Json.JsonProperty("action", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Action { get; set; }

        /// <summary>
        /// Response from AI moderation
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ai_moderation_response", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ModerationResponseInternalDTO AiModerationResponse { get; set; }

        /// <summary>
        /// Word that was blocked
        /// </summary>
        [Newtonsoft.Json.JsonProperty("blocked_word", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string BlockedWord { get; set; }

        /// <summary>
        /// Name of the blocklist
        /// </summary>
        [Newtonsoft.Json.JsonProperty("blocklist_name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string BlocklistName { get; set; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        [Newtonsoft.Json.JsonProperty("created_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// ID of the message
        /// </summary>
        [Newtonsoft.Json.JsonProperty("message_id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string MessageId { get; set; }

        /// <summary>
        /// User who moderated the message
        /// </summary>
        [Newtonsoft.Json.JsonProperty("moderated_by", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ModeratedBy { get; set; }

        /// <summary>
        /// Thresholds for AI moderation
        /// </summary>
        [Newtonsoft.Json.JsonProperty("moderation_thresholds", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ThresholdsInternalDTO ModerationThresholds { get; set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        [Newtonsoft.Json.JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Whether user has bad karma
        /// </summary>
        [Newtonsoft.Json.JsonProperty("user_bad_karma", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool UserBadKarma { get; set; }

        /// <summary>
        /// Karma of the user
        /// </summary>
        [Newtonsoft.Json.JsonProperty("user_karma", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float UserKarma { get; set; }

        private System.Collections.Generic.Dictionary<string, object> _additionalProperties;

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.Dictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

}

