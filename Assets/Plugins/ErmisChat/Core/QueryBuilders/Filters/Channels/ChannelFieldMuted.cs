using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.Muted"/>
    /// </summary>
    public sealed class ChannelFieldMuted : BaseFieldToFilter
    {
        public override string FieldName => "muted";
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Muted"/> state is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(bool isMuted) => InternalEqualsTo(isMuted);
    }
}