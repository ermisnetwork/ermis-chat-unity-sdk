using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public class UpdateMessageResponse : ResponseObjectBase, ILoadableFrom<UpdateMessageResponseInternalDTO, UpdateMessageResponse>
    {
        public MessageResponse Message { get; set; }
        public string Duration { get; set; }

        UpdateMessageResponse ILoadableFrom<UpdateMessageResponseInternalDTO, UpdateMessageResponse>.LoadFromDto(UpdateMessageResponseInternalDTO dto)
        {
            Message = Message.TryLoadFromDto(dto.Message);
            Duration = dto.Duration;
            return this;
        }
    }
}