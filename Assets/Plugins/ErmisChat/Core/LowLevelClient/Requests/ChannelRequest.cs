using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    // ErmisTodo: new openAPI spec makes more granular distinction of channel requests like
    // UpdateChannelRequest, ShowChannelRequest, etc. so this class will have to be replaced by more specific types
    public partial class ChannelRequest : RequestObjectBase, ISavableTo<ChannelRequestInternalDTO>
    {
        public string Name { get; set; }
        public string Description { set; get; }
        public int MemberMessageCooldown { set; get; }
        public bool Public { set; get; }
        public List<string> FilterWords { set; get; }
        public string image { set; get; }
        ChannelRequestInternalDTO ISavableTo<ChannelRequestInternalDTO>.SaveToDto() =>
            new ChannelRequestInternalDTO
            {
                Name = Name,
                Description=Description,
                MemberMessageCooldown = MemberMessageCooldown,
                Public = Public,
                FilterWords= FilterWords,
                image = image
            };
    }
}