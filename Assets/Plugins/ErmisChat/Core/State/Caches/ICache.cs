using Ermis.Core.StatefulModels;

namespace Ermis.Core.State.Caches
{
    internal interface ICache
    {
        ICacheRepository<ErmisChannel> Channels { get; }
        ICacheRepository<ErmisMessage> Messages { get; }
        ICacheRepository<ErmisUser> Users { get; }
        ICacheRepository<ErmisLocalUserData> LocalUser { get; }
        ICacheRepository<ErmisChannelMember> ChannelMembers { get; }
    }
}