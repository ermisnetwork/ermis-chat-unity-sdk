using System;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.Models
{
    public class ChannelMember : ModelBase, ILoadableFrom<ChannelMemberInternalDTO, ChannelMember>, ISavableTo<ChannelMemberInternalDTO>
        //ILoadableFrom<ChannelMemberResponseInternalDTO, ChannelMember>
    {
        
        public bool? Banned { get; set; }

        public bool? Blocked { get; set; }

        /// <summary>
        /// Role of the member in the channel
        /// </summary>
        public string ChannelRole { get; set; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public UserIdObject User { get; set; }

        public string UserId { get; set; }

        ChannelMember ILoadableFrom<ChannelMemberInternalDTO, ChannelMember>.LoadFromDto(ChannelMemberInternalDTO dto)
        {
            Banned = dto.Banned;
            Blocked = dto.Blocked;
            ChannelRole = dto.ChannelRole;
            CreatedAt = dto.CreatedAt;
            UpdatedAt = dto.UpdatedAt;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, UserIdObject>(dto.User);
            UserId = dto.UserId;

            return this;
        }

        //ChannelMember ILoadableFrom<ChannelMemberResponseInternalDTO, ChannelMember>.LoadFromDto(
        //    ChannelMemberResponseInternalDTO dto)
        //{
        //    BanExpires = dto.BanExpires;
        //    Banned = dto.Banned;
        //    ChannelRole = dto.ChannelRole;
        //    CreatedAt = dto.CreatedAt;
        //    DeletedAt = dto.DeletedAt;
        //    InviteAcceptedAt = dto.InviteAcceptedAt;
        //    InviteRejectedAt = dto.InviteRejectedAt;
        //    Invited = dto.Invited;
        //    IsModerator = dto.IsModerator;
        //    ShadowBanned = dto.ShadowBanned;
        //    UpdatedAt = dto.UpdatedAt;
        //    User = User.TryLoadFromDto<UserResponseInternalDTO, User>(dto.User);
        //    UserId = dto.UserId;
        //    AdditionalProperties = dto.AdditionalProperties;
        //    //ErmisTodo: would be safer to update the dictionary instead of overwriting the reference

        //    return this;
        //}

        ChannelMemberInternalDTO ISavableTo<ChannelMemberInternalDTO>.SaveToDto()
        {
            return new ChannelMemberInternalDTO
            {
                Banned = Banned.GetValueOrDefault(),
                ChannelRole = ChannelRole,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                User = User.TrySaveToDto(),
                UserId = UserId,
                Blocked= Blocked.GetValueOrDefault(),
            };
        }
    }
}