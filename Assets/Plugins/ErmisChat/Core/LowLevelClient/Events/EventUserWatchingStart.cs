using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventUserWatchingStart : EventBase,
        ILoadableFrom<UserWatchingStartEventInternalDTO, EventUserWatchingStart>
    {
        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public string Team { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        public int? WatcherCount { get; set; }

        EventUserWatchingStart ILoadableFrom<UserWatchingStartEventInternalDTO, EventUserWatchingStart>.LoadFromDto(
            UserWatchingStartEventInternalDTO dto)
        {
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Team = dto.Team;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            WatcherCount = dto.WatcherCount;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}