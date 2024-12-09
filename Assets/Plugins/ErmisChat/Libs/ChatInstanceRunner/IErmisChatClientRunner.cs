namespace Ermis.Libs.ChatInstanceRunner
{
    /// <summary>
    /// Runner is responsible for calling callbacks on the <see cref="IErmisChatClientEventsListener"/>
    /// </summary>
    public interface IErmisChatClientRunner
    {
        /// <summary>
        /// Pass environment callbacks to the <see cref="IErmisChatClientEventsListener"/> and react to its events
        /// </summary>
        void RunChatInstance(IErmisChatClientEventsListener ermisChatInstance);
    }
}