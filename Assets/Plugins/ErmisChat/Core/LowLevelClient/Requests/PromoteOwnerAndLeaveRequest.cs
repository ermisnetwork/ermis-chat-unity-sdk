using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public  class PromoteOwnerAndLeaveRequest : RequestObjectBase, ISavableTo<PromoteOwnerAndLeaveRequestInternalDTO>
    {

        public string PromoteOwner { get; set; }
        public bool IsLeave { get; set; }
        PromoteOwnerAndLeaveRequestInternalDTO ISavableTo<PromoteOwnerAndLeaveRequestInternalDTO>.SaveToDto() =>
            new PromoteOwnerAndLeaveRequestInternalDTO
            {
                PromoteOwner = PromoteOwner,
                IsLeave=IsLeave,
            };

    }
}