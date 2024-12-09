using Ermis.Core.State.Caches;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;

namespace Ermis.Core.State
{
    internal interface IStatefulModelContext
    {
        ICache Cache { get; }
        ErmisChatClient Client { get; }
        ILogs Logs { get; }
        ISerializer Serializer { get; }
    }
}