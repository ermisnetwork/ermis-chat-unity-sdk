using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Responses
{
    public sealed class ErmisDeleteChannelsResult : ILoadableFrom<DeleteChannelsResultResponseInternalDTO, ErmisDeleteChannelsResult>
    {
        public string Error { get; private set; }

        public string Status { get; private set; }

        ErmisDeleteChannelsResult ILoadableFrom<DeleteChannelsResultResponseInternalDTO, ErmisDeleteChannelsResult>.LoadFromDto(DeleteChannelsResultResponseInternalDTO dto)
        {
            Error = dto.Error;
            Status = dto.Status;

            return this;
        }
    }
}