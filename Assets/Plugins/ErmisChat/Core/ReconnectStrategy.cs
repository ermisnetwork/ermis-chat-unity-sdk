﻿using Ermis.Core.LowLevelClient;

namespace Ermis.Core
{
    /// <summary>
    /// Strategy for <see cref="IErmisChatLowLevelClient"/> when connection is lost
    /// </summary>
    public enum ReconnectStrategy
    {
        /// <summary>
        /// Reconnect attempts will occur at exponentially increasing intervals
        /// </summary>
        Exponential,

        /// <summary>
        /// Reconnect attempts will occur at constant interval
        /// </summary>
        Constant,

        /// <summary>
        /// The Ermis Chat Client will never attempt to reconnect. You need to call the <see cref="IErmisChatLowLevelClient.Connect"/> on your own
        /// </summary>
        Never,
    }
}