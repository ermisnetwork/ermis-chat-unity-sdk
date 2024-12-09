using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public partial class EventMemberUpdated : EventBase, ILoadableFrom<MemberUpdatedEventInternalDTO, EventMemberUpdated>
    {
        public string ChannelId { get; set; }

        public string ChannelType { get; set; }

        public string Cid { get; set; }

        public ChannelMember Member { get; set; }

        public string Team { get; set; }

        public string Type { get; set; }

        public User User { get; set; }

        EventMemberUpdated ILoadableFrom<MemberUpdatedEventInternalDTO, EventMemberUpdated>.LoadFromDto(MemberUpdatedEventInternalDTO dto)
        {
            ChannelId = dto.ChannelId;
            ChannelType = dto.ChannelType;
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Member = Member.TryLoadFromDto<ChannelMemberInternalDTO, ChannelMember>(dto.Member);
            Team = dto.Team;
            Type = dto.Type;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}