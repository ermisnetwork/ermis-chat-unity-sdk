using System.Collections.Generic;
using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.Models;
using Ermis.Core.QueryBuilders.Filters;
using Ermis.Core.QueryBuilders.Filters.Channels;
using Ermis.Core.StatefulModels;

namespace ErmisChat.Samples
{
    internal class EventsSamples
    {

        private void OnMessageReceived(IErmisChannel channel, IErmisMessage message)
        {
        }

        private void OnMessageUpdated(IErmisChannel channel, IErmisMessage message)
        {
        }

        private void OnMessageDeleted(IErmisChannel channel, IErmisMessage message, bool isharddelete)
        {
        }

        private void OnReactionAdded(IErmisChannel channel, IErmisMessage message, ErmisReaction reaction)
        {
        }

        private void OnReactionUpdated(IErmisChannel channel, IErmisMessage message, ErmisReaction reaction)
        {
        }

        private void OnReactionRemoved(IErmisChannel channel, IErmisMessage message, ErmisReaction reaction)
        {
        }

        private void OnMemberAdded(IErmisChannel channel, IErmisChannelMember member)
        {
        }

        private void OnMemberRemoved(IErmisChannel channel, IErmisChannelMember member)
        {
        }

        private void OnMemberUpdated(IErmisChannel channel, IErmisChannelMember member)
        {
        }

        private void OnMembersChanged(IErmisChannel channel, IErmisChannelMember member, OperationType operationType)
        {
        }

        private void OnVisibilityChanged(IErmisChannel channel, bool isVisible)
        {
        }

        private void OnMuteChanged(IErmisChannel channel, bool isMuted)
        {
        }

        private void OnTruncated(IErmisChannel channel)
        {
        }

        private void OnUpdated(IErmisChannel channel)
        {
        }

        private void OnWatcherAdded(IErmisChannel channel, IErmisUser user)
        {
        }

        private void OnWatcherRemoved(IErmisChannel channel, IErmisUser user)
        {
        }

        private void OnUserStartedTyping(IErmisChannel channel, IErmisUser user)
        {
        }

        private void OnUserStoppedTyping(IErmisChannel channel, IErmisUser user)
        {
        }

        private void OnTypingUsersChanged(IErmisChannel channel)
        {
        }

        public void SubscribeToClientEvents()
        {
            Client.AddedToChannelAsMember += OnAddedToChannelAsMember;
            Client.RemovedFromChannelAsMember += OnRemovedFromChannel;
        }

        private void OnAddedToChannelAsMember(IErmisChannel channel, IErmisChannelMember member)
        {
            // channel - new channel to which local user was just added
            // member - object containing channel membership information
        }

        private void OnRemovedFromChannel(IErmisChannel channel, IErmisChannelMember member)
        {
            // channel - channel from which local user was removed
            // member - object containing channel membership information
        }

        public void SubscribeToConnectionEvents()
        {
            Client.Connected += OnConnected;
            Client.Disconnected += OnDisconnected;
            Client.ConnectionStateChanged += OnConnectionStateChanged;
        }

        private void OnConnected(IErmisLocalUserData localUserData)
        {
        }

        private void OnDisconnected()
        {
        }

        private void OnConnectionStateChanged(ConnectionState previous, ConnectionState current)
        {
        }

        public void Unsubscribe()
        {
            Client.Connected -= OnConnected;
            Client.Disconnected -= OnDisconnected;
            Client.ConnectionStateChanged -= OnConnectionStateChanged;
        }

        private IErmisChatClient Client { get; } = ErmisChatClient.CreateDefaultClient();
    }
}