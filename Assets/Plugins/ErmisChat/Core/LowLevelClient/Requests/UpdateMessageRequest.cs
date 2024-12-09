using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class UpdateMessageRequest : RequestObjectBase, ISavableTo<UpdateMessageRequestInternalDTO>
    {
        public MessageUpdateRequest Message { get; set; }

        UpdateMessageRequestInternalDTO ISavableTo<UpdateMessageRequestInternalDTO>.SaveToDto() =>
            new UpdateMessageRequestInternalDTO
            {
                Message = Message.TrySaveToDto(),
            };
    }

    public class MessageUpdateRequest : RequestObjectBase, ISavableTo<MessageUpdateRequestInternalDTO>
    {
        public string Text;
        MessageUpdateRequestInternalDTO ISavableTo<MessageUpdateRequestInternalDTO>.SaveToDto() =>
            new MessageUpdateRequestInternalDTO
            {
                Text = Text,
            };
    }

}