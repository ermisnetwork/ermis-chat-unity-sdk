using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class RemoveMemberRequest : RequestObjectBase, ISavableTo<RemoveMemberRequestInternalDTO>
    {
        
        public System.Collections.Generic.List<string> RemoveMembers { get; set; }

        RemoveMemberRequestInternalDTO ISavableTo<RemoveMemberRequestInternalDTO>.SaveToDto() =>
            new RemoveMemberRequestInternalDTO
            {
                RemoveMembers = RemoveMembers,
            };

    }
}