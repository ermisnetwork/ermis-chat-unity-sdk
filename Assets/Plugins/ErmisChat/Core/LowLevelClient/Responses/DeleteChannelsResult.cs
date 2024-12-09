using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class DeleteChannelsResult : ResponseObjectBase, ILoadableFrom<DeleteChannelsResultResponseInternalDTO, DeleteChannelsResult>
    {
        public string Error { get; set; }

        public string Status { get; set; }

        DeleteChannelsResult ILoadableFrom<DeleteChannelsResultResponseInternalDTO, DeleteChannelsResult>.LoadFromDto(DeleteChannelsResultResponseInternalDTO dto)
        {
            Error = dto.Error;
            Status = dto.Status;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}