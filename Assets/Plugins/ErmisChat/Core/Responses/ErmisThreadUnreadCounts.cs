using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Responses
{
    public sealed class ErmisThreadUnreadCounts : ILoadableFrom<UnreadCountsThreadInternalDTO, ErmisThreadUnreadCounts>
    {
        public System.DateTimeOffset LastRead { get; private set; }

        public string LastReadMessageId { get; private set; }

        public string ParentMessageId { get; private set; }

        public int UnreadCount { get; private set; }

        ErmisThreadUnreadCounts ILoadableFrom<UnreadCountsThreadInternalDTO, ErmisThreadUnreadCounts>.LoadFromDto(UnreadCountsThreadInternalDTO dto)
        {
            LastRead = dto.LastRead;
            LastReadMessageId = dto.LastReadMessageId;
            ParentMessageId = dto.ParentMessageId;
            UnreadCount = dto.UnreadCount;

            return this;
        }
    }
}