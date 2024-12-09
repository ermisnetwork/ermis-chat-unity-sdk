using System;
using Ermis.Libs.Utils;

namespace Ermis.Core
{
    public readonly struct ChannelType
    {
        public bool IsValid => !_channelTypeKey.IsNullOrEmpty();

        public static readonly ChannelType Messaging = new ChannelType("messaging");
        public static ChannelType Livestream => new ChannelType("livestream");
        public static ChannelType Team => new ChannelType("team");
        public static ChannelType Commerce => new ChannelType("commerce");
        public static ChannelType Gaming => new ChannelType("gaming");

        public static ChannelType Custom(string channelTypeKey) => new ChannelType(channelTypeKey);

        public ChannelType(string channelTypeKey)
        {
            if (channelTypeKey.IsNullOrEmpty())
            {
                throw new ArgumentException(
                    $"{channelTypeKey} cannot be null or empty. Use predefined channel types: {nameof(Messaging)}, " +
                    $"{nameof(Livestream)}, {nameof(Team)}, {nameof(Commerce)}, {nameof(Gaming)}, or create custom one in your Dashboard and use {nameof(Custom)}");
            }

            _channelTypeKey = channelTypeKey;
        }

        public static implicit operator string(ChannelType channelType) => channelType._channelTypeKey;

        public override string ToString() => _channelTypeKey;

        private readonly string _channelTypeKey;
    }
}