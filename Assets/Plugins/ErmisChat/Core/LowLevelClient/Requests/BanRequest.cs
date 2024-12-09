using System;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.LowLevelClient.Requests
{
    public partial class BanRequest : RequestObjectBase, ISavableTo<BanRequestInternalDTO>
    {
        //ErmisTodo: isn't this server-side only? 
        /// <summary>
        /// User who issued a ban
        /// </summary>
        public UserObjectRequest BannedBy { get; set; }

        //ErmisTodo: isn't this server-side only? 
        /// <summary>
        /// User ID who issued a ban
        /// </summary>
        public string BannedById { get; set; }
        
        /// <summary>
        /// Channel CID to ban user in e.g. messaging:123. You can grab the channel CID from objects like <see cref="IErmisChannel.Cid"/>, <see cref="IErmisMessage.Cid"/>
        /// </summary>
        public string ChannelCid { get; set; }

        [Obsolete("Will be removed in a future release. Please use the ChannelCid field")] //ErmisTodo: remove this in a major release
        public string Id { get; set; }

        /// <summary>
        /// Whether to perform IP ban or not
        /// </summary>
        public bool? IpBan { get; set; }

        /// <summary>
        /// Ban reason
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Whether to perform shadow-ban or not
        /// </summary>
        public bool? Shadow { get; set; }

        /// <summary>
        /// ID of user to ban
        /// </summary>
        public string TargetUserId { get; set; }

        /// <summary>
        /// Timeout of ban in minutes. User will be unbanned after this period of time
        /// </summary>
        public int? Timeout { get; set; }

        [Obsolete("Will be removed in a future release. Please use the ChannelCid field")] //ErmisTodo: remove this in a major release
        public string Type { get; set; }

        [Obsolete("Has no effect and will be removed in a future release")] //ErmisTodo: remove this in a major release
        public UserObjectRequest User { get; set; }

        [Obsolete("Has no effect and will be removed in a future release")] //ErmisTodo: remove this in a major release
        public string UserId { get; set; }

        BanRequestInternalDTO ISavableTo<BanRequestInternalDTO>.SaveToDto()
        {
            #pragma warning disable CS0618
            string GetCid()
            {
                if(string.IsNullOrEmpty(ChannelCid) && !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Type))
                {
                    return Type + ":" + Id;
                }

                return ChannelCid;
            }
            #pragma warning restore CS0618
            return new BanRequestInternalDTO
            {
                BannedBy = BannedBy.TrySaveToDto<UserRequestInternalDTO>(),
                BannedById = BannedById,
                ChannelCid = GetCid(),
                IpBan = IpBan,
                Reason = Reason,
                Shadow = Shadow,
                TargetUserId = TargetUserId,
                Timeout = Timeout,
                AdditionalProperties = AdditionalProperties,
            };
        }
    }
}