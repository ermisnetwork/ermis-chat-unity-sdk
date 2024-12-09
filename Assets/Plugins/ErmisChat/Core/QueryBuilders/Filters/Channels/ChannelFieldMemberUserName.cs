using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisUser.Name"/> who is a member of 
    /// </summary>
    public sealed class ChannelFieldMemberUserName : BaseFieldToFilter
    {
        public override string FieldName => "member.user.name";
        
        /// <summary>
        /// Return only channels where a MEMBER <see cref="IErmisUser.Name"/> is EQUAL to the provided value
        /// </summary>
        public FieldFilterRule EqualsTo(string userName) => InternalEqualsTo(userName);
        
        /// <summary>
        /// Return only channels where a MEMBER <see cref="IErmisUser.Name"/> is EQUAL to the provided user
        /// </summary>
        public FieldFilterRule EqualsTo(IErmisUser user) => InternalEqualsTo(user.Name);
        
        /// <summary>
        /// Return only channels where a MEMBER <see cref="IErmisUser.Name"/> CONTAINS provided phrase anywhere. Example: "An" would match "Anna", "Anatoly", "Annabelle" because they all start with "An"
        /// </summary>
        public FieldFilterRule Autocomplete(string phrase) => InternalAutocomplete(phrase);
    }
}