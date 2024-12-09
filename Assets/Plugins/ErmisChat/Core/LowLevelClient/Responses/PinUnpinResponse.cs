using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ermis.Core.LowLevelClient.Responses;
using Ermis.Core.LowLevelClient;
using System.Threading;
using Ermis.Core.Helpers;

namespace Ermis.Core.InternalDTO.Requests
{
    public  class PinUnpinResponse: ResponseObjectBase, ILoadableFrom<PinUnpinResponseInternalDTO, PinUnpinResponse>
    {

        public MessageResponse Message {  get; set; }
        public string Duration { get; set; }

        PinUnpinResponse ILoadableFrom<PinUnpinResponseInternalDTO, PinUnpinResponse>.LoadFromDto(PinUnpinResponseInternalDTO dto)
        {
            Message = Message.TryLoadFromDto(dto.Message);
            Duration = dto.Duration;
            return this;
        }
    }
}
