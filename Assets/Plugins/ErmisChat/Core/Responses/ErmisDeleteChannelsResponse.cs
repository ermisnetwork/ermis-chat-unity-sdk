using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Responses
{
    //ErmisTodo: replace all ILoadableFrom to IStateLoadableFrom so we can separate the interfaces
    /// <summary>
    /// Response for <see cref="ErmisChatClient.DeleteMultipleChannelsAsync"/>
    /// </summary>
    public sealed class
        ErmisDeleteChannelsResponse : ILoadableFrom<DeleteChannelsResponseInternalDTO, ErmisDeleteChannelsResponse>
    {
        public System.Collections.Generic.Dictionary<string, ErmisDeleteChannelsResult> Result { get; private set; }

        /// <summary>
        /// ID of the channels delete request server task. This can be used to check the status of this operation.
        /// </summary>
        public string TaskId { get; private set; }

        ErmisDeleteChannelsResponse ILoadableFrom<DeleteChannelsResponseInternalDTO, ErmisDeleteChannelsResponse>.
            LoadFromDto(DeleteChannelsResponseInternalDTO dto)
        {
            Result = Result.TryLoadFromDtoDictionary(dto.Result);
            TaskId = dto.TaskId;

            return this;
        }
    }
}