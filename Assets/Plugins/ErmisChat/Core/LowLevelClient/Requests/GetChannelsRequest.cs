using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class GetChannelsRequest : RequestObjectBase, ISavableTo<GetChannelsRequestDto>
    {
        public FilterConditions FilterConditions { get; set; }

        public List<Sort> Sort {  get; set; }

        /// <summary>
        /// Number of messages to limit
        /// </summary>
        public int MessageLimit { get; set; }

        GetChannelsRequestDto ISavableTo<GetChannelsRequestDto>.SaveToDto() =>
            new GetChannelsRequestDto
            {
                FilterConditions = FilterConditions,
                MessageLimit = MessageLimit,
                Sort = Sort,              
            };
    }
}
