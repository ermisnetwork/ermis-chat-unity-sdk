using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;
using Ermis.Core.Models;
using Ermis.Core.Requests;

namespace Ermis.Core.StatefulModels
{
    internal sealed class ErmisMessage : ErmisStatefulModelBase<ErmisMessage>,
        IErmisMessage, IUpdateableFrom<MessageInternalDTO, ErmisMessage>, IUpdateableFrom<MessageResponseInternalDTO, ErmisMessage>
    {
        public event ErmisMessageReactionHandler ReactionAdded;
        public event ErmisMessageReactionHandler ReactionRemoved;
        public event ErmisMessageReactionHandler ReactionUpdated;

        public IReadOnlyList<ErmisMessageAttachment> Attachments => _attachments;

        /// <summary>
        /// Whether `before_message_send webhook` failed or not. Field is only accessible in push webhook
        /// </summary>
        //public bool? BeforeMessageSendFailed { get; private set; } //ErmisTodo: verify this property

        public string Cid { get; private set; }

        public string Command { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? DeletedAt { get; private set; }

        public string Html { get; private set; }

        public IReadOnlyDictionary<string, string> I18n => _iI18n;

        public string Id { get; private set; }

        public IReadOnlyDictionary<string, IReadOnlyList<string>> ImageLabels => throw new NotImplementedException();

        public IReadOnlyList<ErmisReaction> LatestReactions => _latestReactions;

        public IReadOnlyList<IErmisUser> MentionedUsers => _mentionedUsers;

        public IReadOnlyList<ErmisReaction> OwnReactions => _ownReactions;

        public string ParentId { get; private set; }

        public DateTimeOffset? PinExpires { get; private set; }

        public bool Pinned { get; private set; }

        public DateTimeOffset? PinnedAt { get; private set; }

        public IErmisUser PinnedBy { get; private set; }

        public IErmisMessage QuotedMessage { get; private set; }

        public string QuotedMessageId { get; private set; }

        public IReadOnlyDictionary<string, int> ReactionCounts => _reactionCounts;

        public IReadOnlyDictionary<string, int> ReactionScores => _reactionScores;

        public int? ReplyCount { get; private set; }

        public bool? Shadowed { get; private set; }

        public bool? ShowInChannel { get; private set; }

        public bool? Silent { get; private set; }

        public string Text { get; private set; }

        public IReadOnlyList<IErmisUser> ThreadParticipants => _threadParticipants;

        public ErmisMessageType Type { get; private set; }

        public DateTimeOffset? UpdatedAt { get; private set; }

        public IErmisUser User { get; private set; }

        public bool IsDeleted => Type == MessageType.Deleted;

        //Do not update message from response, the WS event might have been processed and we would overwrite it with an old state
        public Task SoftDeleteAsync()
        {
            if (!Cache.Channels.TryGet(Cid, out var ermisChannel))
            {
                throw new Exception($"Failed to get channel with id {Cid} from cache. Please report this issue");
            }
            return LowLevelClient.InternalMessageApi.DeleteMessageAsync(ermisChannel.Type, ermisChannel.Id, Id);
        }

        //Do not update message from response, the WS event might have been processed and we would overwrite it with an old state
        public Task HardDeleteAsync()
        {
            if (!Cache.Channels.TryGet(Cid, out var ermisChannel))
            {
                throw new Exception($"Failed to get channel with id {Cid} from cache. Please report this issue");
            }
            return  LowLevelClient.InternalMessageApi.DeleteMessageAsync(ermisChannel.Type, ermisChannel.Id, Id);
        }
        
        public async Task UpdateAsync(ErmisUpdateMessageRequest ermisUpdateMessageRequest)
        {
            if (ermisUpdateMessageRequest == null)
            {
                throw new ArgumentNullException(nameof(ermisUpdateMessageRequest));
            }

            if (!Cache.Channels.TryGet(Cid, out var ermisChannel))
            {
                throw new Exception($"Failed to get channel with id {Cid} from cache. Please report this issue");
            }

            var requestDto = ermisUpdateMessageRequest.TrySaveToDto();


            var response = await LowLevelClient.InternalMessageApi.UpdateMessageAsync(ermisChannel.Type, ermisChannel.Id, Id, requestDto);
            Cache.TryCreateOrUpdate(response.Message);
        }

        public async Task PinAsync()
        {
            var request = new PinUnpinRequestInternalDTO
            {
                Message = Text
            };

            if (!Cache.Channels.TryGet(Cid, out var ermisChannel))
            {
                throw new Exception($"Failed to get channel with id {Cid} from cache. Please report this issue");
            }

            var response = await LowLevelClient.InternalMessageApi.PinUnpinMessageAsync(ermisChannel.Type, ermisChannel.Id, Id, "pin", request);
            Cache.TryCreateOrUpdate(response.Message);

            //ErmisTodo: is this needed? How are other users notified about message pin?
            await Client.RefreshChannelState(Cid);
        }

        public async Task UnpinAsync()
        {
            var request = new PinUnpinRequestInternalDTO
            {
                Message = Text
            };

            if (!Cache.Channels.TryGet(Cid, out var ermisChannel))
            {
                throw new Exception($"Failed to get channel with id {Cid} from cache. Please report this issue");
            }

            var response = await LowLevelClient.InternalMessageApi.PinUnpinMessageAsync(ermisChannel.Type, ermisChannel.Id, Id, "unpin", request);
            Cache.TryCreateOrUpdate(response.Message);

            //ErmisTodo: is this needed? How are other users notified about message pin?
            await Client.RefreshChannelState(Cid);
        }

        public async Task SendReactionAsync(string type, int score = 1, bool enforceUnique = false,
            bool skipMobilePushNotifications = false)
        {
            ErmisAsserts.AssertNotNullOrEmpty(type, nameof(type));
            if (!Cache.Channels.TryGet(Cid, out var ermisChannel))
            {
                throw new Exception($"Failed to get channel with id {Cid} from cache. Please report this issue");
            }
            var response = await LowLevelClient.InternalMessageApi.SendReactionAsync(ermisChannel.Type, ermisChannel.Id, Id, type);
            Cache.TryCreateOrUpdate(response.Message);
        }

        //ErmisTodo: docs calls it Remove
        public Task DeleteReactionAsync(string type)
        {
            ErmisAsserts.AssertNotNullOrEmpty(type, nameof(type));
            if (!Cache.Channels.TryGet(Cid, out var ermisChannel))
            {
                throw new Exception($"Failed to get channel with id {Cid} from cache. Please report this issue");
            }
            return LowLevelClient.InternalMessageApi.DeleteReactionAsync(ermisChannel.Type, ermisChannel.Id, Id, type);
        }

        //ErmisTodo: should we unwrap the response?
        public Task FlagAsync() => LowLevelClient.InternalModerationApi.FlagMessageAsync(Id);

        public Task MarkMessageAsLastReadAsync()
        {
            if (!Cache.Channels.TryGet(Cid, out var ermisChannel))
            {
                throw new Exception($"Failed to get channel with id {Cid} from cache. Please report this issue");
            }

            return LowLevelClient.InternalChannelApi.MarkReadAsync(ermisChannel.Type, ermisChannel.Id);

        }

        void IUpdateableFrom<MessageInternalDTO, ErmisMessage>.UpdateFromDto(MessageInternalDTO dto, ICache cache)
        {
            //BeforeMessageSendFailed = GetOrDefault(dto.BeforeMessageSendFailed, BeforeMessageSendFailed);
            Cid = GetOrDefault(dto.Cid, Cid);
            Command = GetOrDefault(dto.Command, Command);
            CreatedAt = GetOrDefault(dto.CreatedAt, CreatedAt);
            DeletedAt = GetOrDefault(dto.DeletedAt, DeletedAt);
            Html = GetOrDefault(dto.Html, Html);
            _iI18n.TryReplaceValuesFromDto(dto.I18n);
            Id = GetOrDefault(dto.Id, Id);
            //_imageLabels.TryReplaceValuesFromDto(dto.ImageLabels); //ErmisTodo: NOT IMPLEMENTED
            _latestReactions.TryReplaceRegularObjectsFromDto(dto.LatestReactions, cache);
            _mentionedUsers.TryReplaceTrackedObjects(dto.MentionedUsers, cache.Users);
            //dto.Mml ignored because its only server-side
            _ownReactions.TryReplaceRegularObjectsFromDto(dto.OwnReactions, cache);
            ParentId = GetOrDefault(dto.ParentId, ParentId);
            PinExpires = GetOrDefault(dto.PinExpires, PinExpires);
            Pinned = GetOrDefault(dto.Pinned, Pinned);
            PinnedAt = GetOrDefault(dto.PinnedAt, PinnedAt);
            PinnedBy = cache.TryCreateOrUpdate(dto.PinnedBy);
            QuotedMessage = cache.TryCreateOrUpdate(dto.QuotedMessage);
            QuotedMessageId = GetOrDefault(dto.QuotedMessageId, QuotedMessageId);
            _reactionCounts.TryReplaceValuesFromDto(dto.ReactionCounts); //ErmisTodo: is this append only?
            _reactionScores.TryReplaceValuesFromDto(dto.ReactionScores);
            ReplyCount = GetOrDefault(dto.ReplyCount, ReplyCount);
            Shadowed = GetOrDefault(dto.Shadowed, Shadowed);
            ShowInChannel = GetOrDefault(dto.ShowInChannel, ShowInChannel);
            Silent = GetOrDefault(dto.Silent, Silent);
            Text = GetOrDefault(dto.Text, Text);
            _mentionedUsers.TryReplaceTrackedObjects(dto.MentionedUsers, cache.Users);
            _threadParticipants.TryReplaceTrackedObjects(dto.ThreadParticipants, cache.Users);
            Type = Type.TryLoadFromDto(dto.Type);
            UpdatedAt = GetOrDefault(dto.UpdatedAt, UpdatedAt);
            User = cache.TryCreateOrUpdate(dto.User);

            LoadAdditionalProperties(dto.AdditionalProperties);
        }

        void IUpdateableFrom<MessageResponseInternalDTO, ErmisMessage>.UpdateFromDto(MessageResponseInternalDTO dto, ICache cache)
        {
            _attachments.TryReplaceRegularObjectsFromDto(dto.Attachments, cache);
            Cid = GetOrDefault(dto.Cid, Cid);
            CreatedAt = GetOrDefault(dto.CreatedAt, CreatedAt);
            Id = GetOrDefault(dto.Id, Id);
            _latestReactions.TryReplaceRegularObjectsFromDto(dto.LatestReactions, cache);
            _mentionedUsers.TryReplaceTrackedObjects(dto.MentionedUsers, cache.Users);
            _reactionCounts.TryReplaceValuesFromDto(dto.ReactionCounts); //ErmisTodo: is this append only?
            Text = GetOrDefault(dto.Text, Text);
            Type = Type.TryLoadFromDto(dto.Type);
            UpdatedAt = GetOrDefault(dto.UpdatedAt, UpdatedAt);
            User = cache.TryCreateOrUpdate(dto.User);
        }

        internal ErmisMessage(string uniqueId, ICacheRepository<ErmisMessage> repository, IStatefulModelContext context)
            : base(uniqueId, repository, context)
        {
        }

        internal void InternalHandleSoftDelete()
        {
            Text = string.Empty;
        }

        internal void HandleReactionNewEvent(ReactionNewEventInternalDTO eventDto, ErmisChannel channel, ErmisReaction reaction)
        {
            AssertCid(eventDto.Cid);
            AssertMessageId(eventDto.Message.Id);

            // Important! `reaction.new` has this field invalidly empty. This is for performance reasons but is left as empty for backward compatibility.
            // We rely on the fact that _ownReactions.TryReplaceRegularObjectsFromDto ignores null and does not clear the list when `own_reactions` is invalidly empty
            eventDto.Message.OwnReactions = null;

            //ErmisTodo: verify if this how we should update the message + what about events for customer to get notified
            Cache.TryCreateOrUpdate(eventDto.Message);
            ReactionAdded?.Invoke(channel, this, reaction);
        }

        internal void HandleReactionUpdatedEvent(ReactionUpdatedEventInternalDTO eventDto, ErmisChannel channel, ErmisReaction reaction)
        {
            AssertCid(eventDto.Cid);
            AssertMessageId(eventDto.Message.Id);

            // Important! `reaction.new` has this field invalidly empty. This is for performance reasons but is left as empty for backward compatibility.
            // We rely on the fact that _ownReactions.TryReplaceRegularObjectsFromDto ignores null and does not clear the list when `own_reactions` is invalidly empty
            eventDto.Message.OwnReactions = null;

            Cache.TryCreateOrUpdate(eventDto.Message);
            ReactionUpdated?.Invoke(channel, this, reaction);
        }

        internal void HandleReactionDeletedEvent(ReactionDeletedEventInternalDTO eventDto, ErmisChannel channel, ErmisReaction reaction)
        {
            AssertCid(eventDto.Cid);
            AssertMessageId(eventDto.Message.Id);

            // Important! `reaction.new` has this field invalidly empty. This is for performance reasons but is left as empty for backward compatibility.
            // We rely on the fact that _ownReactions.TryReplaceRegularObjectsFromDto ignores null and does not clear the list when `own_reactions` is invalidly empty
            eventDto.Message.OwnReactions = null;

            Cache.TryCreateOrUpdate(eventDto.Message);
            ReactionRemoved?.Invoke(channel, this, reaction);
        }

        protected override ErmisMessage Self => this;

        protected override string InternalUniqueId
        {
            get => Id;
            set => Id = value;
        }

        // ErmisTodo: change to lazy loading
        private readonly List<ErmisMessageAttachment> _attachments = new List<ErmisMessageAttachment>();
        private readonly List<ErmisReaction> _latestReactions = new List<ErmisReaction>();
        private readonly List<ErmisUser> _mentionedUsers = new List<ErmisUser>();
        private readonly List<ErmisReaction> _ownReactions = new List<ErmisReaction>();
        private readonly Dictionary<string, int> _reactionCounts = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _reactionScores = new Dictionary<string, int>();
        private readonly List<ErmisUser> _threadParticipants = new List<ErmisUser>();

        private readonly Dictionary<string, string> _iI18n = new Dictionary<string, string>();

        private void AssertMessageId(string messageId)
        {
            if (messageId != Id)
            {
                throw new InvalidOperationException($"ID mismatch, received: `{messageId}` but current message ID is: {Id}");
            }
        }

        private void AssertCid(string cid)
        {
            if (cid != Cid)
            {
                throw new InvalidOperationException($"Cid mismatch, received: `{cid}` but current channel is: {Cid}");
            }
        }
    }
}