﻿using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisChannelConfig : IStateLoadableFrom<ChannelConfigWithInfoInternalDTO, ErmisChannelConfig>
    {
        /// <summary>
        /// Enables automatic message moderation
        /// </summary>
        public ErmisAutomodType? Automod { get; private set; }

        /// <summary>
        /// Sets behavior of automatic moderation
        /// </summary>
        public ErmisAutomodBehaviourType? AutomodBehavior { get; private set; }

        public ErmisThresholds AutomodThresholds { get; private set; }

        /// <summary>
        /// Name of the blocklist to use
        /// </summary>
        public string Blocklist { get; private set; }

        /// <summary>
        /// Sets behavior of blocklist
        /// </summary>
        public ErmisAutomodBehaviourType? BlocklistBehavior { get; private set; }

        /// <summary>
        /// List of commands that channel supports
        /// </summary>
        public System.Collections.Generic.List<ErmisCommand> Commands { get; private set; }

        /// <summary>
        /// Connect events support
        /// </summary>
        public bool? ConnectEvents { get; private set; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// Enables custom events
        /// </summary>
        public bool? CustomEvents { get; private set; }

        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> Grants { get; private set; }

        /// <summary>
        /// Number of maximum message characters
        /// </summary>
        public int? MaxMessageLength { get; private set; }

        /// <summary>
        /// Number of days to keep messages. 'infinite' disables retention
        /// </summary>
        public string MessageRetention { get; private set; }

        /// <summary>
        /// Enables mutes
        /// </summary>
        public bool? Mutes { get; private set; }

        /// <summary>
        /// Channel type name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Enables push notifications
        /// </summary>
        public bool? PushNotifications { get; private set; }

        /// <summary>
        /// Enables message quotes
        /// </summary>
        public bool? Quotes { get; private set; }

        /// <summary>
        /// Enables message reactions
        /// </summary>
        public bool? Reactions { get; private set; }

        /// <summary>
        /// Read events support
        /// </summary>
        public bool? ReadEvents { get; private set; }

        public bool? Reminders { get; private set; }

        /// <summary>
        /// Enables message replies (threads)
        /// </summary>
        public bool? Replies { get; private set; }

        /// <summary>
        /// Enables message search
        /// </summary>
        public bool? Search { get; private set; }

        /// <summary>
        /// Typing events support
        /// </summary>
        public bool? TypingEvents { get; private set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        public System.DateTimeOffset? UpdatedAt { get; private set; }

        /// <summary>
        /// Enables file uploads
        /// </summary>
        public bool? Uploads { get; private set; }

        /// <summary>
        /// Enables URL enrichment
        /// </summary>
        public bool? UrlEnrichment { get; private set; }

        ErmisChannelConfig IStateLoadableFrom<ChannelConfigWithInfoInternalDTO, ErmisChannelConfig>.LoadFromDto(ChannelConfigWithInfoInternalDTO dto, ICache cache)
        {
            Automod = Automod.TryLoadNullableStructFromDto(dto.Automod);
            AutomodBehavior = AutomodBehavior.TryLoadNullableStructFromDto(dto.AutomodBehavior);
            AutomodThresholds = AutomodThresholds.TryLoadFromDto(dto.AutomodThresholds, cache);
            Blocklist = dto.Blocklist;
            BlocklistBehavior = BlocklistBehavior.TryLoadNullableStructFromDto(dto.BlocklistBehavior);
            Commands = Commands.TryLoadFromDtoCollection(dto.Commands, cache);
            ConnectEvents = dto.ConnectEvents;
            CreatedAt = dto.CreatedAt;
            CustomEvents = dto.CustomEvents;
            Grants = dto.Grants;
            MaxMessageLength = dto.MaxMessageLength;
            MessageRetention = dto.MessageRetention;
            Mutes = dto.Mutes;
            Name = dto.Name;
            PushNotifications = dto.PushNotifications;
            Quotes = dto.Quotes;
            Reactions = dto.Reactions;
            ReadEvents = dto.ReadEvents;
            Reminders = dto.Reminders;
            Replies = dto.Replies;
            Search = dto.Search;
            TypingEvents = dto.TypingEvents;
            UpdatedAt = dto.UpdatedAt;
            Uploads = dto.Uploads;
            UrlEnrichment = dto.UrlEnrichment;

            return this;
        }
    }
}