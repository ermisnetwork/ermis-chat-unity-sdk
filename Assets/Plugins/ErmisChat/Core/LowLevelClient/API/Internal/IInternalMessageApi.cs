using System.Net.Http;
using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal interface IInternalMessageApi
    {
        Task<SendMessageResponseInternalDTO> SendNewMessageAsync(string channelType, string channelId,
            SendMessageRequestInternalDTO sendMessageRequest);

        Task<UpdateMessageResponseInternalDTO> UpdateMessageAsync(string channelType, string channelId, string messageId,
            UpdateMessageRequestInternalDTO updateMessageRequest);


        Task<MessageResponseInternalDTO> UpdateMessagePartialAsync(string messageId, string channelType, string channelId,
            UpdateMessagePartialRequestInternalDTO updateMessagePartialRequest);

        Task<DeleteMessageResponseInternalDTO> DeleteMessageAsync(string channelType, string channelId, string messageId);

        Task<SendReactionResponseInternalDTO> SendReactionAsync(string channelType, string channelId, string messageId, string reactionType);

        Task<ReactionRemovalResponseInternalDTO> DeleteReactionAsync(string channelType, string channelId, string messageId, string reactionType);

        Task<FileUploadResponseInternalDTO> UploadFileAsync(string channelType, string channelId,
            byte[] fileContent, string fileName);

        Task<FileDeleteResponseInternalDTO> DeleteFileAsync(string channelType, string channelId, string fileUrl);

        Task<ImageUploadResponseInternalDTO> UploadImageAsync(string channelType, string channelId,
            byte[] fileContent, string fileName);

        Task<SearchResponseInternalDTO> SearchMessagesAsync(SearchRequestInternalDTO searchRequest);

        Task<PinUnpinResponseInternalDTO> PinUnpinMessageAsync(string channelType, string channelId, string messageId, string action,PinUnpinRequestInternalDTO request);
        Task<SearchChannelMessagesResponseInternalDTO> SearchChannelMessagesAsync(SearchChannelMessagesRequestInternalDTO searchRequest);
    }
}