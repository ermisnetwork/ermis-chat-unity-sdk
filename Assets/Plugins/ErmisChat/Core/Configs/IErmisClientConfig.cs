using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Configs
{
    /// <summary>
    /// Configuration for <see cref="IErmisChatLowLevelClient"/>
    /// </summary>
    public interface IErmisClientConfig
    {
    
        ErmisLogLevel LogLevel { get; set; }
    }
}