using System.Collections.Generic;
using System.Linq;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.Id"/>. If possible, you should filter by <see cref="IErmisChannel.Cid"/> which is much faster
    /// </summary>
    public sealed class ChannelFieldId : BaseFieldToFilter
    {
        public override string FieldName => "id";
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Id"/> is EQUAL to provided channel Id. If possible, you should filter by <see cref="IErmisChannel.Cid"/> which is much faster
        /// </summary>
        public FieldFilterRule EqualsTo(string channelCid) => InternalEqualsTo(channelCid);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Id"/> is EQUAL to ANY of provided channel Id. If possible, you should filter by <see cref="IErmisChannel.Cid"/> which is much faster
        /// </summary>
        public FieldFilterRule In(IEnumerable<string> channelCids) => InternalIn(channelCids);
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Id"/> is EQUAL to ANY of provided channel Id. If possible, you should filter by <see cref="IErmisChannel.Cid"/> which is much faster
        /// </summary>
        public FieldFilterRule In(params string[] channelCids) => InternalIn(channelCids);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Id"/> is EQUAL to ANY of the provided channels Id. If possible, you should filter by <see cref="IErmisChannel.Cid"/> which is much faster
        /// </summary>
        public FieldFilterRule In(IEnumerable<IErmisChannel> channels)
            => InternalIn(channels.Select(_ => _.Id));

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Id"/> is EQUAL to ANY of the provided channels Id. If possible, you should filter by <see cref="IErmisChannel.Cid"/> which is much faster
        /// </summary>
        public FieldFilterRule In(params IErmisChannel[] channels)
            => InternalIn(channels.Select(_ => _.Id));
    }
}