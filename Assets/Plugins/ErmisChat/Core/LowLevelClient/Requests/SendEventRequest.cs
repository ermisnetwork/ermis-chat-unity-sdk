using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Events;

namespace Ermis.Core.LowLevelClient.Requests
{
    internal class SendEvent: RequestObjectBase, ISavableTo<SendEventRequestInternalDTO>
    {
        public EventRequest Event { get; set; }

        SendEventRequestInternalDTO ISavableTo<SendEventRequestInternalDTO>.SaveToDto() =>
            new SendEventRequestInternalDTO
            {
                Event = Event.TrySaveToDto()
            };
    }
}