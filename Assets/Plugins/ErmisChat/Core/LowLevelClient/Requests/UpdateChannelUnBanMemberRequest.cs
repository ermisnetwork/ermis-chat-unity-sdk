using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class UpdateChannelUnBanMemberRequest : RequestObjectBase, ISavableTo<UnBanMemberRequestInternalDTO>
    {

        public System.Collections.Generic.List<string> UnBanMembers { get; set; }

        UnBanMemberRequestInternalDTO ISavableTo<UnBanMemberRequestInternalDTO>.SaveToDto() =>
            new UnBanMemberRequestInternalDTO
            {
                UnBanMembers = UnBanMembers,
            };

    }
}