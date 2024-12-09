using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.StatefulModels;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;

namespace Ermis.Core.State.Caches
{
    internal sealed class Cache : ICache
    {
        public Cache(ErmisChatClient stateClient, ISerializer serializer, ILogs logs)
        {
            var trackedObjectsFactory = new StatefulModelsFactory(stateClient, serializer, logs, this);

            Channels = new CacheRepository<ErmisChannel>(trackedObjectsFactory.CreateErmisChannel, cache: this);
            Messages = new CacheRepository<ErmisMessage>(trackedObjectsFactory.CreateErmisMessage, cache: this);
            Users = new CacheRepository<ErmisUser>(trackedObjectsFactory.CreateErmisUser, cache: this);
            LocalUser = new CacheRepository<ErmisLocalUserData>(trackedObjectsFactory.CreateErmisLocalUser, cache: this);
            ChannelMembers = new CacheRepository<ErmisChannelMember>(trackedObjectsFactory.CreateErmisChannelMember, cache: this);

            Channels.RegisterDtoIdMapping<ErmisChannel, ChannelStateResponseInternalDTO>(dto => dto.Channel.Cid);
            Channels.RegisterDtoIdMapping<ErmisChannel, ChannelResponseInternalDTO>(dto => dto.Cid);
            Channels.RegisterDtoIdMapping<ErmisChannel, ChannelStateResponseFieldsInternalDTO>(dto => dto.Channel.Cid);
            Channels.RegisterDtoIdMapping<ErmisChannel, UpdateChannelResponseInternalDTO>(dto => dto.Channel.Cid);

            Users.RegisterDtoIdMapping<ErmisUser, UserIdObjectInternalDTO>(dto => dto.Id);
            Users.RegisterDtoIdMapping<ErmisUser, UserResponseInternalDTO>(dto => dto.Id);
            Users.RegisterDtoIdMapping<ErmisUser, OwnUserInternalDTO>(dto => dto.Id);
            Users.RegisterDtoIdMapping<ErmisUser, FullUserResponseInternalDTO>(dto => dto.Id);

            LocalUser.RegisterDtoIdMapping<ErmisLocalUserData, OwnUserInternalDTO>(dto => dto.Id);

            //In some cases the ChannelMemberInternalDTO.UserId was null -> only known case is channelDto.Membership
            ChannelMembers.RegisterDtoIdMapping<ErmisChannelMember, ChannelMemberInternalDTO>(dto =>
            {
                if(dto.User != null)
                {
                    return dto.User.Id;
                }

                return dto.UserId;
            });

            Messages.RegisterDtoIdMapping<ErmisMessage, MessageInternalDTO>(dto => dto.Id);
            Messages.RegisterDtoIdMapping<ErmisMessage, MessageResponseInternalDTO>(dto => dto.Id);
        }

        public ICacheRepository<ErmisChannel> Channels { get; }

        public ICacheRepository<ErmisMessage> Messages { get; }

        public ICacheRepository<ErmisUser> Users { get; }

        public ICacheRepository<ErmisLocalUserData> LocalUser { get; }

        public ICacheRepository<ErmisChannelMember> ChannelMembers { get; }
    }
}