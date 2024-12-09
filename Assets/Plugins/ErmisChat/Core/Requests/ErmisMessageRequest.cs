using System;
using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient;
using Ermis.Core.State;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.Requests
{
    public class ErmisMessageRequest : ISavableTo<MessageRequestInternalDTO>
    {
        /// <summary>
        /// Array of message attachments
        /// </summary>
        public List<ErmisAttachmentRequest> Attachments { get; set; }

        /// <summary>
        /// Message ID is unique string identifier of the message
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// List of mentioned users
        /// </summary>
        public List<IErmisUser> MentionedUsers { get; set; }

        /// <summary>
        /// ID of parent message (thread)
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Date when pinned message expires
        /// </summary>
        public DateTimeOffset? PinExpires { get; set; }

        /// <summary>
        /// Whether message is pinned or not
        /// </summary>
        public bool? Pinned { get; set; }

        /// <summary>
        /// Date when message got pinned
        /// </summary>
        public DateTimeOffset? PinnedAt { get; set; }

        /// <summary>
        /// Contains user who pinned the message
        /// </summary>
        [Obsolete("Has no effect and will be removed in a future release")]
        public IErmisUser PinnedBy { get; set; } //ErmisTodo: mark as obsolete, this is most probably server-side only

        public IErmisMessage QuotedMessage { get; set; }

        /// <summary>
        /// Whether thread reply should be shown in the channel as well
        /// </summary>
        public bool? ShowInChannel { get; set; }

        /// <summary>
        /// Whether message is silent or not
        /// </summary>
        public bool? Silent { get; set; }

        /// <summary>
        /// Text of the message. Should be empty if `mml` is provided
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Add or update custom data associated with this message. This will be accessible through <see cref="IErmisMessage.CustomData"/>
        /// </summary>
        public ErmisCustomDataRequest CustomData { get; set; }

        MessageRequestInternalDTO ISavableTo<MessageRequestInternalDTO>.SaveToDto()
            => new MessageRequestInternalDTO
            {
                Attachments
                    = Attachments?.TrySaveToDtoCollection<ErmisAttachmentRequest, AttachmentRequestInternalDTO>(),
                // Cid = Cid, Purposely ignored because it has no effect and endpoint already contains channel type&id
                //Html = Html, Marked in DTO as server-side only
                Id = Id,
                MentionedUsers = MentionedUsers?.ToUserIdsListOrNull(),
                ParentId = ParentId,
                Pinned = Pinned,
                QuotedMessageId = QuotedMessage?.Id,
                Text = Text,
            };
    }
}