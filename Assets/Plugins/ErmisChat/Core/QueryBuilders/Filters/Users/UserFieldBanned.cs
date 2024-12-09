using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Users
{
    /// <summary>
    /// Filter by <see cref="IErmisUser.Banned"/>
    /// </summary>
    public sealed class UserFieldBanned : BaseFieldToFilter
    {
        public override string FieldName => "banned";
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Banned"/> state is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(bool isBanned) => InternalEqualsTo(isBanned);
    }
}