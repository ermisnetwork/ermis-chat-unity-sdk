using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class SearchChannelMessagesRequest : RequestObjectBase, ISavableTo<SearchChannelMessagesRequestInternalDTO>
    {
        public string CID {  get; set; }
        public string SearchTerm { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }

        SearchChannelMessagesRequestInternalDTO ISavableTo<SearchChannelMessagesRequestInternalDTO>.SaveToDto()
        {
            return new SearchChannelMessagesRequestInternalDTO
            {
                CID = CID,
                SearchTerm = SearchTerm,
                Limit = Limit,
                Offset= Offset
            };
        }
    }
}
