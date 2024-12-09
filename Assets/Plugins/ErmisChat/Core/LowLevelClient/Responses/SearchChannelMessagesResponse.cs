using System.Collections.Generic;
using System.Security.Cryptography;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Responses
{
    public class SearchChannelMessagesResponse : ResponseObjectBase, ILoadableFrom<SearchChannelMessagesResponseInternalDTO, SearchChannelMessagesResponse>
    {
        public SearchMessagesResultResponse SearchResult { get; set; }
        public string Duration { get; set; }

        SearchChannelMessagesResponse ILoadableFrom<SearchChannelMessagesResponseInternalDTO, SearchChannelMessagesResponse>.LoadFromDto(SearchChannelMessagesResponseInternalDTO dto)
        {
            return new SearchChannelMessagesResponse
            {
                SearchResult = SearchResult.TryLoadFromDto(dto.SearchResult),
                Duration = dto.Duration,
            };
        }
    }
    public class SearchMessagesResultResponse : ResponseObjectBase, ILoadableFrom<SearchMessagesResultResponseInternalDTO, SearchMessagesResultResponse>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Total { get; set; }
        public List<MessageResultResponse> Messages { get; set; }

        SearchMessagesResultResponse ILoadableFrom<SearchMessagesResultResponseInternalDTO, SearchMessagesResultResponse>.LoadFromDto(SearchMessagesResultResponseInternalDTO dto)
        {
            return new SearchMessagesResultResponse
            {
                Limit = dto.Limit,
                Offset = dto.Offset,
                Total = dto.Total,
                Messages = Messages.TryLoadFromDtoCollection(dto.Messages)
            };

        }
    }
    public class MessageResultResponse : ResponseObjectBase, ILoadableFrom<MessageResultResponseInternalDTO, MessageResultResponse>
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public string CreatedAt { get; set; }

        MessageResultResponse ILoadableFrom<MessageResultResponseInternalDTO, MessageResultResponse>.LoadFromDto(MessageResultResponseInternalDTO dto)
        {
            return new MessageResultResponse
            {
                Id = dto.Id,
                Text = dto.Text,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt
            };
        }
    }
}
