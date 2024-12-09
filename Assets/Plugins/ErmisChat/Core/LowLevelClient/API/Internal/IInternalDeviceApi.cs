using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal interface IInternalDeviceApi
    {
        Task<SimpleResponseInternalDTO> AddDeviceAsync(CreateDeviceRequestInternalDTO device);

        Task<ListDevicesResponseInternalDTO> ListDevicesAsync(string userId);

        Task<SimpleResponseInternalDTO> RemoveDeviceAsync(string deviceId, string userId);
    }
}