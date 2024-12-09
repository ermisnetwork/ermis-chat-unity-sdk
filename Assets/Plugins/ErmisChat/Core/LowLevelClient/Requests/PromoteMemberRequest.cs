using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class PromoteMemberRequest : RequestObjectBase, ISavableTo<PromoteMemberRequestInternalDTO>
    {

        public System.Collections.Generic.List<string> PromoteMembers { get; set; }

        PromoteMemberRequestInternalDTO ISavableTo<PromoteMemberRequestInternalDTO>.SaveToDto() =>
            new PromoteMemberRequestInternalDTO
            {
                PromoteMembers = PromoteMembers,
            };

    }
}