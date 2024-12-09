using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.State;
using Ermis.Core.State.Caches;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.Models
{
    /// <summary>
    /// Represents user reaction to a message
    /// </summary>
    public class ErmisReaction : IStateLoadableFrom<ReactionResponseInternalDTO, ErmisReaction>
    {
        /// <summary>
        /// Date/time of creation
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// ID of a message user reacted to
        /// </summary>
        public string MessageId { get; private set; }

        /// <summary>
        /// Reaction score. If not specified reaction has score of 1
        /// </summary>
        public int? Score { get; private set; }

        /// <summary>
        /// The type of reaction (e.g. 'like', 'laugh', 'wow')
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        public System.DateTimeOffset? UpdatedAt { get; private set; }

        /// <summary>
        /// User who reacted to a message
        /// </summary>
        public IErmisUser User { get; private set; }

        /// <summary>
        /// ID of a user who reacted to a message
        /// </summary>
        public string UserId { get; private set; }

        ErmisReaction IStateLoadableFrom<ReactionResponseInternalDTO, ErmisReaction>.LoadFromDto(ReactionResponseInternalDTO dto, ICache cache)
        {
            CreatedAt = dto.CreatedAt;
            MessageId = dto.MessageId;
            Type = dto.Type;
            UpdatedAt = dto.UpdatedAt;
            User = cache.TryCreateOrUpdate(dto.User);
            UserId = dto.UserId;

            return this;
        }
       
    }
}