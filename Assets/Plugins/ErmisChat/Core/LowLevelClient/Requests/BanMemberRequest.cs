using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class BanMemberRequest : RequestObjectBase, ISavableTo<BanMemberRequestInternalDTO>
    {

        public System.Collections.Generic.List<string> BanMembers { get; set; }

        BanMemberRequestInternalDTO ISavableTo<BanMemberRequestInternalDTO>.SaveToDto() =>
            new BanMemberRequestInternalDTO
            {
                BanMembers = BanMembers,
            };

    }
}