﻿using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class ShowChannelResponse : ResponseObjectBase,
        ILoadableFrom<ShowChannelResponseInternalDTO, ShowChannelResponse>
    {
        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        ShowChannelResponse ILoadableFrom<ShowChannelResponseInternalDTO, ShowChannelResponse>.LoadFromDto(ShowChannelResponseInternalDTO dto)
        {
            Duration = dto.Duration;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}