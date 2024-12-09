using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventChannelHidden : EventBase,
        ILoadableFrom<ChannelHiddenEventInternalDTO, EventChannelHidden>
    {
        public Channel Channel { get; set; }

        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public bool? ClearHistory { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        EventChannelHidden ILoadableFrom<ChannelHiddenEventInternalDTO, EventChannelHidden>.LoadFromDto(
            ChannelHiddenEventInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            ClearHistory = dto.ClearHistory;
            CreatedAt = dto.CreatedAt;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}