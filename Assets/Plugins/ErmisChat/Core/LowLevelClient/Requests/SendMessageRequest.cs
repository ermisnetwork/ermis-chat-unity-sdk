using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    /// <summary>
    /// Contains all information needed to send new message
    /// </summary>
    public class SendMessageRequest : RequestObjectBase, ISavableTo<SendMessageRequestInternalDTO>
    {
       

        public MessageRequest Message { get; set; } = new MessageRequest();

        SendMessageRequestInternalDTO ISavableTo<SendMessageRequestInternalDTO>.SaveToDto() =>
            new SendMessageRequestInternalDTO
            {
                Message = Message.TrySaveToDto(),
            };
    }
}