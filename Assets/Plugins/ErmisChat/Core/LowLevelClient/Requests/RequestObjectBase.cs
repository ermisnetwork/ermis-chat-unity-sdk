using System.Collections.Generic;

namespace Ermis.Core.LowLevelClient.Requests
{
    public abstract class RequestObjectBase
    {
        public Dictionary<string, object> AdditionalProperties { get; set; }
    }
}