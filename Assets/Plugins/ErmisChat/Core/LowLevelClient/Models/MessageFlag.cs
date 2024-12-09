﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.Models
{
    public partial class MessageFlag : ModelBase, ILoadableFrom<MessageFlagResponseInternalDTO, MessageFlag>
    {
        public System.DateTimeOffset? ApprovedAt { get; set; }

        public System.DateTimeOffset? CreatedAt { get; set; }

        public bool? CreatedByAutomod { get; set; }

        public Message Message { get; set; }

        public MessageModerationResult ModerationResult { get; set; }

        public System.DateTimeOffset? RejectedAt { get; set; }

        public System.DateTimeOffset? ReviewedAt { get; set; }

        public User ReviewedBy { get; set; }

        public System.DateTimeOffset? UpdatedAt { get; set; }

        public User User { get; set; }

        MessageFlag ILoadableFrom<MessageFlagResponseInternalDTO, MessageFlag>.LoadFromDto(MessageFlagResponseInternalDTO dto)
        {
            ApprovedAt = dto.ApprovedAt;
            CreatedAt = dto.CreatedAt;
            CreatedByAutomod = dto.CreatedByAutomod;
            Message = Message.TryLoadFromDto<MessageInternalDTO, Message>(dto.Message);
            ModerationResult = ModerationResult.TryLoadFromDto(dto.ModerationResult);
            RejectedAt = dto.RejectedAt;
            ReviewedAt = dto.ReviewedAt;
            ReviewedBy = ReviewedBy.TryLoadFromDto<UserResponseInternalDTO, User>(dto.ReviewedBy);
            UpdatedAt = dto.UpdatedAt;
            User = User.TryLoadFromDto<UserResponseInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}