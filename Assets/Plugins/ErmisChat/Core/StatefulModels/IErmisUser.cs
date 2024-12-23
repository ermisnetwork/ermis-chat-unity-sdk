using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ermis.Core.Models;

namespace Ermis.Core.StatefulModels
{
    /// <summary>
    /// <para>Users can become <see cref="IErmisChannelMember"/> of <see cref="IErmisChannel"/> and send messages</para>
    /// <b>State of this object is automatically updated</b>
    /// </summary>
    public interface IErmisUser : IErmisStatefulModel
    {
        /// <summary>
        /// Event fired when the user online state changed. Check <see cref="IErmisUser.Online"/> and <see cref="IErmisUser.LastActive"/> to know if the user is online or when was last active
        /// </summary>
        event ErmisUserPresenceHandler PresenceChanged;

        /// <summary>
        /// Expiration date of the ban
        /// </summary>
        DateTimeOffset? BanExpires { get; }

        /// <summary>
        /// Whether a user is banned or not
        /// </summary>
        bool Banned { get; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Date of deactivation
        /// </summary>
        DateTimeOffset? DeactivatedAt { get; }

        /// <summary>
        /// Date/time of deletion
        /// </summary>
        DateTimeOffset? DeletedAt { get; }

        /// <summary>
        /// Unique user identifier
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Invisible user will appear as offline to other users
        /// You can change user visibility with <see cref="MarkInvisibleAsync"/> & <see cref="MarkVisibleAsync"/>
        /// </summary>
        bool Invisible { get; }

        /// <summary>
        /// Preferred language of a user
        /// </summary>
        string Language { get; }

        /// <summary>
        /// Date of last activity
        /// </summary>
        DateTimeOffset? LastActive { get; }

        /// <summary>
        /// Whether a user online or not
        /// </summary>
        bool Online { get; }

        ErmisPushNotificationSettings PushNotifications { get; }

        /// <summary>
        /// Revocation date for tokens
        /// </summary>
        DateTimeOffset? RevokeTokensIssuedBefore { get; }

        /// <summary>
        /// Determines the set of user permissions
        /// </summary>
        string Role { get; }

        /// <summary>
        /// Whether user is shadow banned or not
        /// </summary>
        bool ShadowBanned { get; }

        /// <summary>
        /// List of teams user is a part of
        /// </summary>
        IReadOnlyList<string> Teams { get; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        DateTimeOffset? UpdatedAt { get; }

        string Name { get; }
        string Image { get; }

        /// <summary>
        /// Flag this user
        /// </summary>
        Task FlagAsync();

        /// <summary>
        /// Mark user as muted. Any user is allowed to mute another user. Mute will last until the <see cref="IErmisChatClient"/> is called or until mute expires.
        /// Muted user messages will still be received by the <see cref="IErmisLocalUserData"/> so if you wish to hide muted users messages you need implement by yourself
        ///
        /// You can access mutes via <see cref="IErmisChatClient.LocalUserData"/> in <see cref="IErmisUser.UnmuteAsync"/>
        /// </summary>
        Task MuteAsync();

        /// <summary>
        /// Remove user mute. Any user is allowed to mute another user. Mute will last until the <see cref="IErmisChatClient"/> is called or until mute expires.
        /// Muted user messages will still be received by the <see cref="IErmisChatClient"/> so if you wish to hide muted users messages you need implement by yourself
        ///
        /// You can access mutes via <see cref="IErmisLocalUserData.Mutes"/> in <see cref="IErmisUser.UnmuteAsync"/>
        /// </summary>
        Task UnmuteAsync();

        /// <summary>
        /// Mark user as invisible. Invisible user will appear as offline to other users.
        /// User will remain invisible even if you disconnect and reconnect again. You must call <see cref="MarkVisibleAsync"/> in order to become visible again.
        /// </summary>
        Task MarkInvisibleAsync();

        /// <summary>
        /// Mark user visible again if he was previously marked as invisible with <see cref="MarkInvisibleAsync"/>
        /// </summary>
        Task MarkVisibleAsync();
    }
}