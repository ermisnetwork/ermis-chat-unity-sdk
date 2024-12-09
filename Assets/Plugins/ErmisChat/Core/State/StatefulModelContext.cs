using Ermis.Core.State.Caches;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;

namespace Ermis.Core.State
{
    internal sealed class StatefulModelContext : IStatefulModelContext
    {
        public ICache Cache { get; }
        public ErmisChatClient Client { get; }
        public ILogs Logs { get; }
        public ISerializer Serializer { get; }

        public StatefulModelContext(ICache cache, ErmisChatClient ermisChatClient, ISerializer serializer, ILogs logs)
        {
            Cache = cache;
            Client = ermisChatClient;
            Serializer = serializer;
            Logs = logs;
        }
    }
}