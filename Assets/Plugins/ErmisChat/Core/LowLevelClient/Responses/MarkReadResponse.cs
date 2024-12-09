using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Events;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class MarkReadResponse : ResponseObjectBase, ILoadableFrom<MarkReadResponseInternalDTO, MarkReadResponse>
    {
        
        public string Duration { get; set; }
        public EventResponse Event { get; set; }

        MarkReadResponse ILoadableFrom<MarkReadResponseInternalDTO, MarkReadResponse>.LoadFromDto(MarkReadResponseInternalDTO dto)
        {
            Duration = dto.Duration;
            Event = Event.TryLoadFromDto(dto.Event);

            return this;
        }
    }
}