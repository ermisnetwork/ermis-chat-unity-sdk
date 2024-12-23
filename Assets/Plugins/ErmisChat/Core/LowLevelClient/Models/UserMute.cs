﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;

namespace Ermis.Core.LowLevelClient.Models
{
    public class UserMute : ModelBase, ILoadableFrom<UserMuteInternalDTO, UserMute>
    {
        /// <summary>
        /// Date/time of creation
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Date/time of mute expiration
        /// </summary>
        public System.DateTimeOffset? Expires { get; set; }

        /// <summary>
        /// User who's muted
        /// </summary>
        public User Target { get; set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        public System.DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Owner of channel mute
        /// </summary>
        public User User { get; set; }

        UserMute ILoadableFrom<UserMuteInternalDTO, UserMute>.LoadFromDto(UserMuteInternalDTO dto)
        {
            CreatedAt = dto.CreatedAt;
            Expires = dto.Expires;
            Target = Target.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.Target);
            UpdatedAt = dto.UpdatedAt;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}