using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.State;

namespace Ermis.Core.LowLevelClient.Responses
{
    public class MessageResponse : ResponseObjectBase, ILoadableFrom<MessageResponseInternalDTO, MessageResponse>
    {
        public string Id {  get; set; }
        public string Text { get; set; }
        public List<OldTextResponse> OldTexts { get; set; }
        public string Type { get; set; }
        public string Cid { get; set; }
        public UserIdObject User { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<AttachmentResponse> Attachments { get; set; }
        public List<Reaction> LatestReactions { get; set; }
        public Dictionary<string, int> ReactionCounts { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        MessageResponse ILoadableFrom<MessageResponseInternalDTO, MessageResponse>.LoadFromDto(MessageResponseInternalDTO dto)
        {
            Id = dto.Id;
            Text = dto.Text;
            OldTexts = OldTexts.TryLoadFromDtoCollection(dto.OldTexts);
            Type = dto.Type;
            Cid = dto.Cid;
            User = User.TryLoadFromDto(dto.User);
            CreatedAt = dto.CreatedAt;
            Attachments = Attachments.TryLoadFromDtoCollection(dto.Attachments);
            LatestReactions = LatestReactions.TryLoadFromDtoCollection(dto.LatestReactions);
            ReactionCounts = dto.ReactionCounts;
            UpdatedAt= dto.UpdatedAt;
            return this;
        }
    }

    public class OldTextResponse : ResponseObjectBase, ILoadableFrom<OldTextResponseInternalDTO, OldTextResponse>
    {
        public string Text { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        OldTextResponse ILoadableFrom<OldTextResponseInternalDTO, OldTextResponse>.LoadFromDto(OldTextResponseInternalDTO dto)
        {
            Text = dto.Text;
            CreatedAt = dto.CreatedAt;
            return this;
        }
    }

    public class AttachmentResponse : ResponseObjectBase, ILoadableFrom<AttachmentResponseInternalDTO, AttachmentResponse>
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public long FileSize { get; set; }
        public string MimeType { get; set; }
        public string AssetUrl { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbUrl { get; set; }

        AttachmentResponse ILoadableFrom<AttachmentResponseInternalDTO, AttachmentResponse>.LoadFromDto(AttachmentResponseInternalDTO dto)
        {
            Type = dto.Type;
            Title = dto.Title;
            FileSize = dto.FileSize;
            MimeType = dto.MimeType;
            AssetUrl = dto.AssetUrl;
            ImageUrl = dto.ImageUrl;
            ThumbUrl = dto.ThumbUrl;
            return this;
        }
    }
}