﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class QueryBannedUsersRequest : RequestObjectBase, ISavableTo<QueryBannedUsersRequestInternalDTO>
    {
        public System.DateTimeOffset? CreatedAtAfter { get; set; }

        public System.DateTimeOffset? CreatedAtAfterOrEqual { get; set; }

        public System.DateTimeOffset? CreatedAtBefore { get; set; }

        public System.DateTimeOffset? CreatedAtBeforeOrEqual { get; set; }

        public System.Collections.Generic.Dictionary<string, object> FilterConditions { get; set; }

        public int? Limit { get; set; }

        public int? Offset { get; set; }

        public System.Collections.Generic.List<SortParamRequest> Sort { get; set; }

        QueryBannedUsersRequestInternalDTO ISavableTo<QueryBannedUsersRequestInternalDTO>.SaveToDto()
        {
            return new QueryBannedUsersRequestInternalDTO
            {
                CreatedAtAfter = CreatedAtAfter,
                CreatedAtAfterOrEqual = CreatedAtAfterOrEqual,
                CreatedAtBefore = CreatedAtBefore,
                CreatedAtBeforeOrEqual = CreatedAtBeforeOrEqual,
                FilterConditions = FilterConditions,
                Limit = Limit,
                Offset = Offset,
                Sort = Sort.TrySaveToDtoCollection<SortParamRequest, SortParamRequestInternalDTO>(),
                AdditionalProperties = AdditionalProperties,
            };
        }
    }
}