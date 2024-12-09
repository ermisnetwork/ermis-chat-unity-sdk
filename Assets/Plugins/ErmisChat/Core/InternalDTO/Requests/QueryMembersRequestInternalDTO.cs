﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;

namespace Ermis.Core.InternalDTO.Requests
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    internal partial class QueryMembersRequestInternalDTO
    {
        [Newtonsoft.Json.JsonProperty("created_at_after", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? CreatedAtAfter { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at_after_or_equal", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? CreatedAtAfterOrEqual { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at_before", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? CreatedAtBefore { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at_before_or_equal", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? CreatedAtBeforeOrEqual { get; set; }

        /// <summary>
        /// Filter to apply to members
        /// </summary>
        [Newtonsoft.Json.JsonProperty("filter_conditions", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.Dictionary<string, object> FilterConditions { get; set; } = new System.Collections.Generic.Dictionary<string, object>();

        /// <summary>
        /// Channel ID to interact with
        /// </summary>
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// Number of records to return
        /// </summary>
        [Newtonsoft.Json.JsonProperty("limit", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        /// <summary>
        /// List of members to search in distinct channels
        /// </summary>
        [Newtonsoft.Json.JsonProperty("members", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<ChannelMemberInternalDTO> Members { get; set; }

        /// <summary>
        /// Number of records to offset
        /// </summary>
        [Newtonsoft.Json.JsonProperty("offset", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        /// <summary>
        /// Array of sort parameters
        /// </summary>
        [Newtonsoft.Json.JsonProperty("sort", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<SortParamRequestInternalDTO> Sort { get; set; }

        /// <summary>
        /// Channel type to interact with
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Type { get; set; }


        [Newtonsoft.Json.JsonProperty("user_id_gt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string UserIdGt { get; set; }

        [Newtonsoft.Json.JsonProperty("user_id_gte", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string UserIdGte { get; set; }

        [Newtonsoft.Json.JsonProperty("user_id_lt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string UserIdLt { get; set; }

        [Newtonsoft.Json.JsonProperty("user_id_lte", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string UserIdLte { get; set; }
        private System.Collections.Generic.Dictionary<string, object> _additionalProperties;

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.Dictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

}
