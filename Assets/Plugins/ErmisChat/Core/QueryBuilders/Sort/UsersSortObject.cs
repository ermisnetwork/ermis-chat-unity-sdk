namespace Ermis.Core.QueryBuilders.Sort
{
    /// <summary>
    /// Sort object for <see cref="IErmisUser"/> query: <see cref="IErmisChatClient.QueryUsersAsync"/>
    /// </summary>
    public sealed class UsersSortObject : QuerySort<UsersSortObject, UserSortField>
    {
        protected override UsersSortObject Instance => this;

        protected override string ToUnderlyingFieldName(UserSortField field) => field.FieldName;
    }
}