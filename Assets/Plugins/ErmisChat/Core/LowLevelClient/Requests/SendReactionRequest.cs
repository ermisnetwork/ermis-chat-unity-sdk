using System;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class SendReactionRequest : RequestObjectBase, ISavableTo<SendReactionRequestInternalDTO>
    {
        [Obsolete("Has no effect and will be removed in a future release")] //ErmisTODO: remove this in a major release
        public string ID { get; set; }

        /// <summary>
        /// Whether to replace all existing user reactions
        /// </summary>
        public bool? EnforceUnique { get; set; }

        public ReactionRequest Reaction { get; set; }

        /// <summary>
        /// Skips any mobile push notifications
        /// </summary>
        public bool? SkipPush { get; set; }

        SendReactionRequestInternalDTO ISavableTo<SendReactionRequestInternalDTO>.SaveToDto() =>
            new SendReactionRequestInternalDTO
            {
                EnforceUnique = EnforceUnique,
                Reaction = Reaction.TrySaveToDto(),
                SkipPush = SkipPush,
                AdditionalProperties = AdditionalProperties,
            };
    }
}