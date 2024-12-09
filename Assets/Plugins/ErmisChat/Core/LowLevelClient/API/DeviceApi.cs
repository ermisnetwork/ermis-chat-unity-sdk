using System;
using System.Threading.Tasks;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.API.Internal;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.API
{
    internal class DeviceApi : IDeviceApi
    {
        public DeviceApi(IInternalDeviceApi internalDeviceApi)
        {
            _internalDeviceApi = internalDeviceApi ?? throw new ArgumentNullException(nameof(internalDeviceApi));
        }

        public async Task<SimpleResponse> AddDeviceAsync(CreateDeviceRequest device)
        {
            var dto = await _internalDeviceApi.AddDeviceAsync(device.TrySaveToDto());
            return dto.ToDomain<SimpleResponseInternalDTO, SimpleResponse>();
        }

        public async Task<ListDevicesResponse> ListDevicesAsync(string userId)
        {
            var dto = await _internalDeviceApi.ListDevicesAsync(userId);
            return dto.ToDomain<ListDevicesResponseInternalDTO, ListDevicesResponse>();
        }


        public async Task<SimpleResponse> RemoveDeviceAsync(string deviceId, string userId)
        {
            var dto = await _internalDeviceApi.RemoveDeviceAsync(deviceId, userId);
            return dto.ToDomain<SimpleResponseInternalDTO, SimpleResponse>();
        }

        private readonly IInternalDeviceApi _internalDeviceApi;
    }
}