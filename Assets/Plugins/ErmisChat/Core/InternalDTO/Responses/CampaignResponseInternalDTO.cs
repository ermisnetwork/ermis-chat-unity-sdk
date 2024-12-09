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

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    internal partial class CampaignResponseInternalDTO
    {
        [Newtonsoft.Json.JsonProperty("channel_template", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public CampaignChannelTemplateInternalDTO ChannelTemplate { get; set; }

        [Newtonsoft.Json.JsonProperty("create_channels", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool CreateChannels { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset CreatedAt { get; set; }

        [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Description { get; set; }

        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("message_template", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public CampaignMessageTemplateInternalDTO MessageTemplate { get; set; }

        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("scheduled_for", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? ScheduledFor { get; set; }

        [Newtonsoft.Json.JsonProperty("segment_ids", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<string> SegmentIds { get; set; } = new System.Collections.Generic.List<string>();

        [Newtonsoft.Json.JsonProperty("segments", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<SegmentInternalDTO> Segments { get; set; } = new System.Collections.Generic.List<SegmentInternalDTO>();

        [Newtonsoft.Json.JsonProperty("sender", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public UserResponseInternalDTO Sender { get; set; }

        [Newtonsoft.Json.JsonProperty("sender_id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string SenderId { get; set; }

        [Newtonsoft.Json.JsonProperty("skip_push", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool SkipPush { get; set; }

        [Newtonsoft.Json.JsonProperty("skip_webhook", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool SkipWebhook { get; set; }

        [Newtonsoft.Json.JsonProperty("stats", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public CampaignStatsResponseInternalDTO Stats { get; set; } = new CampaignStatsResponseInternalDTO();

        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Status { get; set; }

        [Newtonsoft.Json.JsonProperty("stop_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? StopAt { get; set; }

        [Newtonsoft.Json.JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset UpdatedAt { get; set; }

        [Newtonsoft.Json.JsonProperty("user_ids", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<string> UserIds { get; set; } = new System.Collections.Generic.List<string>();

        [Newtonsoft.Json.JsonProperty("users", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<UserResponseInternalDTO> Users { get; set; } = new System.Collections.Generic.List<UserResponseInternalDTO>();

        private System.Collections.Generic.Dictionary<string, object> _additionalProperties;

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.Dictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

}

