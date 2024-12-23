﻿using Ermis.Core.LowLevelClient;
using Ermis.Core.LowLevelClient.Events;

namespace ErmisChat.Samples.LowLevelClient.ClientDocs
{
    /// <summary>
    /// </summary>
    public class EventsClientCodeSamples
    {
        private IErmisChatLowLevelClient _lowLevelClient;

        public void SimpleSubscribeToEvents()
        {
            _lowLevelClient.MessageReceived += newMessageEvent =>
            {
                var channelId = newMessageEvent.ChannelId;
                var channelType = newMessageEvent.ChannelType;
                var channelCid = newMessageEvent.Cid;
                var messageData = newMessageEvent.Message;
            };

            _lowLevelClient.MessageUpdated += messageUpdatedEvent =>
            {
                var channelId = messageUpdatedEvent.ChannelId;
                var channelType = messageUpdatedEvent.ChannelType;
                var channelCid = messageUpdatedEvent.Cid;
                var messageData = messageUpdatedEvent.Message;
            };

            _lowLevelClient.MessageDeleted += messageDeletedEvent =>
            {
                var channelId = messageDeletedEvent.ChannelId;
                var channelType = messageDeletedEvent.ChannelType;
                var channelCid = messageDeletedEvent.Cid;
                var messageData = messageDeletedEvent.Message;
            };

            _lowLevelClient.EventReceived += serializedEventData => { };

            _lowLevelClient.Connected += (localUser) =>
            {
                //client connected
            };

            _lowLevelClient.ConnectionStateChanged += (prevState, currentState) =>
            {
                //connection changed from prevState to currentState
            };
        }

        private void SubscribeToEvents()
        {
            _lowLevelClient.MessageReceived += OnLowLevelClientMessageReceived;
            _lowLevelClient.MessageUpdated += OnLowLevelClientMessageUpdated;
            _lowLevelClient.MessageDeleted += OnLowLevelClientMessageDeleted;
        }

        private void UnsubscribeFromEvents()
        {
            _lowLevelClient.MessageReceived -= OnLowLevelClientMessageReceived;
            _lowLevelClient.MessageUpdated -= OnLowLevelClientMessageUpdated;
            _lowLevelClient.MessageDeleted -= OnLowLevelClientMessageDeleted;
        }

        private void OnLowLevelClientMessageReceived(EventMessageNew newMessageEvent)
        {
        }

        private void OnLowLevelClientMessageDeleted(EventMessageDeleted messageDeletedEvent)
        {
        }

        private void OnLowLevelClientMessageUpdated(EventMessageUpdated messageDeletedEvent)
        {
        }
    }
}