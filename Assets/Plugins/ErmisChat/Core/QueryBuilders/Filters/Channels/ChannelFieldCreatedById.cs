using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisUser.Id"/> of a user who created the <see cref="IErmisChannel"/>
    /// </summary>
    public sealed class ChannelFieldCreatedById : BaseFieldToFilter
    {
        public override string FieldName => "created_by_id";
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.CreatedBy"/> is EQUAL to the provided user ID
        /// </summary>
        public FieldFilterRule EqualsTo(string userId) => InternalEqualsTo(userId);
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.CreatedBy"/> is EQUAL to the provided user
        /// </summary>
        public FieldFilterRule EqualsTo(IErmisUser user) => InternalEqualsTo(user.Id);
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel.CreatedBy"/> is EQUAL to the local user
        /// </summary>
        public FieldFilterRule EqualsTo(IErmisLocalUserData localUserData) => InternalEqualsTo(localUserData.UserId);
    }
}