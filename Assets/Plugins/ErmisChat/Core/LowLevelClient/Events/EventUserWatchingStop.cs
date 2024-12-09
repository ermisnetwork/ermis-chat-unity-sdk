using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public sealed class EventUserWatchingStop : EventBase,
        ILoadableFrom<UserWatchingStopEventInternalDTO, EventUserWatchingStop>
    {
        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        public int? WatcherCount { get; set; }

        EventUserWatchingStop ILoadableFrom<UserWatchingStopEventInternalDTO, EventUserWatchingStop>.LoadFromDto(
            UserWatchingStopEventInternalDTO dto)
        {
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            WatcherCount = dto.WatcherCount;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}