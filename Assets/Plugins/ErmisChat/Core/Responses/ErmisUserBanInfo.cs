using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.State;
using Ermis.Core.State.Caches;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.Responses
{
    public sealed class ErmisUserBanInfo : IStateLoadableFrom<BanResponseInternalDTO, ErmisUserBanInfo>
    {
        /// <summary>
        /// User that got banned
        /// </summary>
        public IErmisUser User { get; private set; }

        /// <summary>
        /// Channel from which user got banned
        /// </summary>
        public IErmisChannel Channel { get; private set; }

        /// <summary>
        /// Date when user got banned
        /// </summary>
        public System.DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// Ban expiration date. No expiration date means ban is permanent
        /// </summary>
        public System.DateTimeOffset? Expires { get; private set; }

        /// <summary>
        /// Reason why user got banned
        /// </summary>
        public string Reason { get; private set; }

        /// <summary>
        /// Is this a shadow ban
        /// </summary>
        public bool? Shadow { get; private set; }

        /// <summary>
        /// The user who created the ban
        /// </summary>
        public IErmisUser BannedBy { get; private set; }

        ErmisUserBanInfo IStateLoadableFrom<BanResponseInternalDTO, ErmisUserBanInfo>.LoadFromDto(
            BanResponseInternalDTO dto, ICache cache)
        {
            BannedBy = cache.TryCreateOrUpdate(dto.BannedBy);
            Channel = cache.TryCreateOrUpdate(dto.Channel);
            CreatedAt = dto.CreatedAt;
            Expires = dto.Expires;
            Reason = dto.Reason;
            Shadow = dto.Shadow;
            User = cache.TryCreateOrUpdate(dto.User);
                
            return this;
        }
    }
}