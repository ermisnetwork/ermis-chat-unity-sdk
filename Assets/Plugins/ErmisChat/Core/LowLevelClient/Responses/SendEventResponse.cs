using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class SendEventResponse : ResponseObjectBase, ILoadableFrom<SendEventResponseInternalDTO, SendEventResponse>
    {
        
        public EventResponse Event { get; set; }
        SendEventResponse ILoadableFrom<SendEventResponseInternalDTO, SendEventResponse>.LoadFromDto(SendEventResponseInternalDTO dto)
        {
            Event = Event.TryLoadFromDto(dto.Event);
            return this;
        }
    }
}