using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public class ReactionRemovalResponse : ResponseObjectBase, ILoadableFrom<ReactionRemovalResponseInternalDTO, ReactionRemovalResponse>
    {
        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        public MessageResponse Message { get; set; }

        public Reaction Reaction { get; set; }

        ReactionRemovalResponse ILoadableFrom<ReactionRemovalResponseInternalDTO, ReactionRemovalResponse>.LoadFromDto(ReactionRemovalResponseInternalDTO dto)
        {
            Duration = Duration;
            Message = Message.TryLoadFromDto<MessageResponseInternalDTO, MessageResponse>(dto.Message);
            Reaction = Reaction.TryLoadFromDto(dto.Reaction);
            AdditionalProperties = AdditionalProperties;

            return this;
        }
    }
}