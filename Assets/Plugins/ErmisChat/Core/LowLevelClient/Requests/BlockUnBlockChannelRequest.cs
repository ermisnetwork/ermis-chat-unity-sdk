using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class BlockUnBlockChannelRequest : ResponseObjectBase, ISavableTo<BlockUnBlockChannelRequestInternalDTO>
    {

        public string Action { get; set; }// "block" | "unblock"

        BlockUnBlockChannelRequestInternalDTO ISavableTo<BlockUnBlockChannelRequestInternalDTO>.SaveToDto() =>
            new BlockUnBlockChannelRequestInternalDTO
            {
                Action = Action,
            };

    }
}