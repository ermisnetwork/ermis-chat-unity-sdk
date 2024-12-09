using System.Collections.Generic;

namespace Ermis.Core.Web
{
    internal class QueryParameters : Dictionary<string, string>
    {
        public static QueryParameters Default => new QueryParameters();
    }
}