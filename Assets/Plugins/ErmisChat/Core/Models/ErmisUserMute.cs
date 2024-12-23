﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisUserMute : IStateLoadableFrom<UserMuteInternalDTO, ErmisUserMute>
    {
        /// <summary>
        /// Date/time of creation
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// Date/time of mute expiration
        /// </summary>
        public System.DateTimeOffset? Expires { get; private set; }

        /// <summary>
        /// User who's muted
        /// </summary>
        public User Target { get; private set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        public System.DateTimeOffset? UpdatedAt { get; private set; }

        /// <summary>
        /// Owner of channel mute
        /// </summary>
        public User User { get; private set; }

        ErmisUserMute IStateLoadableFrom<UserMuteInternalDTO, ErmisUserMute>.LoadFromDto(UserMuteInternalDTO dto, ICache cache)
        {
            CreatedAt = dto.CreatedAt;
            Expires = dto.Expires;
            Target = Target.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.Target);
            UpdatedAt = dto.UpdatedAt;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);

            return this;
        }
    }
}