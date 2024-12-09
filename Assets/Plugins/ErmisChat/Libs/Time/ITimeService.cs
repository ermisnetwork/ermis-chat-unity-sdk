using System;

namespace Ermis.Libs.Time
{
    /// <summary>
    /// Provides time information
    /// </summary>
    public interface ITimeService
    {
        float Time { get; }
        float DeltaTime { get; }
        DateTimeOffset Now { get; }
    }
}