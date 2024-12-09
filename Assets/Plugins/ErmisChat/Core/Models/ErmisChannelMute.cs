using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.Models
{
    public class ErmisChannelMute : IStateLoadableFrom<ChannelMuteInternalDTO, ErmisChannelMute>
    {
        public IErmisChannel Channel { get; private set; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// Date/time of mute expiration
        /// </summary>
        public System.DateTimeOffset? Expires { get; private set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        public System.DateTimeOffset? UpdatedAt { get; private set; }

        /// <summary>
        /// Owner of channel mute
        /// </summary>
        public IErmisUser User { get; private set; }

        ErmisChannelMute IStateLoadableFrom<ChannelMuteInternalDTO, ErmisChannelMute>.LoadFromDto(ChannelMuteInternalDTO dto, ICache cache)
        {
            Channel = cache.TryCreateOrUpdate(dto.Channel);
            CreatedAt = dto.CreatedAt;
            Expires = dto.Expires;
            UpdatedAt = dto.UpdatedAt;
            User = cache.TryCreateOrUpdate(dto.User);

            return this;
        }
    }
}