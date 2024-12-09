using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.State.Caches
{
    internal static class ICacheExt
    {
        public static ErmisMessage TryCreateOrUpdate(this ICache cache, MessageInternalDTO dto)
            => dto == null ? null : cache.Messages.CreateOrUpdate<ErmisMessage, MessageInternalDTO>(dto, out _);

        public static ErmisMessage TryCreateOrUpdate(this ICache cache, MessageResponseInternalDTO dto)
            => dto == null ? null : cache.Messages.CreateOrUpdate<ErmisMessage, MessageResponseInternalDTO>(dto, out _);

        public static ErmisMessage TryCreateOrUpdate(this ICache cache, MessageInternalDTO dto, out bool wasCreated)
        {
            wasCreated = false;
            return dto == null
                ? null
                : cache.Messages.CreateOrUpdate<ErmisMessage, MessageInternalDTO>(dto, out wasCreated);
        }

        public static ErmisMessage TryCreateOrUpdate(this ICache cache, MessageResponseInternalDTO dto, out bool wasCreated)
        {
            wasCreated = false;
            return dto == null
                ? null
                : cache.Messages.CreateOrUpdate<ErmisMessage, MessageResponseInternalDTO>(dto, out wasCreated);
        }

        public static ErmisChannel TryCreateOrUpdate(this ICache cache, ChannelResponseInternalDTO dto)
            => dto == null
                ? null
                : cache.Channels.CreateOrUpdate<ErmisChannel, ChannelResponseInternalDTO>(dto, out _);
        
        public static ErmisChannel TryCreateOrUpdate(this ICache cache, ChannelResponseInternalDTO dto, out bool wasCreated)
        {
            wasCreated = false;
            return dto == null
                ? null
                : cache.Channels.CreateOrUpdate<ErmisChannel, ChannelResponseInternalDTO>(dto, out wasCreated);
        }

        public static ErmisChannel TryCreateOrUpdate(this ICache cache, ChannelStateResponseFieldsInternalDTO dto)
            => dto == null
                ? null
                : cache.Channels.CreateOrUpdate<ErmisChannel, ChannelStateResponseFieldsInternalDTO>(dto, out _);

        public static ErmisChannel TryCreateOrUpdate(this ICache cache, ChannelStateResponseInternalDTO dto)
            => dto == null
                ? null
                : cache.Channels.CreateOrUpdate<ErmisChannel, ChannelStateResponseInternalDTO>(dto, out _);

        public static ErmisChannel TryCreateOrUpdate(this ICache cache, UpdateChannelResponseInternalDTO dto)
            => dto == null
                ? null
                : cache.Channels.CreateOrUpdate<ErmisChannel, UpdateChannelResponseInternalDTO>(dto, out _);

        public static ErmisChannelMember TryCreateOrUpdate(this ICache cache, ChannelMemberInternalDTO dto)
            => dto == null
                ? null
                : cache.ChannelMembers.CreateOrUpdate<ErmisChannelMember, ChannelMemberInternalDTO>(dto, out _);

        public static ErmisUser TryCreateOrUpdate(this ICache cache, UserResponseInternalDTO dto)
            => dto == null ? null : cache.Users.CreateOrUpdate<ErmisUser, UserResponseInternalDTO>(dto, out _);
        
        public static ErmisUser TryCreateOrUpdate(this ICache cache, UserIdObjectInternalDTO dto)
            => dto == null ? null : cache.Users.CreateOrUpdate<ErmisUser, UserIdObjectInternalDTO>(dto, out _);

        public static ErmisUser TryCreateOrUpdate(this ICache cache, UserIdObjectInternalDTO dto,
            out bool wasCreated)
        {
            wasCreated = false;
            return dto == null
                ? null
                : cache.Users.CreateOrUpdate<ErmisUser, UserIdObjectInternalDTO>(dto, out wasCreated);
        }

        public static ErmisLocalUserData TryCreateOrUpdate(this ICache cache, OwnUserInternalDTO dto)
            => dto == null ? null : cache.LocalUser.CreateOrUpdate<ErmisLocalUserData, OwnUserInternalDTO>(dto, out _);
        
        public static ErmisUser TryCreateOrUpdate(this ICache cache, FullUserResponseInternalDTO dto)
            => dto == null ? null : cache.Users.CreateOrUpdate<ErmisUser, FullUserResponseInternalDTO>(dto, out _);
        
        public static ErmisUser TryCreateOrUpdate(this ICache cache, UserEventPayloadInternalDTO dto)
            => dto == null ? null : cache.Users.CreateOrUpdate<ErmisUser, UserEventPayloadInternalDTO>(dto, out _);
    }
}