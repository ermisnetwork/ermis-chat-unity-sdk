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

    /// <summary>
    /// Represents a conversation thread linked to a specific message in a channel.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    internal partial class ThreadStateInternalDTO
    {
        [Newtonsoft.Json.JsonProperty("active_participant_count", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ActiveParticipantCount { get; set; }

        /// <summary>
        /// Channel is the channel the thread belongs to
        /// </summary>
        [Newtonsoft.Json.JsonProperty("channel", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ChannelInternalDTO Channel { get; set; }

        /// <summary>
        /// Channel CID is unique string identifier of the channel
        /// </summary>
        [Newtonsoft.Json.JsonProperty("channel_cid", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ChannelCid { get; set; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        [Newtonsoft.Json.JsonProperty("created_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Created By is the user who created the thread
        /// </summary>
        [Newtonsoft.Json.JsonProperty("created_by", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public UserIdObjectInternalDTO CreatedBy { get; set; }

        /// <summary>
        /// Custom is the custom data of the thread
        /// </summary>
        [Newtonsoft.Json.JsonProperty("custom", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.Dictionary<string, object> Custom { get; set; } = new System.Collections.Generic.Dictionary<string, object>();

        /// <summary>
        /// Date/time of deletion
        /// </summary>
        [Newtonsoft.Json.JsonProperty("deleted_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// Last Message At is the time of the last message in the thread
        /// </summary>
        [Newtonsoft.Json.JsonProperty("last_message_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? LastMessageAt { get; set; }

        [Newtonsoft.Json.JsonProperty("latest_replies", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<MessageInternalDTO> LatestReplies { get; set; } = new System.Collections.Generic.List<MessageInternalDTO>();

        /// <summary>
        /// Parent Message is the message the thread is replying to
        /// </summary>
        [Newtonsoft.Json.JsonProperty("parent_message", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public MessageInternalDTO ParentMessage { get; set; }

        /// <summary>
        /// Parent Message ID is unique string identifier of the parent message
        /// </summary>
        [Newtonsoft.Json.JsonProperty("parent_message_id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ParentMessageId { get; set; }

        /// <summary>
        /// The number of participants in the thread
        /// </summary>
        [Newtonsoft.Json.JsonProperty("participant_count", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ParticipantCount { get; set; }

        [Newtonsoft.Json.JsonProperty("read", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<ReadInternalDTO> Read { get; set; }

        /// <summary>
        /// The number of replies in the thread
        /// </summary>
        [Newtonsoft.Json.JsonProperty("reply_count", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ReplyCount { get; set; }

        [Newtonsoft.Json.JsonProperty("thread_participants", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<ThreadParticipantInternalDTO> ThreadParticipants { get; set; }

        /// <summary>
        /// Title is the title of the thread
        /// </summary>
        [Newtonsoft.Json.JsonProperty("title", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        [Newtonsoft.Json.JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset UpdatedAt { get; set; }

        private System.Collections.Generic.Dictionary<string, object> _additionalProperties;

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.Dictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

}

