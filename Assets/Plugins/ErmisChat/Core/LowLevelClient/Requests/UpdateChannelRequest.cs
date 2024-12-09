using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public  class UpdateChannelRequest : RequestObjectBase, ISavableTo<UpdateChannelRequestInternalDTO>
    {
        
        public ChannelRequest Data { get; set; }

        UpdateChannelRequestInternalDTO ISavableTo<UpdateChannelRequestInternalDTO>.SaveToDto() =>
            new UpdateChannelRequestInternalDTO
            {
                Data = Data.TrySaveToDto(),
            };
    }
}