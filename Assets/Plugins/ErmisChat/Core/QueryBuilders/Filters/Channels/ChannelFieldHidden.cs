using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.Hidden"/>
    /// </summary>
    public sealed class ChannelFieldHidden : BaseFieldToFilter
    {
        public override string FieldName => "hidden";

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Hidden"/> state is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(bool isHidden) => InternalEqualsTo(isHidden);
    }
}