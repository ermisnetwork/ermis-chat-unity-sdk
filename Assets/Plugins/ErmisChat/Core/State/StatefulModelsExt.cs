using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.State
{
    internal static class StatefulModelsExt
    {
        [Pure]
        public static List<string> ToUserIdsListOrNull(this IEnumerable<IErmisUser> users) => users?.Select(x => x.Id).ToList();
    }
}