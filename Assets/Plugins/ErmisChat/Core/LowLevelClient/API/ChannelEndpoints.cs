using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.API
{
    /// <summary>
    /// <see cref="Channel"/> endpoints
    /// </summary>
    internal static class ChannelEndpoints
    {

        public static string Signal() => "/signal";
        public static string QueryChannels() => "/channels";

        public static string GetOrCreate(string type, string id) => $"channels/{type}/{id}/query";

        public static string Invite(string type, string id,string action) => $"channels/{type}/{id}/{action}";

        public static string GetOrCreate(string type) => $"channels/{type}/query";

        public static string GetAttachment(string type, string id) => $"channels/{type}/{id}/attachment";

        public static string Update(string type, string id) => $"channels/{type}/{id}";

        public static string SearchMessages() => $"channels/search";
        public static string SearchPublicChannelName() => $"channels/public/search";

        public static string ChannelMute(string type, string id) => $"channels/{type}/{id}/muted";

        public static string UpdatePartial(string type, string id) => $"channels/{type}/{id}";

        public static string DeleteChannel(string type, string id) => $"/channels/{type}/{id}";

        public static string DeleteMessage(string type, string id,string messageId) => $"/messages/{type}/{id}/{messageId}";

        public static string Reaction(string type, string id, string messageId,string reactionType) => $"/messages/{type}/{id}/{messageId}/reaction/{reactionType}";

        public static string PinUnpinMessage(string type, string id, string messageId, string action) => 
            $"/messages/{type}/{id}/{messageId}/{action}";


        public static string DeleteChannels() => "channels/delete";

        public static string TruncateChannel(string type, string id) => $"/channels/{type}/{id}/truncate";

        public static string MuteChannel() => $"/moderation/mute/channel";

        public static string UnmuteChannel() => $"/moderation/unmute/channel";

        public static string ShowChannel(string type, string id) => $"/channels/{type}/{id}/show";

        public static string HideChannel(string type, string id) => $"/channels/{type}/{id}/hide";
    }
}