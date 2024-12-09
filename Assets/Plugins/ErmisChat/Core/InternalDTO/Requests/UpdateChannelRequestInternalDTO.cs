
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ermis.Core.InternalDTO.Requests
{
    internal class AddMemberRequestInternalDTO
    {
        [JsonProperty("add_members")]
        public List<string> AddMembers;
    }

    internal class RemoveMemberRequestInternalDTO
    {
        [JsonProperty("remove_members")]
        public List<string> RemoveMembers;
    }

    internal class BanMemberRequestInternalDTO
    {
        [JsonProperty("ban_members")]
        public List<string> BanMembers;
    }

    internal class UnBanMemberRequestInternalDTO
    {
        [JsonProperty("uban_members")]
        public List<string> UnBanMembers;
    }

    internal class PromoteMemberRequestInternalDTO
    {
        [JsonProperty("promote_members")]
        public List<string> PromoteMembers;
    }

    internal class DemoteMemberRequestInternalDTO
    {
        [JsonProperty("demote_members")]
        public List<string> DemoteMembers;
    }

    internal class PromoteOwnerAndLeaveRequestInternalDTO
    {
        [JsonProperty("promote_owner")]
        public string PromoteOwner;
        [JsonProperty("leave")]
        public bool IsLeave;
    }

    internal class UpdateMemberCapabilitiesRequestDTO
    {
        [JsonProperty("user_id")]
        public string Member;
        [JsonProperty("capabilities")]
        public List<string> Capabilities;
    }

    internal class UpdateChannelCapabilitiesRequestDTO
    {
        [JsonProperty("capabilities")]
        public List<string> Capabilities;
    }

    internal class BlockUnBlockChannelRequestInternalDTO
    {
        [JsonProperty("action")]
        public string Action;// "block" | "unblock"
    }

    internal class ChannelMuteRequestInternalDTO
    {
        [JsonProperty("mute")]
        public bool Mute; // truyền false để bật lại thông báo | truyền true thì field duration mới sử dụng đến
        [JsonProperty("duration")]
        public int Duration; // truyền null để tắt hẳn noti | truyền miliseconds để tắt đến khoảng
    }

    internal class ChannelRequestInternalDTO
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("description")]
        public string Description;
        [JsonProperty("member_message_cooldown")]
        public int MemberMessageCooldown;
        [JsonProperty("public")]
        public bool Public;
        [JsonProperty("filter_words")]
        public List<string> FilterWords;
        [JsonProperty("uban_members")]
        public string image;
    }

    internal class UpdateChannelRequestInternalDTO
    {
        [JsonProperty("data")]
        public ChannelRequestInternalDTO Data;
    }
}
