﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class SearchRequest : RequestObjectBase, ISavableTo<SearchRequestInternalDTO>
    {
        /// <summary>
        /// Channel filter conditions
        /// </summary>
        public System.Collections.Generic.Dictionary<string, object> FilterConditions { get; set; } = new System.Collections.Generic.Dictionary<string, object>();

        /// <summary>
        /// Number of messages to return
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Message filter conditions
        /// </summary>
        public System.Collections.Generic.Dictionary<string, object> MessageFilterConditions { get; set; }

        /// <summary>
        /// Pagination parameter. Cannot be used with non-zero offset.
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// Pagination offset. Cannot be used with sort or next.
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Search phrase
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Sort parameters. Cannot be used with non-zero offset
        /// </summary>
        public System.Collections.Generic.List<SortParamRequest> Sort { get; set; }

        SearchRequestInternalDTO ISavableTo<SearchRequestInternalDTO>.SaveToDto() =>
            new SearchRequestInternalDTO
            {
                FilterConditions = FilterConditions,
                Limit = Limit,
                MessageFilterConditions = MessageFilterConditions,
                Next = Next,
                Offset = Offset,
                Query = Query,
                Sort = Sort.TrySaveToDtoCollection<SortParamRequest, SortParamRequestInternalDTO>(),
                AdditionalProperties = AdditionalProperties,
            };
    }
}