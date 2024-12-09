using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class SearchPublicChannelRequest : RequestObjectBase, ISavableTo<SearchPublicChannelRequestInternalDTO>
    {

        
        public string ProjectId {  get; set; }
        public string SearchTerm { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }

        SearchPublicChannelRequestInternalDTO ISavableTo<SearchPublicChannelRequestInternalDTO>.SaveToDto() =>
            new SearchPublicChannelRequestInternalDTO
            {
                ProjectId = ProjectId,
                SearchTerm = SearchTerm,
                Limit = Limit,
                Offset = Offset
            };

    }
}