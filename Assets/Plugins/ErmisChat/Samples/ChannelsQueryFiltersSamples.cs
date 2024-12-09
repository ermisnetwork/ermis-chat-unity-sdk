using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.QueryBuilders.Filters;
using Ermis.Core.QueryBuilders.Filters.Channels;
using Ermis.Core.QueryBuilders.Sort;
using Ermis.Core.StatefulModels;

namespace ErmisChat.Samples
{
    internal class ChannelsQueryFiltersSamples
    {
        public async Task Operators()
        {
            IErmisChannel channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, "channel-id-1");

// Each operator usually supports multiple argument types to match your needs
            ChannelFilter.Cid.EqualsTo("channel-cid"); // string
            ChannelFilter.Cid.EqualsTo(channel); // IErmisChannel
            ChannelFilter.Cid.In("channel-cid", "channel-2-cid", "channel-3-cid"); // Comma separated strings

            var channelCids = new List<string> { "channel-1-cid", "channel-2-cid" };
            ChannelFilter.Cid.In(channelCids); // Any collection of string
        }

        private IErmisChatClient Client { get; } = ErmisChatClient.CreateDefaultClient();
    }
}