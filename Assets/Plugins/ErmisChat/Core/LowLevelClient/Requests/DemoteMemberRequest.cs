using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class DemoteMemberRequest : RequestObjectBase, ISavableTo<DemoteMemberRequestInternalDTO>
    {

        public System.Collections.Generic.List<string> DemoteMembers { get; set; }

        DemoteMemberRequestInternalDTO ISavableTo<DemoteMemberRequestInternalDTO>.SaveToDto() =>
            new DemoteMemberRequestInternalDTO
            {
                DemoteMembers = DemoteMembers,
            };

    }
}