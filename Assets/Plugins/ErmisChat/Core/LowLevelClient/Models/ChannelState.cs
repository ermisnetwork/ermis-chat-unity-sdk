using System;
using System.Collections.Generic;
using System.Linq;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.Models
{
    public class ChannelState : ModelBase, ILoadableFrom<ChannelStateResponseFieldsInternalDTO, ChannelState>
       // ILoadableFrom<ChannelStateResponseInternalDTO, ChannelState>
    {

        public Channel Channel { get; set; }

        /// <summary>
        /// List of channel messages
        /// </summary>
        public List<MessageResponse> Messages { get; set; }

        /// <summary>
        /// List of user who is watching the channel
        /// </summary>
        public List<User> Watchers { get; set; }

        /// <summary>
        /// List of read states
        /// </summary>
        public List<Read> Read { get; set; }

        public ChannelMember Membership { get; set; }

        ChannelState ILoadableFrom<ChannelStateResponseFieldsInternalDTO, ChannelState>.LoadFromDto(ChannelStateResponseFieldsInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            Membership = Membership.TryLoadFromDto<ChannelMemberInternalDTO, ChannelMember>(dto.Membership);
            Messages = Messages.TryLoadFromDtoCollection(dto.Messages);
            Read = Read.TryLoadFromDtoCollection(dto.Read);
            Watchers = Watchers.TryLoadFromDtoCollection(dto.Watchers);

            return this;
        }

        //ChannelState ILoadableFrom<ChannelStateResponseInternalDTO, ChannelState>.LoadFromDto(ChannelStateResponseInternalDTO dto)
        //{
        //    Channel = Channel.TryLoadFromDto(dto.Channel);
        //    Members = Members.TryLoadFromDtoCollection(dto.Channel.Members);
        //    Membership = Membership.TryLoadFromDto<ChannelMemberInternalDTO, ChannelMember>(dto.Membership);
        //    Messages = Messages.TryLoadFromDtoCollection(dto.Messages);
        //    Read = Read.TryLoadFromDtoCollection(dto.Read);
        //    WatcherCount = dto.Watchers.Count;
        //    Watchers = Watchers.TryLoadFromDtoCollection(dto.Watchers);
        //    return this;
        //}
    }

    public class ChannelStateResponseFields : ModelBase, ILoadableFrom<ChannelStateResponseFieldsInternalDTO, ChannelStateResponseFields>
    {

        public Channel Channel { get; set; }

        /// <summary>
        /// List of channel messages
        /// </summary>
        public List<MessageResponse> Messages { get; set; }

        /// <summary>
        /// List of user who is watching the channel
        /// </summary>
        public List<UserIdObject> Watchers { get; set; }

        /// <summary>
        /// List of read states
        /// </summary>
        public List<Read> Read { get; set; }

        public ChannelMember Membership { get; set; }

        ChannelStateResponseFields ILoadableFrom<ChannelStateResponseFieldsInternalDTO, ChannelStateResponseFields>.LoadFromDto(ChannelStateResponseFieldsInternalDTO dto)
        {
            Channel = Channel.TryLoadFromDto(dto.Channel);
            Watchers = Watchers.TryLoadFromDtoCollection(dto.Watchers);
            Membership = Membership.TryLoadFromDto<ChannelMemberInternalDTO, ChannelMember>(dto.Membership);
            Messages = Messages.TryLoadFromDtoCollection(dto.Messages);
            Read = Read.TryLoadFromDtoCollection(dto.Read);

            return this;
        }
    }
}