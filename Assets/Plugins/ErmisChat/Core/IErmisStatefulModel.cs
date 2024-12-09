using Ermis.Core.StatefulModels;

namespace Ermis.Core
{
    /// <summary>
    /// Model with its state being automatically updated by the <see cref="ErmisChatClient"/>
    ///
    /// This means that this objects corresponds to an object on the Ermis Chat server with the same ID
    /// its state will be automatically updated whenever new information is received from the server
    /// </summary>
    public interface IErmisStatefulModel
    {
        string UniqueId { get; }

        
        IErmisCustomData CustomData { get; }
    }
}