using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.API
{
    /// <summary>
    /// Messages API Endpoints
    /// </summary>
    internal static class MessageEndpoints
    {
        public static string SendMessage(string channelType, string channelId)
            => $"/channels/{channelType}/{channelId}/message";

        public static string SendMessage(ChannelState channel) => SendMessage(channel.Channel.Type, channel.Channel.Id);

        public static string UpdateMessage(string channelType, string channelId, string messageId) => $"/messages/{channelType}/{channelId}/{messageId}";

        public static string DeleteMessage(string channelType, string channelId, string messageId) => $"/messages/{channelType}/{channelId}/{messageId}";

        public static string SendReaction(string channelType, string channelId, string messageId, string reaction) => 
            $"/messages/{channelType}/{channelId}/{messageId}/reaction/{reaction}";

        public static string DeleteReaction(string channelType, string channelId, string messageId, string reaction) => 
            $"/messages/{channelType}/{channelId}/{messageId}/reaction/{reaction}";

        public static string PinUnpinMessage(string type, string id, string messageId, string action) =>
            $"/messages/{type}/{id}/{messageId}/{action}";

        public static string SearchMessages() => $"channels/search";
    }
}