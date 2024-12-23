﻿using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class MuteChannelRequest : ResponseObjectBase, ISavableTo<MuteChannelRequestInternalDTO>
    {
        /// <summary>
        /// Channel CIDs to mute (if multiple channels)
        /// </summary>
        public System.Collections.Generic.List<string> ChannelCids { get; set; } = new System.Collections.Generic.List<string>();

        /// <summary>
        /// Duration of mute in milliseconds
        /// </summary>
        public int? Expiration { get; set; }

        MuteChannelRequestInternalDTO ISavableTo<MuteChannelRequestInternalDTO>.SaveToDto() =>
            new MuteChannelRequestInternalDTO
            {
                ChannelCids = ChannelCids,
                Expiration = Expiration,
                AdditionalProperties = AdditionalProperties,
            };
    }
}