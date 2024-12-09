using System.Threading.Tasks;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.API
{
    /// <summary>
    /// API Client that can be used to retrieve, create and alter push notification devices of a Ermis Chat application.
    /// </summary>
    public interface IDeviceApi
    {
        /// <summary>
        /// <para>Adds a new device.</para>
        /// Registering a device associates it with a user and tells the
        /// push provider to send new message notifications to the device.
        /// </summary>
        Task<SimpleResponse> AddDeviceAsync(CreateDeviceRequest device);

        /// <summary>
        /// Provides a list of all devices associated with a user.
        /// </summary>
        Task<ListDevicesResponse> ListDevicesAsync(string userId);

        /// <summary>
        /// <para>Removes a device.</para>
        /// Unregistering a device removes the device from the user and stops further new message notifications.
        /// </summary>
        Task<SimpleResponse> RemoveDeviceAsync(string deviceId, string userId);
    }
}