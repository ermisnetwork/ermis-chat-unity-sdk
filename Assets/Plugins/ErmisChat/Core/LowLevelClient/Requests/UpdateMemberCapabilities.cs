using System.Collections.Generic;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class UpdateMemberCapabilities : RequestObjectBase, ISavableTo<UpdateMemberCapabilitiesRequestDTO>
    {
        public string Member { get; set; }
        public System.Collections.Generic.List<string> Capabilities { get; set; }

        UpdateMemberCapabilitiesRequestDTO ISavableTo<UpdateMemberCapabilitiesRequestDTO>.SaveToDto() =>
            new UpdateMemberCapabilitiesRequestDTO
            {
                Member = Member,
                Capabilities= Capabilities
            };

    }
}