using System;

namespace Ermis.Core.Exceptions
{
    /// <summary>
    /// Thrown when auth credentials are missing
    /// </summary>
    public class ErmisMissingAuthCredentialsException : Exception
    {
        public ErmisMissingAuthCredentialsException(string message)
            : base(message)
        {
        }
    }
}