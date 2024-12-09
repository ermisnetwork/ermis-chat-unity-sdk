using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class SyncRequest : RequestObjectBase, ISavableTo<SyncRequestInternalDTO>
    {
        public System.Collections.Generic.List<string> ChannelCids { get; set; }
        public System.DateTimeOffset LastSyncAt { get; set; }
        public bool? Watch { get; set; }

        public bool? WithInaccessibleCids { get; set; }

        SyncRequestInternalDTO ISavableTo<SyncRequestInternalDTO>.SaveToDto() =>
            new SyncRequestInternalDTO
            {
                ChannelCids = ChannelCids,
                LastSyncAt = LastSyncAt,
                Watch = Watch,
                WithInaccessibleCids = WithInaccessibleCids,
            };
    }
}