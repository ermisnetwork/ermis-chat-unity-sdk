using System;
using Ermis.Libs.Logs;

namespace Ermis.Core
{
    /// <summary>
    /// Log level of ErmisChatClient.
    /// </summary>
    public enum ErmisLogLevel
    {
        /// <summary>
        /// Logging is entirely disabled. This option is not recommended.
        /// </summary>
        Disabled,

        /// <summary>
        /// Only Errors and Exception. Recommended for production mode.
        /// </summary>
        FailureOnly,

        /// <summary>
        /// All logs will be emitted. Recommended for development or production mode.
        /// </summary>
        All,

        /// <summary>
        /// Additional logs will be emitted. Useful when debugging the ErmisChatClient or internal WebSocket connection.
        /// </summary>
        Debug
    }

    /// <summary>
    /// Extensions for <see cref="ErmisLogLevel"/>
    /// </summary>
    internal static class ErmisLogsLevelExt
    {
        public static LogLevel ToLogLevel(this ErmisLogLevel ermisLogLevel)
        {
            switch (ermisLogLevel)
            {
                case ErmisLogLevel.Disabled: return LogLevel.Disabled;
                case ErmisLogLevel.FailureOnly: return LogLevel.FailureOnly;
                case ErmisLogLevel.All:
                case ErmisLogLevel.Debug: return LogLevel.All;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ermisLogLevel), ermisLogLevel, null);
            }
        }

        public static bool IsDebugEnabled(this ErmisLogLevel ermisLogLevel) => ermisLogLevel == ErmisLogLevel.Debug;
    }
}