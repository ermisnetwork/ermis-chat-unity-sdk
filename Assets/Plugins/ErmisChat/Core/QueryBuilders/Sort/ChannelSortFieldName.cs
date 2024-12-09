using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Sort
{
    /// <summary>
    /// Fields that you can use to sort <see cref="IErmisChannel"/> query results when using <see cref="IErmisChatClient.QueryChannelsAsync"/>
    /// </summary>
    public enum ChannelSortFieldName
    {
        LastUpdated,
        LastMessageAt,
        UpdatedAt,
        CreatedAt,
        MemberCount,
        UnreadCount,
        HasUnread
    }
}