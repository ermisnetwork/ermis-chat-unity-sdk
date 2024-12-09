using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.Models
{
    public class Read : ModelBase, ILoadableFrom<ReadStateResponseInternalDTO, Read>
    {
        public UserIdObject User { get; set; }

        public System.DateTimeOffset LastRead { get; set; }
        public string LastReadMessageId { get; set; }
        public System.DateTimeOffset LastSend { get; set; }
        public int UnreadMessages { get; set; }

        Read ILoadableFrom<ReadStateResponseInternalDTO, Read>.LoadFromDto(ReadStateResponseInternalDTO dto)
        {
            User = User.TryLoadFromDto(dto.User);
            LastRead = dto.LastRead;
            LastReadMessageId = dto.LastReadMessageId;
            LastSend = dto.LastSend;
            UnreadMessages = dto.UnreadMessages;
            return this;
        }
    }
}