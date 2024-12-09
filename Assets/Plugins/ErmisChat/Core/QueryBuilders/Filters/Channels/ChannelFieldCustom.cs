using System;
using System.Collections.Generic;
using Ermis.Core.State;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.QueryBuilders.Filters.Channels
{
    /// <summary>
    /// Filter by <see cref="IErmisChannel"/> custom field. Custom fields can be defined in <see cref="IErmisChatClient.CreateChannelWithIdAsync"/>,
    /// <see cref="IErmisChatClient.CreateChannelWithMembersAsync"/>, or <see cref="IErmisChannel.UpdatePartialAsync"/>
    /// </summary>
    public sealed class ChannelFieldCustom : BaseFieldToFilter
    {
        public override string FieldName { get; }

        public ChannelFieldCustom(string customFieldName)
        {
            ErmisAsserts.AssertNotNullOrEmpty(customFieldName, nameof(customFieldName));
            FieldName = customFieldName;
        }

        /// <summary>
        /// Return only channels where <see cref="IErmisChannel"/> custom field has value EQUAL to the provided one
        /// </summary>
        public FieldFilterRule EqualsTo(string value) => InternalEqualsTo(value);
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel"/> custom field has value EQUAL to ANY of provided channel Id.
        /// </summary>
        public FieldFilterRule In(IEnumerable<string> values) => InternalIn(values);
        
        /// <summary>
        /// Return only channels where <see cref="IErmisChannel"/> custom field has value EQUAL to ANY of provided channel Id.
        /// </summary>
        public FieldFilterRule In(params string[] values) => InternalIn(values);
    }
}