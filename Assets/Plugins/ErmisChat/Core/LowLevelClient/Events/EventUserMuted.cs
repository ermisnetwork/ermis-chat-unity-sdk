using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public partial class EventUserMuted : EventBase, ILoadableFrom<UserMutedEventInternalDTO, EventUserMuted>
    {
        public string TargetUser { get; set; }

        public System.Collections.Generic.List<string> TargetUsers { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        EventUserMuted ILoadableFrom<UserMutedEventInternalDTO, EventUserMuted>.LoadFromDto(
            UserMutedEventInternalDTO dto)
        {
            CreatedAt = dto.CreatedAt;
            TargetUser = dto.TargetUser;
            TargetUsers = dto.TargetUsers;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}