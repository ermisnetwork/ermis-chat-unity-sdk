using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.Team"/>
    /// </summary>
    public sealed class ChannelFieldTeam : BaseFieldToFilter
    {
        public override string FieldName => "team";
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Team"/> state is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(string team) => InternalEqualsTo(team);
    }
}