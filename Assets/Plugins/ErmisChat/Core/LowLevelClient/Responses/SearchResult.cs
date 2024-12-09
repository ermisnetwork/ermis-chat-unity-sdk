using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class SearchResult : ResponseObjectBase, ILoadableFrom<SearchResultInternalDTO, SearchResult>
    {
        /// <summary>
        /// Found message
        /// </summary>
        public Message Message { get; set; }

        SearchResult ILoadableFrom<SearchResultInternalDTO, SearchResult>.LoadFromDto(SearchResultInternalDTO dto)
        {
            Message = Message.TryLoadFromDto<SearchResultMessageInternalDTO, Message>(dto.Message);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}