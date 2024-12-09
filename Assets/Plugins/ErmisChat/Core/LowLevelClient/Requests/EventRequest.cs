using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Events;

namespace Ermis.Core.LowLevelClient.Requests
{
    internal class EventRequest : RequestObjectBase, ISavableTo<EventRequestInternalDTO>
    {
        public string Type{ get; set; }

        EventRequestInternalDTO ISavableTo<EventRequestInternalDTO>.SaveToDto() =>
            new EventRequestInternalDTO
            {
                Type = Type,
            };
    }
}