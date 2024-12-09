using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.InternalDTO.Requests
{
    public  class PinUnpinRequest: RequestObjectBase, ISavableTo<PinUnpinRequestInternalDTO>
    {
        public string  Message{ get; set; }

        PinUnpinRequestInternalDTO ISavableTo<PinUnpinRequestInternalDTO>.SaveToDto() =>
           new PinUnpinRequestInternalDTO
           {
               Message = Message,
           };
    }
}
