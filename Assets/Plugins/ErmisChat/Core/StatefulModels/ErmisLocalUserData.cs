using System.Collections.Generic;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.State;
using Ermis.Core.State.Caches;
using Ermis.Core.Models;

namespace Ermis.Core.StatefulModels
{
    internal sealed class ErmisLocalUserData : ErmisStatefulModelBase<ErmisLocalUserData>,
        IUpdateableFrom<OwnUserInternalDTO, ErmisLocalUserData>, IUpdateableFrom<WrappedUnreadCountsResponseInternalDTO, ErmisLocalUserData>, IErmisLocalUserData
    {
        #region OwnUser
        
        public IReadOnlyList<ErmisChannelMute> ChannelMutes => _channelMutes;

        public IReadOnlyList<ErmisDevice> Devices => _devices;

        public IReadOnlyList<string> LatestHiddenChannels => _latestHiddenChannels;
        
        public IReadOnlyList<ErmisUserMute> Mutes => _mutes;

        public int? TotalUnreadCount { get; private set; }

        public int? UnreadChannels { get; private set; }
        
        #endregion
        
        public IErmisUser User { get; private set; }
        public string UserId => User?.Id;

        void IUpdateableFrom<OwnUserInternalDTO, ErmisLocalUserData>.UpdateFromDto(OwnUserInternalDTO dto,
            ICache cache)
        {
            #region OwnUser

            _channelMutes.TryReplaceRegularObjectsFromDto(dto.ChannelMutes, cache);
            _devices.TryReplaceRegularObjectsFromDto(dto.Devices, cache);
            _latestHiddenChannels.TryReplaceValuesFromDto(dto.LatestHiddenChannels);
            _mutes.TryReplaceRegularObjectsFromDto(dto.Mutes, cache);

            TotalUnreadCount = GetOrDefault(dto.TotalUnreadCount, TotalUnreadCount);
            UnreadChannels = GetOrDefault(dto.UnreadChannels, UnreadChannels);
            //UnreadCount = dto.UnreadCount; Deprecated

            #endregion

            User = cache.Users.CreateOrUpdate<ErmisUser, OwnUserInternalDTO>(dto, out _);

            LoadAdditionalProperties(dto.AdditionalProperties);
            
#if ERMIS_DEBUG_ENABLED
            Logs.Info($"Local User Data Loaded. {nameof(TotalUnreadCount)}: {TotalUnreadCount}, UnreadChannels: {UnreadChannels}");
#endif
        }
        
        void IUpdateableFrom<WrappedUnreadCountsResponseInternalDTO, ErmisLocalUserData>.UpdateFromDto(WrappedUnreadCountsResponseInternalDTO dto,
            ICache cache)
        {
            TotalUnreadCount = GetOrDefault(dto.TotalUnreadCount, TotalUnreadCount);
            UnreadChannels = dto.Channels?.Count ?? 0;
        }
        
        internal ErmisLocalUserData(string uniqueId, ICacheRepository<ErmisLocalUserData> repository,
            IStatefulModelContext context)
            : base(uniqueId, repository, context)
        {
        }
        
        internal void InternalHandleMarkReadNotification(NotificationMarkReadEventInternalDTO eventDto)
        {
            TotalUnreadCount = GetOrDefault(eventDto.TotalUnreadCount, TotalUnreadCount);
            UnreadChannels = GetOrDefault(eventDto.UnreadChannels, UnreadChannels);
            //UnreadCount = dto.UnreadCount; Deprecated
        }
        
        protected override string InternalUniqueId { get; set; }
        protected override ErmisLocalUserData Self => this;

        private readonly List<ErmisChannelMute> _channelMutes = new List<ErmisChannelMute>();
        private readonly List<ErmisDevice> _devices = new List<ErmisDevice>();
        private readonly List<string> _latestHiddenChannels = new List<string>();
        private readonly List<ErmisUserMute> _mutes = new List<ErmisUserMute>();
    }
}