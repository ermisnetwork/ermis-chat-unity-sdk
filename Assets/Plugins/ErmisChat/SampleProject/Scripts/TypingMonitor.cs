using System;
using System.Collections.Generic;
using Ermis.Core;
using Ermis.Core.StatefulModels;
using TMPro;
using UnityEngine;

namespace ErmisChat.SampleProject
{
    public class TypingMonitor : IDisposable
    {
        public TypingMonitor(TMP_InputField source, IErmisChatClient client, IChatState chatState, Func<bool> isActive)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _chatState = chatState ?? throw new ArgumentNullException(nameof(chatState));
            _isActive = isActive ?? throw new ArgumentNullException(nameof(isActive));

            _source.onValueChanged.AddListener(OnInputValueChanged);
        }

        public void Update()
        {
            for (int i = _startedTypingChannels.Count - 1; i >= 0; i--)
            {
                var channel = _startedTypingChannels[i];

                if (!_channelCidToTypingStartEventSentTime.TryGetValue(channel.Cid, out var eventSentTime))
                {
                    Debug.LogWarning($"Tried to send typing.stop event but channel CId `{channel}` was not found");
                    _startedTypingChannels.RemoveAt(i);
                    continue;
                }

                if (Time.time - eventSentTime > TypingStopEventTimeout)
                {
                    TrySendStopTypingEvent(channel);
                }
            }
        }

        public void NotifyChannelStoppedTyping(IErmisChannel channel)
            => TrySendStopTypingEvent(channel);

        public void Dispose()
        {
            _source.onValueChanged.RemoveListener(OnInputValueChanged);
        }

        /// <summary>
        /// Minimum interval to send the typing.start event per channel
        /// </summary>
        private const float TypingStartEventThrottleInterval = 2;

        /// <summary>
        /// Timeout after typing has stopped to send the typing.stop event
        /// </summary>
        private const float TypingStopEventTimeout = 15;

        private readonly TMP_InputField _source;
        private readonly IErmisChatClient _client;
        private readonly Func<bool> _isActive;
        private readonly IChatState _chatState;

        private readonly Dictionary<string, float> _channelCidToTypingStartEventSentTime =
            new Dictionary<string, float>();

        private readonly List<IErmisChannel> _startedTypingChannels = new List<IErmisChannel>();

        private void OnInputValueChanged(string text)
        {
            if (!_isActive() || _chatState.ActiveChannel == null)
            {
                return;
            }

            var activeChannel = _chatState.ActiveChannel;

            TrySendStartTypingEvent(activeChannel);
        }

        private void TrySendStartTypingEvent(IErmisChannel channel)
        {
            if (_channelCidToTypingStartEventSentTime.TryGetValue(channel.Cid, out var typingStartSentTime) &&
                Time.time - typingStartSentTime < TypingStartEventThrottleInterval)
            {
                return;
            }

            channel.SendTypingStartedEventAsync();

            if (!_channelCidToTypingStartEventSentTime.ContainsKey(channel.Cid))
            {
                _startedTypingChannels.Add(channel);
            }

            _channelCidToTypingStartEventSentTime[channel.Cid] = Time.time;
        }

        private void TrySendStopTypingEvent(IErmisChannel channel)
        {
            if (!_channelCidToTypingStartEventSentTime.ContainsKey(channel.Cid))
            {
                return;
            }

            channel.SendTypingStoppedEventAsync();

            _channelCidToTypingStartEventSentTime.Remove(channel.Cid);

            for (int i = _startedTypingChannels.Count - 1; i >= 0; i--)
            {
                if (_startedTypingChannels[i].Cid == channel.Cid)
                {
                    _startedTypingChannels.RemoveAt(i);
                    break;
                }
            }
        }
    }
}