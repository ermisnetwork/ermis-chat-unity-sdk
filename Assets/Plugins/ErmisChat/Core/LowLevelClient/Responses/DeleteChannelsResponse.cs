﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class DeleteChannelsResponse : ResponseObjectBase, ILoadableFrom<DeleteChannelsResponseInternalDTO, DeleteChannelsResponse>
    {
        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        public System.Collections.Generic.Dictionary<string, DeleteChannelsResult> Result { get; set; }

        public string TaskId { get; set; }

        DeleteChannelsResponse ILoadableFrom<DeleteChannelsResponseInternalDTO, DeleteChannelsResponse>.LoadFromDto(DeleteChannelsResponseInternalDTO dto)
        {
            Duration = dto.Duration;
            Result = Result.TryLoadFromDtoDictionary(dto.Result);
            TaskId = dto.TaskId;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}