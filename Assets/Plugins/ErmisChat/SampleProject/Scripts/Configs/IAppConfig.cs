namespace ErmisChat.SampleProject.Configs
{
    public interface IAppConfig
    {
        IViewFactoryConfig ViewFactoryConfig { get; }
        IEmojiConfig Emojis { get; }
    }
}