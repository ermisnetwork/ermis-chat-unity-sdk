using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Events
{
    public partial class EventHealthCheck : EventBase, ILoadableFrom<HealthCheckEventInternalDTO, EventHealthCheck>
    {
        public string Cid { get; set; }

        public OwnUser Me { get; set; }

        public string Type { get; set; }

        public string ConnectionId { get; set; }

        EventHealthCheck ILoadableFrom<HealthCheckEventInternalDTO, EventHealthCheck>.LoadFromDto(HealthCheckEventInternalDTO dto)
        {
            Cid = dto.Cid;
            CreatedAt = dto.CreatedAt;
            Me = Me.TryLoadFromDto<OwnUserInternalDTO, OwnUser>(dto.Me);
            Type = dto.Type;
            AdditionalProperties = dto.AdditionalProperties;

            ConnectionId = dto.ConnectionId;

            return this;
        }
    }
}