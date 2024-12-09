using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventChannelVisible : EventBase,
        ILoadableFrom<ChannelVisibleEventInternalDTO, EventChannelVisible>
    {
        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        EventChannelVisible ILoadableFrom<ChannelVisibleEventInternalDTO, EventChannelVisible>.LoadFromDto(
            ChannelVisibleEventInternalDTO dto)
        {
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}