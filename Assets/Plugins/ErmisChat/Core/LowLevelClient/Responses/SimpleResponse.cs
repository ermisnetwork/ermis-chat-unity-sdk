using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class SimpleResponse : ResponseObjectBase, ILoadableFrom<SimpleResponseInternalDTO, SimpleResponse>
    {
        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        SimpleResponse ILoadableFrom<SimpleResponseInternalDTO, SimpleResponse>.LoadFromDto(SimpleResponseInternalDTO dto)
        {
            Duration = dto.Duration;
            return this;
        }
    }
}