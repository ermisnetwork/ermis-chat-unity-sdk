using System.Collections.Generic;
using Ermis.Core.Models;

namespace Ermis.Core.StatefulModels
{
    /// <summary>
    /// Data of the local <see cref="IErmisUser"/> connected with <see cref="IErmisChatClient"/> to the Ermis Chat Server
    /// Use <see cref="IErmisLocalUserData.User"/> to get local <see cref="IErmisUser"/> reference
    /// </summary>
    public interface IErmisLocalUserData : IErmisStatefulModel
    {
        /// <summary>
        /// Muted channels
        /// </summary>
        IReadOnlyList<ErmisChannelMute> ChannelMutes { get; }

        IReadOnlyList<ErmisDevice> Devices { get; }
        IReadOnlyList<string> LatestHiddenChannels { get; }

        /// <summary>
        /// Muted users
        /// </summary>
        IReadOnlyList<ErmisUserMute> Mutes { get; }

        int? TotalUnreadCount { get; }
        int? UnreadChannels { get; }
        IErmisUser User { get; }
        string UserId { get; }
    }
}