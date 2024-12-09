﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventChannelDeleted : EventBase,
        ILoadableFrom<ChannelDeletedEventInternalDTO, EventChannelDeleted>
    {
        public Channel Channel { get; set; }

        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public string Team { get; set; }

        public string Type { get; set; }

        EventChannelDeleted ILoadableFrom<ChannelDeletedEventInternalDTO, EventChannelDeleted>.LoadFromDto(
            ChannelDeletedEventInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Team = dto.Team;
            Type = dto.Type;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}