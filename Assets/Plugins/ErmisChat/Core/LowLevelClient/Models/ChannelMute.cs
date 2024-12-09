using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;

namespace Ermis.Core.LowLevelClient.Models
{
    public class ChannelMute : ModelBase, ILoadableFrom<ChannelMuteInternalDTO, ChannelMute>
    {
        public Channel Channel { get; set; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Date/time of mute expiration
        /// </summary>
        public System.DateTimeOffset? Expires { get; set; }

        /// <summary>
        /// Date/time of the last update
        /// </summary>
        public System.DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Owner of channel mute
        /// </summary>
        public User User { get; set; }

        ChannelMute ILoadableFrom<ChannelMuteInternalDTO, ChannelMute>.LoadFromDto(ChannelMuteInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            CreatedAt = dto.CreatedAt;
            Expires = dto.Expires;
            UpdatedAt = dto.UpdatedAt;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}