using System;
using System.Collections.Generic;

namespace Ermis.Core.Web
{
    /// <summary>
    /// Requests Uri Factory
    /// </summary>
    internal interface IRequestUriFactory
    {
        Uri CreateConnectionUri();

        Uri CreateEndpointUri(string endpoint, Dictionary<string, string> parameters = null);
    }
}