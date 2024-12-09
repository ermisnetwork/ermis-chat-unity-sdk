using ErmisChat.SampleProject.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace ErmisChat.SampleProject.Views
{
    /// <summary>
    /// Factory for views
    /// </summary>
    public interface IViewFactory
    {
        RectTransform PopupsContainer { get; }

        MessageOptionsPopup CreateMessageOptionsPopup(MessageView messageView, IChatState state);

        void CreateEmoji(Image prefab, Transform container, string key);

        TPopup CreateFullscreenPopup<TPopup>()
            where TPopup : BaseFullscreenPopup;
    }
}