using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.Web;
using Ermis.Libs.Http;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal class InternalDeviceApi : InternalApiClientBase, IInternalDeviceApi
    {
        public InternalDeviceApi(IHttpClient httpClient, ISerializer serializer, ILogs logs,
            IRequestUriFactory requestUriFactory, IErmisChatLowLevelClient lowLevelClient)
            : base(httpClient, serializer, logs, requestUriFactory, lowLevelClient)
        {
        }

        public Task<SimpleResponseInternalDTO> AddDeviceAsync(CreateDeviceRequestInternalDTO device)
            => PostWithCustomHeader<CreateDeviceRequestInternalDTO, SimpleResponseInternalDTO>("devices/add", device);

        public Task<ListDevicesResponseInternalDTO> ListDevicesAsync(string userId)
        {
            var parameters = QueryParameters.Default.Append("user_id", userId);
            return GetWithCustomHeader<ListDevicesResponseInternalDTO>("devices", parameters);
        }

        public Task<SimpleResponseInternalDTO> RemoveDeviceAsync(string deviceId, string userId)
        {
            var parameters = QueryParameters.Default
                .Append("id", deviceId);

            return DeleteWithCustomHeader<SimpleResponseInternalDTO>("devices", parameters);
        }
    }
}