using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using System;
namespace Ermis.Core.LowLevelClient.Models
{
    public class Channel : ModelBase, ILoadableFrom<ChannelResponseInternalDTO, Channel>
    {
        public string Id {  get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Public { get; set; }
        public string Cid { get; set; }
        public UserIdObject CreatedBy { get; set; }
        public int MemberCount { get; set; }
        public List<ChannelMember> Members { get; set; }
        public List<string> MemberCapabilities { get; set; }
        public List<string> FilterWords { get; set; }
        public DateTimeOffset LastMessageAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        Channel ILoadableFrom<ChannelResponseInternalDTO, Channel>.LoadFromDto(ChannelResponseInternalDTO dto)
        {
            Id = dto.Id;
            Type = dto.Type;
            Name = dto.Name;
            Image = dto.Image;
            Description = dto.Description;
            CreatedAt = dto.CreatedAt;
            Public = dto.Public;
            Cid = dto.Cid;
            MemberCount = MemberCount;
            Members = Members.TryLoadFromDtoCollection(dto.Members);
            MemberCapabilities= dto.MemberCapabilities;
            FilterWords = dto.FilterWords;
            LastMessageAt = dto.LastMessageAt;
            CreatedAt = dto.CreatedAt;
            UpdatedAt = dto.UpdatedAt;
            return this;
        }
    }
}