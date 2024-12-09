using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.Models
{
    public partial class ErmisPendingMessage : ModelBase, IStateLoadableFrom<PendingMessageInternalDTO, ErmisPendingMessage>
    {
        /// <summary>
        /// The message
        /// </summary>
        public IErmisMessage Message { get; private set; }

        /// <summary>
        /// Additional data attached to the pending message. This data is discarded once the pending message is committed.
        /// </summary>
        public System.Collections.Generic.Dictionary<string, string> Metadata { get; private set; }

        ErmisPendingMessage IStateLoadableFrom<PendingMessageInternalDTO, ErmisPendingMessage>.LoadFromDto(PendingMessageInternalDTO dto, ICache cache)
        {
            Message = cache.TryCreateOrUpdate(dto.Message);
            Metadata = dto.Metadata;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}