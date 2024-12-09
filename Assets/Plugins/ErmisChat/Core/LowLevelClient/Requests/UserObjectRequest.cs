using System;
using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    /// <summary>
    /// Represents chat user
    /// </summary>
    public class UserObjectRequest : RequestObjectBase, ISavableTo<UserObjectRequestInternalDTO>,
        ISavableTo<UserRequestInternalDTO>, ISavableTo<UserIdObjectInternalDTO>
    {
        /// <summary>
        /// Expiration date of the ban
        /// </summary>
        public DateTimeOffset? BanExpires { get; set; }

        /// <summary>
        /// Whether a user is banned or not
        /// </summary>
        public bool? Banned { get; set; }

        /// <summary>
        /// Unique user identifier
        /// </summary>
        public string Id { get; set; }

        public bool? Invisible { get; set; }

        /// <summary>
        /// Preferred language of a user
        /// </summary>
        public string Language { get; set; }

        public PushNotificationSettingsRequest PushNotifications { get; set; }

        /// <summary>
        /// Revocation date for tokens
        /// </summary>
        public DateTimeOffset? RevokeTokensIssuedBefore { get; set; }

        /// <summary>
        /// Determines the set of user permissions
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// List of teams user is a part of
        /// </summary>
        public List<string> Teams { get; set; }

        UserObjectRequestInternalDTO ISavableTo<UserObjectRequestInternalDTO>.SaveToDto()
            => new UserObjectRequestInternalDTO
            {
                BanExpires = BanExpires,
                Banned = Banned,
                Id = Id,
                Invisible = Invisible,
                Language = Language,
                PushNotifications = PushNotifications.TrySaveToDto<PushNotificationSettingsRequestInternalDTO>(),
                RevokeTokensIssuedBefore = RevokeTokensIssuedBefore,
                Role = Role,
                Teams = Teams,
                AdditionalProperties = AdditionalProperties
            };

        UserRequestInternalDTO ISavableTo<UserRequestInternalDTO>.SaveToDto()
            => new UserRequestInternalDTO
            {
                BanExpires = BanExpires,
                Banned = Banned,
                Id = Id,
                Invisible = Invisible,
                Language = Language,
                PushNotifications = PushNotifications.TrySaveToDto<PushNotificationSettingsRequestInternalDTO>(),
                RevokeTokensIssuedBefore = RevokeTokensIssuedBefore,
                Role = Role,
                Teams = Teams,
                AdditionalProperties = AdditionalProperties
            };

        UserIdObjectInternalDTO ISavableTo<UserIdObjectInternalDTO>.SaveToDto()
            => new UserIdObjectInternalDTO
            {
                Id = Id,
            };
    }
}