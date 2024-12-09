using ErmisChat.SampleProject.Popups;
using ErmisChat.SampleProject.Views;

namespace ErmisChat.SampleProject.Configs
{
    /// <summary>
    /// Config for <see cref="IViewFactory"/>
    /// </summary>
    public interface IViewFactoryConfig
    {
        MessageOptionsPopup MessageOptionsPopupPrefab { get; }
        CreateNewChannelFormPopup CreateNewChannelFormPopupPrefab { get; }
        InviteChannelMembersPopup InviteChannelMembersPopupPrefab { get; }
        ErrorPopup ErrorPopupPrefab { get; }
        InviteReceivedPopup InviteReceivedPopup { get; }
    }
}