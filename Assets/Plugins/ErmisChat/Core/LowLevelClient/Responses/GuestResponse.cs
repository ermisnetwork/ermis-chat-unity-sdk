﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class GuestResponse : ResponseObjectBase, ILoadableFrom<CreateGuestResponseInternalDTO, GuestResponse>
    {
        /// <summary>
        /// Authentication token to use for guest user
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Created user object
        /// </summary>
        public User User { get; set; }

        GuestResponse ILoadableFrom<CreateGuestResponseInternalDTO, GuestResponse>.LoadFromDto(CreateGuestResponseInternalDTO dto)
        {
            AccessToken = dto.AccessToken;
            Duration = dto.Duration;
            User = User.TryLoadFromDto<UserResponseInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}