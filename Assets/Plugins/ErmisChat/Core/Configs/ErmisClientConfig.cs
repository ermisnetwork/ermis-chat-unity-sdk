namespace Ermis.Core.Configs
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public class ErmisClientConfig : IErmisClientConfig
    {
        public static IErmisClientConfig Default { get; set; } = new ErmisClientConfig();

        public ErmisLogLevel LogLevel { get; set; } = ErmisLogLevel.FailureOnly;
    }
}