using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public class ReactionResponse : ResponseObjectBase, ILoadableFrom<SendReactionResponseInternalDTO, ReactionResponse>
    {
        public MessageResponse Message { get; set; }

        public Reaction Reaction { get; set; }

        ReactionResponse ILoadableFrom<SendReactionResponseInternalDTO, ReactionResponse>.LoadFromDto(SendReactionResponseInternalDTO dto)
        {
            Message = Message.TryLoadFromDto<MessageResponseInternalDTO, MessageResponse>(dto.Message);
            Reaction = Reaction.TryLoadFromDto<ReactionResponseInternalDTO, Reaction>(dto.Reaction);

            return this;
        }
    }
}