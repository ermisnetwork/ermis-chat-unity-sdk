using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Responses
{
    public sealed class ErmisChannelUnreadCounts  : ILoadableFrom<UnreadCountsChannelInternalDTO, ErmisChannelUnreadCounts>
    {
        /// <summary>
        /// CID of the channel
        /// </summary>
        public string ChannelCid { get; private set; }

        /// <summary>
        /// DateTimeOffset of the last read message
        /// </summary>
        public System.DateTimeOffset LastRead { get; private set; }

        /// <summary>
        /// Count of unread messages
        /// </summary>
        public int UnreadCount { get; private set; }
        
        ErmisChannelUnreadCounts ILoadableFrom<UnreadCountsChannelInternalDTO, ErmisChannelUnreadCounts>.LoadFromDto(UnreadCountsChannelInternalDTO dto)
        {
            ChannelCid = dto.ChannelId;
            LastRead = dto.LastRead;
            UnreadCount = dto.UnreadCount;

            return this;
        }
    }
}