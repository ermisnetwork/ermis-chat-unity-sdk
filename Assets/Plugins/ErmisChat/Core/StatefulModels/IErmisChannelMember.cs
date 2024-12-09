using System;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.StatefulModels
{
    /// <summary>
    /// <see cref="IErmisUser"/> that became a member of the <see cref="IErmisChannel"/>. Check <see cref="IErmisChannel.Members"/>
    /// </summary>
    public interface IErmisChannelMember : IErmisStatefulModel
    {
        public bool? Banned { get; }

        public bool? Blocked { get; }

        /// <summary>
        /// Role of the member in the channel
        /// </summary>
        public string ChannelRole { get; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        public DateTimeOffset UpdatedAt { get; }

        public IErmisUser User { get; }

        public string UserId { get; }
    }
}