using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;
using System;

namespace Ermis.Core.LowLevelClient.Responses
{
    public class SearchPublicChannelResponse : ResponseObjectBase, ILoadableFrom<SearchPublicChannelResponseInternalDTO, SearchPublicChannelResponse>
    {
        public SearchPublicChannelResultResponse SearchResult { get; set; }
        public string Duration{ get; set; }

        SearchPublicChannelResponse ILoadableFrom<SearchPublicChannelResponseInternalDTO, SearchPublicChannelResponse>.LoadFromDto(SearchPublicChannelResponseInternalDTO dto)
        {
            return new SearchPublicChannelResponse
            {
                SearchResult = SearchResult.TryLoadFromDto(dto.SearchResult),
                Duration = dto.Duration,
            };
        }
    }
    public class SearchPublicChannelResultResponse: ResponseObjectBase, ILoadableFrom<SearchPublicChannelResultResponseInternalDTO, SearchPublicChannelResultResponse>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Total { get; set; }
        public List<ChannelSearchResultResponse> Channel { get; set; }

        SearchPublicChannelResultResponse ILoadableFrom<SearchPublicChannelResultResponseInternalDTO, SearchPublicChannelResultResponse>.LoadFromDto(SearchPublicChannelResultResponseInternalDTO dto)
        {
            return new SearchPublicChannelResultResponse
            {
                Limit = dto.Limit,
                Offset = dto.Offset,
                Total = dto.Total,
                Channel = Channel.TryLoadFromDtoCollection(dto.Channels)
            };

        }
    }

    public class ChannelSearchResultResponse : ResponseObjectBase, ILoadableFrom<ChannelSearchResultResponseInternalDTO, ChannelSearchResultResponse>
    {
        public string Index { get; set; }
        public string Cid { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        ChannelSearchResultResponse ILoadableFrom<ChannelSearchResultResponseInternalDTO, ChannelSearchResultResponse>.LoadFromDto(ChannelSearchResultResponseInternalDTO dto)
        {
            return new ChannelSearchResultResponse
            {
                Index = dto.Index,
                Cid = dto.Cid,
                Type = dto.Type,
                Name = dto.Name,
                Image = dto.Image,
                Description = dto.Description,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy,
            };

        }
    }
}
