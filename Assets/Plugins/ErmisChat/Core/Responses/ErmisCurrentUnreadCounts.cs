using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Responses
{
    /// <summary>
    /// Represents the current state of unread counts for the user. Unread counts mean how many messages and threads are unread in the channels and threads the user is participating in
    /// </summary>
    public sealed class ErmisCurrentUnreadCounts : ILoadableFrom<WrappedUnreadCountsResponseInternalDTO, ErmisCurrentUnreadCounts>
    {
        /// <summary>
        /// Unread status grouped by <see cref="ChannelType"/>. Each entry represents a channel type with unread messages among all channels of that type
        /// </summary>
        public IReadOnlyList<ErmisChannelTypeUnreadCounts> UnreadChannelsByType => _unreadChannelsByType;
        
        /// <summary>
        /// Unread status per channel. Each entry represents a channel with unread messages
        /// </summary>
        public IReadOnlyList<ErmisChannelUnreadCounts> UnreadChannels => _unreadChannels;
        
        /// <summary>
        /// Unread status per thread. Each entry represents a thread with unread messages
        /// </summary>
        public IReadOnlyList<ErmisThreadUnreadCounts> UnreadThreads => _unreadThreads;

        /// <summary>
        /// Total unread messages count
        /// </summary>
        public int TotalUnreadCount { get; private set; }

        /// <summary>
        /// Total unread threads count
        /// </summary>
        public int TotalUnreadThreadsCount { get; private set; }

        ErmisCurrentUnreadCounts ILoadableFrom<WrappedUnreadCountsResponseInternalDTO, ErmisCurrentUnreadCounts>.LoadFromDto(WrappedUnreadCountsResponseInternalDTO dto)
        {
            _unreadChannelsByType = _unreadChannelsByType.TryLoadFromDtoCollection(dto.ChannelType);
            _unreadChannels = _unreadChannels.TryLoadFromDtoCollection(dto.Channels);
            _unreadThreads = _unreadThreads.TryLoadFromDtoCollection(dto.Threads);

            TotalUnreadCount = dto.TotalUnreadCount;
            TotalUnreadThreadsCount = dto.TotalUnreadThreadsCount;

            return this;
        }
        
        private List<ErmisChannelTypeUnreadCounts> _unreadChannelsByType = new List<ErmisChannelTypeUnreadCounts>();
        private List<ErmisChannelUnreadCounts> _unreadChannels = new List<ErmisChannelUnreadCounts>();
        private List<ErmisThreadUnreadCounts> _unreadThreads = new List<ErmisThreadUnreadCounts>();
    }
}