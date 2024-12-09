using System;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.UpdatedAt"/>
    /// </summary>
    public sealed class ChannelFieldUpdatedAt : BaseFieldToFilter
    {
        public override string FieldName => "updated_at";

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is EQUAL to the provided one
        /// </summary>
        public FieldFilterRule EqualsTo(DateTime updatedAt) => InternalEqualsTo(updatedAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is EQUAL to the provided one
        /// </summary>
        public FieldFilterRule EqualsTo(DateTimeOffset updatedAt) => InternalEqualsTo(updatedAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is GREATER THAN the provided one
        /// </summary>
        public FieldFilterRule GreaterThan(DateTime updatedAt) => InternalGreaterThan(updatedAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is GREATER THAN the provided one
        /// </summary>
        public FieldFilterRule GreaterThan(DateTimeOffset updatedAt) => InternalGreaterThan(updatedAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is GREATER THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule GreaterThanOrEquals(DateTime updatedAt) => InternalGreaterThanOrEquals(updatedAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is GREATER THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule GreaterThanOrEquals(DateTimeOffset updatedAt) => InternalGreaterThanOrEquals(updatedAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is LESS THAN the provided one
        /// </summary>
        public FieldFilterRule LessThan(DateTime updatedAt) => InternalLessThan(updatedAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is LESS THAN the provided one
        /// </summary>
        public FieldFilterRule LessThan(DateTimeOffset updatedAt) => InternalLessThan(updatedAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is LESS THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule LessThanOrEquals(DateTime updatedAt) => InternalLessThanOrEquals(updatedAt);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.UpdatedAt"/> is LESS THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule LessThanOrEquals(DateTimeOffset updatedAt) => InternalLessThanOrEquals(updatedAt);
    }
}