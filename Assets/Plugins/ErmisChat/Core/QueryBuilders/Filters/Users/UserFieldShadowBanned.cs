using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Users
{
    /// <summary>
    /// Filter by <see cref="IErmisUser.ShadowBanned"/>
    /// </summary>
    public sealed class UserFieldShadowBanned : BaseFieldToFilter
    {
        public override string FieldName => "shadow_banned";
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.ShadowBanned"/> state is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(bool isShadowBanned) => InternalEqualsTo(isShadowBanned);
    }
}