using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class AddMemberRequest : RequestObjectBase, ISavableTo<AddMemberRequestInternalDTO>
    {

        public System.Collections.Generic.List<string> AddMembers { get; set; }

        AddMemberRequestInternalDTO ISavableTo<AddMemberRequestInternalDTO>.SaveToDto() =>
            new AddMemberRequestInternalDTO
            {
                AddMembers = AddMembers,
            };

    }
}