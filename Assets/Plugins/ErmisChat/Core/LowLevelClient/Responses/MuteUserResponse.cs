using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class MuteUserResponse : ResponseObjectBase, ILoadableFrom<SimpleResponseInternalDTO, MuteUserResponse>
    {
        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Object with user mute (if one user was muted)
        /// </summary>
        public UserMute Mute { get; set; }

        /// <summary>
        /// Object with mutes (if multiple users were muted)
        /// </summary>
        public System.Collections.Generic.List<UserMute> Mutes { get; set; }

        /// <summary>
        /// Authorized user object with fresh mutes information
        /// </summary>
        public OwnUser OwnUser { get; set; }

        MuteUserResponse ILoadableFrom<SimpleResponseInternalDTO, MuteUserResponse>.LoadFromDto(SimpleResponseInternalDTO dto)
        {
            Duration = dto.Duration;
           // Mute = Mute.TryLoadFromDto(dto.Mute);
           // Mutes = Mutes.TryLoadFromDtoCollection(dto.Mutes);
           // OwnUser = OwnUser.TryLoadFromDto<OwnUserInternalDTO, OwnUser>(dto.OwnUser);
           // AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}