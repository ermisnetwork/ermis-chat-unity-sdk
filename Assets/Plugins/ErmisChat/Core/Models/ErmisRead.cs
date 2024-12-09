using System;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.State;
using Ermis.Core.State.Caches;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.Models
{
    //ErmisTodo: this could contain the last read ErmisMessage
    public class ErmisRead : IStateLoadableFrom<ReadInternalDTO, ErmisRead>,
        IStateLoadableFrom<ReadStateResponseInternalDTO, ErmisRead>
    {
        public DateTimeOffset LastRead { get; private set; }

        public int UnreadMessages { get; private set; }

        public IErmisUser User { get; private set; }

        ErmisRead IStateLoadableFrom<ReadInternalDTO, ErmisRead>.LoadFromDto(ReadInternalDTO dto, ICache cache)
        {
            //Is this always set? What if a user marks empty channel as read? 
            LastRead = dto.LastRead; //ErmisTodo: GetValueOrThrow? 
            UnreadMessages = dto.UnreadMessages;
            User = cache.TryCreateOrUpdate(dto.User);

            return this;
        }
        
        ErmisRead IStateLoadableFrom<ReadStateResponseInternalDTO, ErmisRead>.LoadFromDto(ReadStateResponseInternalDTO dto, ICache cache)
        {
            //Is this always set? What if a user marks empty channel as read? 
            LastRead = dto.LastRead; //ErmisTodo: GetValueOrThrow? 
            UnreadMessages = dto.UnreadMessages;
            User = cache.TryCreateOrUpdate(dto.User);

            return this;
        }

        internal void Update(DateTimeOffset lastRead) => LastRead = lastRead;
    }
}