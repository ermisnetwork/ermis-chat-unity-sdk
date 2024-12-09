using System;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class EventResponse : ResponseObjectBase, ILoadableFrom<EventResponseInternalDTO, EventResponse>
    {
        public string Type {  get; set; }
        public string Cid { get; set; }
        public string ChannelId { get; set; }
        public string ChannelType { get; set; }
        public UserIdObject User { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        EventResponse ILoadableFrom<EventResponseInternalDTO, EventResponse>.LoadFromDto(EventResponseInternalDTO dto)
        {
            Type = dto.Type;
            Cid = dto.Cid;
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            User = User.TryLoadFromDto(dto.User);
            CreatedAt = dto.CreatedAt;
            return this;
        }
    }
}