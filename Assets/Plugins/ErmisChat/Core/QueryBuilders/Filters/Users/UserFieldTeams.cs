using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Users
{
    /// <summary>
    /// Filter by <see cref="IErmisUser.Teams"/>
    /// </summary>
    public sealed class UserFieldTeams : BaseFieldToFilter
    {
        public override string FieldName => "teams";
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Teams"/> is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(string team) => InternalEqualsTo(team);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Name"/> CONTAINS provided phrase anywhere. Example: "An" would match "Anna", "Anatoly", "Annabelle" because they all start with "An"
        /// </summary>
        public FieldFilterRule Contains(string team) => InternalContains(team);
    }
}