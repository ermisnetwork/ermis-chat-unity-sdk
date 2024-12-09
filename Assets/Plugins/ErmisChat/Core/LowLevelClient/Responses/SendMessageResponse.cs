using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public class SendMessageResponse : ResponseObjectBase, ILoadableFrom<SendMessageResponseInternalDTO, SendMessageResponse>
    {
        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        public MessageResponse Message { get; set; }

        SendMessageResponse ILoadableFrom<SendMessageResponseInternalDTO, SendMessageResponse>.LoadFromDto(SendMessageResponseInternalDTO dto)
        {
            Duration = Duration;
            Message = Message.TryLoadFromDto(dto.Message);
            return this;
        }
    }
}