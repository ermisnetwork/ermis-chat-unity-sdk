using System.Collections.Generic;
using System.Linq;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Users
{
    /// <summary>
    /// Filter by <see cref="IErmisUser.Role"/>
    /// </summary>
    public sealed class UserFieldRole : BaseFieldToFilter
    {
        public override string FieldName => "role";

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is EQUAL to provided user Id
        /// </summary>
        public FieldFilterRule EqualsTo(string userRole) => InternalEqualsTo(userRole);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is EQUAL to provided user
        /// </summary>
        public FieldFilterRule EqualsTo(IErmisUser user) => InternalEqualsTo(user.Role);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is EQUAL to ANY of provided user Id
        /// </summary>
        public FieldFilterRule In(IEnumerable<string> userRoles) => InternalIn(userRoles);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is EQUAL to ANY of provided user Id
        /// </summary>
        public FieldFilterRule In(params string[] userRoles) => InternalIn(userRoles);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is EQUAL to ANY of the provided user Id
        /// </summary>
        public FieldFilterRule In(IEnumerable<IErmisUser> users)
            => InternalIn(users.Select(_ => _.Role));

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is EQUAL to ANY of the provided user Id
        /// </summary>
        public FieldFilterRule In(params IErmisUser[] users)
            => InternalIn(users.Select(_ => _.Role));
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is GREATER THAN the provided one
        /// </summary>
        public FieldFilterRule GreaterThan(string userRole) => InternalGreaterThan(userRole);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is GREATER THAN the provided one
        /// </summary>
        public FieldFilterRule GreaterThan(IErmisUser user) => InternalGreaterThan(user.Role);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is GREATER THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule GreaterThanOrEquals(string userRole) => InternalGreaterThanOrEquals(userRole);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is GREATER THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule GreaterThanOrEquals(IErmisUser user) => InternalGreaterThanOrEquals(user.Role);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is LESS THAN the provided one
        /// </summary>
        public FieldFilterRule LessThan(string userRole) => InternalLessThan(userRole);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is LESS THAN the provided one
        /// </summary>
        public FieldFilterRule LessThan(IErmisUser user) => InternalLessThan(user.Role);

        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is LESS THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule LessThanOrEquals(string userRole) => InternalLessThanOrEquals(userRole);
        
        /// <summary>
        /// Return only users where <see cref="IErmisUser.Role"/> is LESS THAN OR EQUAL to the provided one
        /// </summary>
        public FieldFilterRule LessThanOrEquals(IErmisUser user) => InternalLessThanOrEquals(user.Role);
    }
}