using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class DeleteChannelsRequest : ResponseObjectBase, ISavableTo<DeleteChannelsRequestInternalDTO>
    {
        /// <summary>
        /// All channels that should be deleted
        /// </summary>
        public System.Collections.Generic.List<string> Cids { get; set; }

        /// <summary>
        /// Specify if channels and all ressources should be hard deleted
        /// </summary>
        public bool? HardDelete { get; set; }

        DeleteChannelsRequestInternalDTO ISavableTo<DeleteChannelsRequestInternalDTO>.SaveToDto() =>
            new DeleteChannelsRequestInternalDTO
            {
                Cids = Cids,
                HardDelete = HardDelete,
                AdditionalProperties = AdditionalProperties,
            };

    }
}