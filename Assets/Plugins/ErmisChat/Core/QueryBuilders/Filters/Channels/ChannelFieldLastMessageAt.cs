using System;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.LastMessageAt"/>
    /// </summary>
    public sealed class ChannelFieldLastMessageAt : BaseFieldToFilter
    {
        public override string FieldName => "last_message_at";

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is EQUAL to the provided one
        /// </summary>
        public FieldFilterRule EqualsTo(DateTime lastMessageAt) => InternalEqualsTo(lastMessageAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is EQUAL to the provided one
        /// </summary>
        public FieldFilterRule EqualsTo(DateTimeOffset lastMessageAt) => InternalEqualsTo(lastMessageAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is GREATER THAN the provided one
        /// </summary>
        public FieldFilterRule GreaterThan(DateTime lastMessageAt) => InternalGreaterThan(lastMessageAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is GREATER THAN the provided one
        /// </summary>
        public FieldFilterRule GreaterThan(DateTimeOffset lastMessageAt) => InternalGreaterThan(lastMessageAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is GREATER THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule GreaterThanOrEquals(DateTime lastMessageAt)
            => InternalGreaterThanOrEquals(lastMessageAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is GREATER THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule GreaterThanOrEquals(DateTimeOffset lastMessageAt)
            => InternalGreaterThanOrEquals(lastMessageAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is LESS THAN the provided one
        /// </summary>
        public FieldFilterRule LessThan(DateTime lastMessageAt) => InternalLessThan(lastMessageAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is LESS THAN the provided one
        /// </summary>
        public FieldFilterRule LessThan(DateTimeOffset lastMessageAt) => InternalLessThan(lastMessageAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is LESS THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule LessThanOrEquals(DateTime lastMessageAt) => InternalLessThanOrEquals(lastMessageAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.LastMessageAt"/> is LESS THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule LessThanOrEquals(DateTimeOffset lastMessageAt)
            => InternalLessThanOrEquals(lastMessageAt);
    }
}