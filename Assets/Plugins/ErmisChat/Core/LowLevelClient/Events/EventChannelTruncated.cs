﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventChannelTruncated : EventBase,
        ILoadableFrom<ChannelTruncatedEventInternalDTO, EventChannelTruncated>
    {
        public Channel Channel { get; set; }

        public string ChannelId { get; set; }

        public string ChannelType { get; set; }
        public Message Message { get; set; }

        public string Cid { get; set; }

        public string Type { get; set; }

        EventChannelTruncated ILoadableFrom<ChannelTruncatedEventInternalDTO, EventChannelTruncated>.LoadFromDto(
            ChannelTruncatedEventInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Message = Message.TryLoadFromDto<MessageInternalDTO, Message>(dto.Message);
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Type = dto.Type;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}