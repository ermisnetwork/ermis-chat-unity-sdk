using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.Frozen"/>
    /// </summary>
    public sealed class ChannelFieldFrozen : BaseFieldToFilter
    {
        public override string FieldName => "frozen";

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Frozen"/> state is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(bool isFrozen) => InternalEqualsTo(isFrozen);
    }
}