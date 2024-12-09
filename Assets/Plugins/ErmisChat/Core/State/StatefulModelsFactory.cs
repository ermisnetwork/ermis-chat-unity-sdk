using System;
using Ermis.Core.State.Caches;
using Ermis.Core.StatefulModels;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;

namespace Ermis.Core.State
{
    /// <summary>
    /// Factory for <see cref="IErmisStatefulModel"/>
    /// </summary>
    internal sealed class StatefulModelsFactory : IStatefulModelsFactory
    {
        public StatefulModelsFactory(ErmisChatClient ermisChatClient, ISerializer serializer, ILogs logs, Cache cache)
        {
            _ermisChatClient = ermisChatClient ?? throw new ArgumentNullException(nameof(ermisChatClient));
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

            _context = new StatefulModelContext(_cache, ermisChatClient, serializer, logs);
        }

        public ErmisChannel CreateErmisChannel(string uniqueId)
            => new ErmisChannel(uniqueId, _cache.Channels, _context);

        public ErmisChannelMember CreateErmisChannelMember(string uniqueId)
            => new ErmisChannelMember(uniqueId, _cache.ChannelMembers, _context);

        public ErmisLocalUserData CreateErmisLocalUser(string uniqueId)
            => new ErmisLocalUserData(uniqueId, _cache.LocalUser, _context);

        public ErmisMessage CreateErmisMessage(string uniqueId)
            => new ErmisMessage(uniqueId, _cache.Messages, _context);

        public ErmisUser CreateErmisUser(string uniqueId)
            => new ErmisUser(uniqueId, _cache.Users, _context);

        private readonly ILogs _logs;
        private readonly ErmisChatClient _ermisChatClient;
        private readonly IStatefulModelContext _context;
        private readonly ICache _cache;
    }
}