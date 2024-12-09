using System.Collections.Generic;
using System.Linq;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.Type"/>
    /// </summary>
    public sealed class ChannelFieldType : BaseFieldToFilter
    {
        public override string FieldName => "type";

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Type"/> state is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(ChannelType channelType)
            => InternalEqualsTo(channelType.ToString());

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Type"/> is EQUAL to ANY of provided values
        /// </summary>
        public FieldFilterRule In(IEnumerable<ChannelType> channelTypes)
            => InternalIn(channelTypes.Select(_ => _.ToString()));
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Type"/> is EQUAL to ANY of provided values
        /// </summary>
        public FieldFilterRule In(params ChannelType[] channelTypes)
            => InternalIn(channelTypes.Select(_ => _.ToString()));
    }
}