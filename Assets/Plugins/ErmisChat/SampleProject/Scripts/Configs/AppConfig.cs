using Ermis.Core.LowLevelClient;
using ErmisChat.SampleProject.Views;
using UnityEngine;

namespace ErmisChat.SampleProject.Configs
{
    /// <summary>
    /// Asset to keep <see cref="IViewFactory"/> config
    /// </summary>
    [CreateAssetMenu(fileName = "AppConfig", menuName = ErmisChatLowLevelClient.MenuPrefix + "View/Create app config asset", order = 1)]
    public class AppConfig : ScriptableObject, IAppConfig
    {
        public IEmojiConfig Emojis => _emojiConfig;

        public IViewFactoryConfig ViewFactoryConfig => _viewFactoryConfig;

        [SerializeField]
        private ViewFactoryConfig _viewFactoryConfig = new ViewFactoryConfig();

        [SerializeField]
        private EmojiConfigAsset _emojiConfig;
    }
}