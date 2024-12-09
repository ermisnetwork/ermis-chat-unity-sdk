using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class ListDevicesResponse : ResponseObjectBase, ILoadableFrom<ListDevicesResponseInternalDTO, ListDevicesResponse>
    {
        /// <summary>
        /// List of devices
        /// </summary>
        public System.Collections.Generic.List<Device> Devices { get; set; }

        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        ListDevicesResponse ILoadableFrom<ListDevicesResponseInternalDTO, ListDevicesResponse>.LoadFromDto(ListDevicesResponseInternalDTO dto)
        {
            Devices = Devices.TryLoadFromDtoCollection(dto.Devices);
            Duration = dto.Duration;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}