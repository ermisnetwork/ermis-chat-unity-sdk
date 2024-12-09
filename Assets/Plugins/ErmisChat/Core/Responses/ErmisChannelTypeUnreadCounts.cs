using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Responses
{
    public sealed class ErmisChannelTypeUnreadCounts  : ILoadableFrom<UnreadCountsChannelTypeInternalDTO, ErmisChannelTypeUnreadCounts>
    {
        public int ChannelCount { get; private set; }

        public ChannelType ChannelType { get; private set; }

        public int UnreadCount { get; private set; }

        ErmisChannelTypeUnreadCounts ILoadableFrom<UnreadCountsChannelTypeInternalDTO, ErmisChannelTypeUnreadCounts>.LoadFromDto(UnreadCountsChannelTypeInternalDTO dto)
        {
            ChannelCount = dto.ChannelCount;
            ChannelType = new ChannelType(dto.ChannelType);
            UnreadCount = dto.UnreadCount;

            return this;
        }
    }
}