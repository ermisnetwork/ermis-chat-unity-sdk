using System;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Models
{
    /// <summary>
    /// Represents user reaction to a message
    /// </summary>
    public class Reaction : ResponseObjectBase, ILoadableFrom<ReactionResponseInternalDTO, Reaction>
        //ILoadableFrom<ReactionResponseInternalDTO, Reaction>
    {
        public string MessageId {  get; set; }
        public string UserId { get; set; }
        public UserIdObject User { get; set; }
        public string Type { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        Reaction ILoadableFrom<ReactionResponseInternalDTO, Reaction>.LoadFromDto(ReactionResponseInternalDTO dto)
        {
            CreatedAt = dto.CreatedAt;
            MessageId = dto.MessageId;
            Type = dto.Type;
            UpdatedAt = dto.UpdatedAt;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, UserIdObject>(dto.User);
            UserId = dto.UserId;

            return this;
        }
    }
}