﻿using System;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Sort
{
    /// <summary>
    /// Sort object for <see cref="IErmisChannel"/> query: <see cref="IErmisChatClient.QueryChannelsAsync"/>
    /// </summary>
    public sealed class ChannelSortObject : QuerySort<ChannelSortObject, ChannelSortFieldName>
    {
        protected override ChannelSortObject Instance => this;

        protected override string ToUnderlyingFieldName(ChannelSortFieldName fieldName)
        {
            switch (fieldName)
            {
                case ChannelSortFieldName.LastUpdated: return "last_updated";
                case ChannelSortFieldName.LastMessageAt: return "last_message_at";
                case ChannelSortFieldName.UpdatedAt: return "updated_at";
                case ChannelSortFieldName.CreatedAt: return "created_at";
                case ChannelSortFieldName.MemberCount: return "member_count";
                case ChannelSortFieldName.UnreadCount: return "unread_count ";
                case ChannelSortFieldName.HasUnread: return "has_unread";
                default:
                    throw new ArgumentOutOfRangeException(nameof(fieldName), fieldName, null);
            }
        }
    }
}