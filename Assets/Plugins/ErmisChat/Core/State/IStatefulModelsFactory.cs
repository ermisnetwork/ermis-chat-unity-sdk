using Ermis.Core.StatefulModels;

namespace Ermis.Core.State
{
    internal interface IStatefulModelsFactory
    {
        ErmisChannel CreateErmisChannel(string uniqueId);

        ErmisChannelMember CreateErmisChannelMember(string uniqueId);

        ErmisLocalUserData CreateErmisLocalUser(string uniqueId);

        ErmisMessage CreateErmisMessage(string uniqueId);

        ErmisUser CreateErmisUser(string uniqueId);
    }
}