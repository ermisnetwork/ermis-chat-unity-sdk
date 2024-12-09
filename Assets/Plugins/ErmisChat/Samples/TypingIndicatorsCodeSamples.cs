using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.StatefulModels;

namespace ErmisChat.Samples
{
    internal sealed class TypingIndicatorsCodeSamples
    {
        public async Task SendStartStopTypingEvents()
        {
            IErmisChannel channel = null;

// Send typing started event
            await channel.SendTypingStartedEventAsync();

// Send typing stopped event
            await channel.SendTypingStoppedEventAsync();
        }

        public async Task ReceivingTypingEvents()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, "channel-id");
            channel.UserStartedTyping += OnUserStartedTyping;
            channel.UserStoppedTyping += OnUserStoppedTyping;
        }

        private void OnUserStartedTyping(IErmisChannel channel, IErmisUser user)
        {
        }

        private void OnUserStoppedTyping(IErmisChannel channel, IErmisUser user)
        {
        }

        private IErmisChatClient Client { get; } = ErmisChatClient.CreateDefaultClient();
    }
}