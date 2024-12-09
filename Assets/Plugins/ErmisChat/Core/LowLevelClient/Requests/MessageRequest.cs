﻿using System;
using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    /// <summary>
    /// Represents any chat message
    /// </summary>
    public class MessageRequest : RequestObjectBase, ISavableTo<MessageRequestInternalDTO>
    {
        /// <summary>
        /// Array of message attachments
        /// </summary>
        public List<AttachmentRequest> Attachments { get; set; }

        /// <summary>
        /// Channel unique identifier in <type>:<id> format
        /// </summary>
        [Obsolete("Has no effect and will be removed in a future release")] //ErmisTodo: remove this in a major release
        public string Cid { get; set; }

        /// <summary>
        /// Contains HTML markup of the message. Can only be set when using server-side API
        /// </summary>
        [Obsolete("Has no effect and will be removed in a future release")] //ErmisTodo: remove this in a major release
        public string Html { get; set; } //ErmisTodo: mark as obsolete, this is most probably server-side only

        /// <summary>
        /// Message ID is unique string identifier of the message
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// List of mentioned users
        /// </summary>
        public List<string> MentionedUsers { get; set; }

        /// <summary>
        /// Should be empty if `text` is provided. Can only be set when using server-side API
        /// </summary>
        public string Mml { get; set; }

        /// <summary>
        /// ID of parent message (thread)
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Date when pinned message expires
        /// </summary>
        public DateTimeOffset? PinExpires { get; set; } //ErmisTodo: check if this has any effect

        /// <summary>
        /// Whether message is pinned or not
        /// </summary>
        public bool? Pinned { get; set; } //ErmisTodo: check if this has any effect

        /// <summary>
        /// Date when message got pinned
        /// </summary>
        public DateTimeOffset? PinnedAt { get; set; } //ErmisTodo: check if this has any effect

        [Obsolete("Has no effect and will be removed in a future release")]
        public string PinnedBy { get; set; } //ErmisTodo: check if this has any effect

        public string QuotedMessageId { get; set; }

        /// <summary>
        /// An object containing scores of reactions of each type. Key: reaction type (string), value: total score of reactions (int)
        /// </summary>
        [Obsolete("Has no effect and will be removed in a future release")]
        public Dictionary<string, int> ReactionScores { get; set; } //ErmisTodo: remove this in a major release

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

        [Obsolete("Has no effect and will be removed in a future release")]
        public UserObjectRequest User { get; set; } //ErmisTodo: remove this in a major release

        [Obsolete("Has no effect and will be removed in a future release")]
        public string UserId { get; set; } //ErmisTodo: remove this in a major release

        MessageRequestInternalDTO ISavableTo<MessageRequestInternalDTO>.SaveToDto() =>
            new MessageRequestInternalDTO
            {
                Attachments = Attachments?.TrySaveToDtoCollection<AttachmentRequest, AttachmentRequestInternalDTO>(),
                //Html = Html,
                Id = Id,
                MentionedUsers = MentionedUsers,
                ParentId = ParentId,
                Pinned = Pinned,
                QuotedMessageId = QuotedMessageId,
                Text = Text,
            };
    }
}