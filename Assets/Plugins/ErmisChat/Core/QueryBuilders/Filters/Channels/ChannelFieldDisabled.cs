using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.Disabled"/>
    /// </summary>
    public sealed class ChannelFieldDisabled : BaseFieldToFilter
    {
        public override string FieldName => "disabled";
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Disabled"/> state is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(bool isDisabled) => InternalEqualsTo(isDisabled);
    }
}