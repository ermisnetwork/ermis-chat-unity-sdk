﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.Models
{
    public class User : ModelBase, ILoadableFrom<UserIdObjectInternalDTO, User>,
        ILoadableFrom<UserResponseInternalDTO, User>, ILoadableFrom<UserEventPayloadInternalDTO, User>,
        ILoadableFrom<FullUserResponseInternalDTO, User>,
        ISavableTo<UserIdObjectInternalDTO>
    {
        /// <summary>
        /// Expiration date of the ban
        /// </summary>
        public System.DateTimeOffset? BanExpires { get; set; }

        /// <summary>
        /// Whether a user is banned or not
        /// </summary>
        public bool Banned { get; set; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Date of deactivation
        /// </summary>
        public System.DateTimeOffset? DeactivatedAt { get; set; }

        /// <summary>
        /// Date/time of deletion
        /// </summary>
        public System.DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// Unique user identifier
        /// </summary>
        public string Id { get; set; }

        public bool? Invisible { get; set; }

        /// <summary>
        /// Preferred language of a user
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Date of last activity
        /// </summary>
        public System.DateTimeOffset? LastActive { get; set; }

        /// <summary>
        /// Whether a user online or not
        /// </summary>
        public bool? Online { get; set; }

        public PushNotificationSettings PushNotifications { get; set; }

        /// <summary>
        /// Revocation date for tokens
        /// </summary>
        public System.DateTimeOffset? RevokeTokensIssuedBefore { get; set; }

        /// <summary>
        /// Determines the set of user permissions
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// List of teams user is a part of
        /// </summary>
        public System.Collections.Generic.List<string> Teams { get; set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        public System.DateTimeOffset? UpdatedAt { get; set; }

        //Not in API
        public string Name;
        public string Image;
        public string ProjectId;
        public string AboutMe;

        User ILoadableFrom<UserIdObjectInternalDTO, User>.LoadFromDto(UserIdObjectInternalDTO dto)
        {
            Id = dto.Id;
            return this;
        }

        User ILoadableFrom<UserResponseInternalDTO, User>.LoadFromDto(UserResponseInternalDTO dto)
        {
            AdditionalProperties = dto.AdditionalProperties;
            BanExpires = dto.BanExpires;
            Banned = dto.Banned;
            CreatedAt = dto.CreatedAt;
            DeactivatedAt = dto.DeactivatedAt;
            DeletedAt = dto.DeletedAt;
            Id = dto.Id;
            Invisible = dto.Invisible;
            Language = dto.Language;
            LastActive = dto.LastActive;
            Online = dto.Online;
            PushNotifications
                = PushNotifications.TryLoadFromDto<PushNotificationSettingsInternalDTO, PushNotificationSettings>(
                    dto.PushNotifications);
            RevokeTokensIssuedBefore = dto.RevokeTokensIssuedBefore;
            Role = dto.Role;
            Teams = dto.Teams;
            UpdatedAt = dto.UpdatedAt;

            //Not in API spec
            Name = dto.Name;
            Image = dto.Image;

            return this;
        }

        User ILoadableFrom<UserEventPayloadInternalDTO, User>.LoadFromDto(UserEventPayloadInternalDTO dto)
        {
            AdditionalProperties = dto.AdditionalProperties;
            BanExpires = dto.BanExpires;
            Banned = dto.Banned;
            CreatedAt = dto.CreatedAt;
            DeactivatedAt = dto.DeactivatedAt;
            DeletedAt = dto.DeletedAt;
            Id = dto.Id;
            Invisible = dto.Invisible;
            Language = dto.Language;
            LastActive = dto.LastActive;
            Online = dto.Online;
            PushNotifications
                = PushNotifications.TryLoadFromDto<PushNotificationSettingsInternalDTO, PushNotificationSettings>(
                    dto.PushNotifications);
            RevokeTokensIssuedBefore = dto.RevokeTokensIssuedBefore;
            Role = dto.Role;
            Teams = dto.Teams;
            UpdatedAt = dto.UpdatedAt;

            //Not in API spec
            Name = dto.Name;
            Image = dto.Image;

            return this;
        }

        UserIdObjectInternalDTO ISavableTo<UserIdObjectInternalDTO>.SaveToDto()
            => new UserIdObjectInternalDTO
            {
                Id = Id,
            };

        User ILoadableFrom<FullUserResponseInternalDTO, User>.LoadFromDto(FullUserResponseInternalDTO dto)
        {
            //AdditionalProperties = dto.AdditionalProperties;
            //BanExpires = dto.BanExpires;
            //Banned = dto.Banned;
            //CreatedAt = dto.CreatedAt;
            //DeactivatedAt = dto.DeactivatedAt;
            //DeletedAt = dto.DeletedAt;
            Id = dto.Id;
            //Invisible = dto.Invisible;
            //Language = dto.Language;
            //LastActive = dto.LastActive;
            //Online = dto.Online;
            //PushNotifications
            //    = PushNotifications.TryLoadFromDto<PushNotificationSettingsResponseInternalDTO, PushNotificationSettings>(
            //        dto.PushNotifications);
           // RevokeTokensIssuedBefore = dto.RevokeTokensIssuedBefore;
            //Role = dto.Role;
           // Teams = dto.Teams;
            //UpdatedAt = dto.UpdatedAt;

            //Not in API spec
            Name = dto.Name;
            Image = dto.Avatar;
            ProjectId = dto.ProjectId;
            AboutMe = dto.AboutMe;
            return this;
        }
    }

    public class UserIdObject : ModelBase, ILoadableFrom<UserIdObjectInternalDTO, UserIdObject>,
        ISavableTo<UserIdObjectInternalDTO>
    {
        
        public string Id { get; set; }

        UserIdObject ILoadableFrom<UserIdObjectInternalDTO, UserIdObject>.LoadFromDto(UserIdObjectInternalDTO dto)
        {
            Id = dto.Id;
            return this;
        }

        UserIdObjectInternalDTO ISavableTo<UserIdObjectInternalDTO>.SaveToDto()
            => new UserIdObjectInternalDTO
            {
                Id = Id,
            };
    }

}