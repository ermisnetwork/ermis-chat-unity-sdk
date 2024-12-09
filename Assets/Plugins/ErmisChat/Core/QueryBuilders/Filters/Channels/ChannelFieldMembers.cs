using System.Collections.Generic;
using System.Linq;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel.Members"/>
    /// </summary>
    public sealed class ChannelFieldMembers : BaseFieldToFilter
    {
        public override string FieldName => "members";

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Members"/> contains a user with provided user ID
        /// </summary>
        public FieldFilterRule EqualsTo(string userId) => InternalEqualsTo(userId);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Members"/> contain any of the users with provided user IDs
        /// </summary>
        public FieldFilterRule In(IEnumerable<string> userIds) => InternalIn(userIds);
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Members"/> contain any of the users with provided user IDs
        /// </summary>
        public FieldFilterRule In(params string[] userIds) => InternalIn(userIds);

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Members"/> contain any of the users with provided user IDs
        /// </summary>
        public FieldFilterRule In(IEnumerable<IErmisUser> userIds)
            => InternalIn(userIds.Select(_ => _.Id));
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Members"/> contain any of the users with provided user IDs
        /// </summary>
        public FieldFilterRule In(params IErmisUser[] userIds)
            => InternalIn(userIds.Select(_ => _.Id));

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Members"/> contain any of the users with provided user IDs
        /// </summary>
        public FieldFilterRule In(IEnumerable<IErmisChannelMember> userIds)
            => InternalIn(userIds.Select(_ => _.User.Id));
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.Members"/> contain any of the users with provided user IDs
        /// </summary>
        public FieldFilterRule In(params IErmisChannelMember[] userIds)
            => InternalIn(userIds.Select(_ => _.User.Id));
    }
}