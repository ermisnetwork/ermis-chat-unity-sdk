using System.Collections.Generic;
using System.Linq;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Users
{
    /// <summary>
    /// Filter by <see cref="IErmisUser.Id"/>
    /// </summary>
    public sealed class UserFieldId : BaseFieldToFilter
    {
        public override string FieldName => "id";

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is EQUAL to provided user Id
        /// </summary>
        public FieldFilterRule EqualsTo(string userId) => InternalEqualsTo(userId);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is EQUAL to provided user
        /// </summary>
        public FieldFilterRule EqualsTo(IErmisUser user) => InternalEqualsTo(user.Id);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is EQUAL to ANY of provided user Id
        /// </summary>
        public FieldFilterRule In(IEnumerable<string> userIds) => InternalIn(userIds);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is EQUAL to ANY of provided user Id
        /// </summary>
        public FieldFilterRule In(params string[] userIds) => InternalIn(userIds);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is EQUAL to ANY of the provided user Id
        /// </summary>
        public FieldFilterRule In(IEnumerable<IErmisUser> users)
            => InternalIn(users.Select(_ => _.Id));

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is EQUAL to ANY of the provided user Id
        /// </summary>
        public FieldFilterRule In(params IErmisUser[] users)
            => InternalIn(users.Select(_ => _.Id));
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is GREATER THAN the provided one
        /// </summary>
        public FieldFilterRule GreaterThan(string userId) => InternalGreaterThan(userId);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is GREATER THAN the provided one
        /// </summary>
        public FieldFilterRule GreaterThan(IErmisUser user) => InternalGreaterThan(user.Id);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is GREATER THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule GreaterThanOrEquals(string userId) => InternalGreaterThanOrEquals(userId);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is GREATER THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule GreaterThanOrEquals(IErmisUser user) => InternalGreaterThanOrEquals(user.Id);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is LESS THAN the provided one
        /// </summary>
        public FieldFilterRule LessThan(string userId) => InternalLessThan(userId);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is LESS THAN the provided one
        /// </summary>
        public FieldFilterRule LessThan(IErmisUser user) => InternalLessThan(user.Id);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is LESS THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule LessThanOrEquals(string userId) => InternalLessThanOrEquals(userId);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Id"/> is LESS THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule LessThanOrEquals(IErmisUser user) => InternalLessThanOrEquals(user.Id);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Name"/> CONTAINS provided phrase anywhere. Example: "An" would match "Anna", "Anatoly", "Annabelle" because they all start with "An"
        /// </summary>
        public FieldFilterRule Autocomplete(string phrase) => InternalAutocomplete(phrase);
    }
}