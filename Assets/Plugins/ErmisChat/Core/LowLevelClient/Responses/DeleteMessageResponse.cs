using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public class DeleteMessageResponse : ResponseObjectBase, ILoadableFrom<DeleteMessageResponseInternalDTO, DeleteMessageResponse>
    {
        public MessageResponse Message { get; set; }
        public string Duration { get; set; }

        DeleteMessageResponse ILoadableFrom<DeleteMessageResponseInternalDTO, DeleteMessageResponse>.LoadFromDto(DeleteMessageResponseInternalDTO dto)
        {
            Duration = dto.Duration;
            Message = Message.TryLoadFromDto(dto.Message);
            return this;
        }
    }
}