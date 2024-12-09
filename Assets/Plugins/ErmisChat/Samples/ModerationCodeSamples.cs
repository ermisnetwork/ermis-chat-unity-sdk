using System;
using System.Linq;
using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.Requests;

namespace ErmisChat.Samples
{
    internal sealed class ModerationCodeSamples
    {
      
        public async Task Flag()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var message = channel.Messages.First();
            var channelMember = channel.Members.First();

// Flag a message
            await message.FlagAsync();

// Flag a user
            await channelMember.User.FlagAsync();
        }

     
        public async Task QueryMessageFlags()
        {
            await Task.CompletedTask;
        }

    
        public async Task Mutes()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var channelMember = channel.Members.First();

// Mute a user
            await channelMember.User.MuteAsync();

// Unmute previously muted user
            await channelMember.User.UnmuteAsync();

// Mute a channel
            await channel.MuteChannelAsync();

// Mute previously muted channel
            await channel.UnmuteChannelAsync();
        }

     
        public async Task Ban()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

// Dummy example to get IErmisUser to ban
            var user = channel.Messages.First().User;

// Dummy example to get IErmisUser to ban
            var channelMember = channel.Members.First();

// Ban a user permanently from this channel permanently
            await channel.BanUserAsync(user);

// Use any combination of the optional parameters: reason, timeoutMinutes, isIpBan

// Ban a user from this channel for 2 hours with a reason
            await channel.BanUserAsync(user, "You got banned for 2 hours for toxic behaviour.", 120);

// Ban a user IP from this channel for 2 hours without a reason
            await channel.BanUserAsync(user, timeoutMinutes: 120, isIpBan: true);

// Ban a member from this channel permanently
            await channel.BanMemberAsync(channelMember);
        }

     
        public async Task QueryBannedUsers()
        {
// Get users banned in the last 24 hours
            var request = new ErmisQueryBannedUsersRequest
            {
                CreatedAtAfterOrEqual = new DateTimeOffset().AddHours(-24),
                Limit = 30,
                Offset = 0,
            };

            var bannedUsersInfo = await Client.QueryBannedUsersAsync(request);
        }

       
        public async Task QueryBansEndpoint()
        {
            await Task.CompletedTask;
        }

      
        public async Task ShadowBan()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

// Dummy example to get IErmisUser to ban
            var user = channel.Messages.First().User;

// Dummy example to get IErmisUser to ban
            var channelMember = channel.Members.First();

// Shadow Ban a user from this channel permanently
            await channel.ShadowBanUserAsync(user);

// Shadow Ban a member from this channel
            await channel.ShadowBanMemberAsync(channelMember);

// Use any combination of optional parameters: reason, timeoutMinutes, isIpBan

// Shadow Ban a member from this channel permanently
            await channel.ShadowBanMemberAsync(channelMember);

// Shadow Ban a member from this channel for 2 hours with a reason
            await channel.ShadowBanMemberAsync(channelMember, "Banned for 2 hours for toxic behaviour.", 120);

// Shadow Ban a member IP from this channel for 2 hours without a reason
            await channel.ShadowBanMemberAsync(channelMember, timeoutMinutes: 120, isIpBan: true);
        }

        private IErmisChatClient Client { get; } = ErmisChatClient.CreateDefaultClient();
    }
}