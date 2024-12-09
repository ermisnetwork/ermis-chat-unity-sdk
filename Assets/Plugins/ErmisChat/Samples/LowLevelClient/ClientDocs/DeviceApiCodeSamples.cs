using System.Threading.Tasks;
using Ermis.Core.LowLevelClient;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.LowLevelClient.Requests;
using UnityEngine;

namespace ErmisChat.Samples.LowLevelClient.ClientDocs
{
   
    internal class DeviceApiCodeSamples
    {
        public async Task AddDeviceAsync()
        {
            await Client.DeviceApi.AddDeviceAsync(new CreateDeviceRequest
            {
                //Device ID provided by the notifications provider e.g. Token provided by Firebase Messaging SDK
                Id = "unique-device-id", 
                PushProvider = PushProviderType.Firebase,
            });
        }

        public async Task ListDevicesAsync()
        {
            var response = await Client.DeviceApi.ListDevicesAsync(Client.UserId);
            foreach (var userDevice in response.Devices)
            {
                Debug.Log(userDevice.Id); // Unique Device ID provided by push notifications provider
                Debug.Log(userDevice.CreatedAt);
                Debug.Log(userDevice.PushProvider); //E.g. Firebase
                Debug.Log(userDevice.Disabled);
                Debug.Log(userDevice.DisabledReason);
            }
        }

        public async Task RemoveDeviceAsync()
        {
            var deviceId = "unique-device-id";
            await Client.DeviceApi.RemoveDeviceAsync(deviceId, Client.UserId);
        }
        
        private IErmisChatLowLevelClient Client;
    }
    
    //ErmisTodo: Update code samples. They refer to Client as LLC but should be using IErmisChatClient
}