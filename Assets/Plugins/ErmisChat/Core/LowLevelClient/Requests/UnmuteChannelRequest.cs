﻿using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class UnmuteChannelRequest : ResponseObjectBase, ISavableTo<UnmuteChannelRequestInternalDTO>
    {
        /// <summary>
        /// Channel CIDs to mute (if multiple channels)
        /// </summary>
        public System.Collections.Generic.List<string> ChannelCids { get; set; } = new System.Collections.Generic.List<string>();

        /// <summary>
        /// Duration of mute in milliseconds
        /// </summary>
        public int? Expiration { get; set; }

        UnmuteChannelRequestInternalDTO ISavableTo<UnmuteChannelRequestInternalDTO>.SaveToDto() =>
            new UnmuteChannelRequestInternalDTO
            {
                ChannelCids = ChannelCids,
                Expiration = Expiration,
                AdditionalProperties = AdditionalProperties,
            };
    }
}