using System;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.StatefulModels
{
    internal sealed class ErmisChannelMember : ErmisStatefulModelBase<ErmisChannelMember>,
        IUpdateableFrom<ChannelMemberInternalDTO, ErmisChannelMember>, IErmisChannelMember, IUpdateableFrom<ChannelMember, ErmisChannelMember>
    {
        public bool? Banned { get; private set; }

        public bool? Blocked { get; private set; }

        /// <summary>
        /// Role of the member in the channel
        /// </summary>
        public string ChannelRole { get; private set; }

        /// <summary>
        /// Date/time of creation
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }

        public IErmisUser User { get; private set; }

        public string UserId { get; private set; }

        //ErmisTodo: this object should not inherit custom data, it seems there's no way to set it for a member

        void IUpdateableFrom<ChannelMemberInternalDTO, ErmisChannelMember>.UpdateFromDto(ChannelMemberInternalDTO dto,
            ICache cache)
        {
            Banned = GetOrDefault(dto.Banned, Banned);
            ChannelRole = GetOrDefault(dto.ChannelRole, ChannelRole);
            CreatedAt = GetOrDefault(dto.CreatedAt, CreatedAt);
            UpdatedAt = GetOrDefault(dto.UpdatedAt, UpdatedAt);
            User = cache.TryCreateOrUpdate(dto.User);
            UserId = GetOrDefault(dto.UserId, UserId);
        }

        void IUpdateableFrom<ChannelMember, ErmisChannelMember>.UpdateFromDto(ChannelMember dto,
            ICache cache)
        {
            Banned = GetOrDefault(dto.Banned, Banned);
            ChannelRole = GetOrDefault(dto.ChannelRole, ChannelRole);
            CreatedAt = GetOrDefault(dto.CreatedAt, CreatedAt);
            UpdatedAt = GetOrDefault(dto.UpdatedAt, UpdatedAt);
            UserId = GetOrDefault(dto.UserId, UserId);
        }

        internal ErmisChannelMember(string uniqueId, ICacheRepository<ErmisChannelMember> repository,
            IStatefulModelContext context)
            : base(uniqueId, repository, context)
        {
        }

        protected override string InternalUniqueId
        {
            get => UserId;
            set => UserId = value;
        }

        protected override ErmisChannelMember Self => this;
    }
}