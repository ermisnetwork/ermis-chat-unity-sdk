using Ermis.Core;
using ErmisChat.SampleProject.Configs;
using ErmisChat.SampleProject.Inputs;
using ErmisChat.SampleProject.Utils;
using ErmisChat.SampleProject.Views;

namespace ErmisChat.SampleProject
{
    /// <summary>
    /// Context for view with state and common services
    /// </summary>
    public interface IChatViewContext
    {
        IErmisChatClient Client { get; }
        IImageLoader ImageLoader { get; }
        IViewFactory Factory { get; }
        IChatState State { get; }
        IInputSystem InputSystem { get; }
        IAppConfig AppConfig { get; }
    }
}