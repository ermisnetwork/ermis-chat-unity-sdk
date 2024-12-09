using System.Net.Http;
using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.Web;
using Ermis.Libs.Http;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;

namespace Ermis.Core.LowLevelClient.API.Internal
{
    internal class InternalMessageApi : InternalApiClientBase, IInternalMessageApi
    {
        #region Old

        public InternalMessageApi(IHttpClient httpClient, ISerializer serializer, ILogs logs,
            IRequestUriFactory requestUriFactory, IErmisChatLowLevelClient lowLevelClient)
            : base(httpClient, serializer, logs, requestUriFactory, lowLevelClient)
        {
        }

        

        public Task<MessageResponseInternalDTO> UpdateMessagePartialAsync(string messageId, string channelType, string channelId,
            UpdateMessagePartialRequestInternalDTO updateMessagePartialRequest)
        {
            var endpoint = MessageEndpoints.UpdateMessage(messageId, channelType, channelId);
            return Put<UpdateMessagePartialRequestInternalDTO, MessageResponseInternalDTO>(endpoint, updateMessagePartialRequest);
        }

        public Task<FileUploadResponseInternalDTO> UploadFileAsync(string channelType, string channelId,
            byte[] fileContent, string fileName)
        {
            var endpoint = $"/channels/{channelType}/{channelId}/file";

            var fileWrapper = new FileWrapper(fileName, fileContent);
            return PostWithCustomHeader<FileUploadResponseInternalDTO>(endpoint, fileWrapper);
        }

        public Task<FileDeleteResponseInternalDTO> DeleteFileAsync(string channelType, string channelId, string fileUrl)
        {
            var endpoint = $"channels/{channelType}/{channelId}/file";
            var parameters = QueryParameters.Default.Append("url", fileUrl);

            return Delete<FileDeleteResponseInternalDTO>(endpoint, parameters);
        }

        public Task<ImageUploadResponseInternalDTO> UploadImageAsync(string channelType, string channelId,
            byte[] fileContent, string fileName)
        {
            var endpoint = $"/channels/{channelType}/{channelId}/image";

            var fileWrapper = new FileWrapper(fileName, fileContent);
            return PostWithCustomHeader<ImageUploadResponseInternalDTO>(endpoint, fileWrapper);
        }

        public Task<SearchResponseInternalDTO> SearchMessagesAsync(SearchRequestInternalDTO searchRequest)
            => Get<SearchRequestInternalDTO, SearchResponseInternalDTO>("/search", searchRequest);
        #endregion


        #region New

        public Task<SendMessageResponseInternalDTO> SendNewMessageAsync(string channelType, string channelId,
            SendMessageRequestInternalDTO sendMessageRequest)
        {
            var endpoint = MessageEndpoints.SendMessage(channelType, channelId);
            return PostWithCustomHeader<SendMessageRequestInternalDTO, SendMessageResponseInternalDTO>(endpoint, sendMessageRequest);
        }

        public Task<UpdateMessageResponseInternalDTO> UpdateMessageAsync(string channelType, string channelId, string messageId,
            UpdateMessageRequestInternalDTO updateMessageRequest)
        {
            var endpoint = MessageEndpoints.UpdateMessage(channelType, channelId, messageId);
            return PostWithCustomHeader<UpdateMessageRequestInternalDTO, UpdateMessageResponseInternalDTO>(endpoint, updateMessageRequest);
        }
        public Task<DeleteMessageResponseInternalDTO> DeleteMessageAsync(string channelType, string channelId, string messageId)
        {
            var endpoint = MessageEndpoints.DeleteMessage(channelType, channelId, messageId);
            return DeleteWithCustomHeader<DeleteMessageResponseInternalDTO>(endpoint);
        }

        public Task<ReactionRemovalResponseInternalDTO> DeleteReactionAsync(string channelType, string channelId, string messageId, string reactionType)
        {
            var endpoint = MessageEndpoints.DeleteReaction(channelType, channelId, messageId, reactionType);
            return DeleteWithCustomHeader<ReactionRemovalResponseInternalDTO>(endpoint);
        }

        public Task<PinUnpinResponseInternalDTO> PinUnpinMessageAsync(string channelType, string channelId, string messageId, string action, PinUnpinRequestInternalDTO request)
        {
            var endpoint = MessageEndpoints.PinUnpinMessage(channelType, channelId, messageId, action);
            return PostWithCustomHeader<PinUnpinResponseInternalDTO>(endpoint, null);

        }

        public Task<SendReactionResponseInternalDTO> SendReactionAsync(string channelType, string channelId, string messageId, string reactionType)
        {
            var endpoint = MessageEndpoints.SendReaction(channelType, channelId, messageId, reactionType);
            return PostWithCustomHeader<SendReactionResponseInternalDTO>(endpoint, null);
        }

        public Task<SearchChannelMessagesResponseInternalDTO> SearchChannelMessagesAsync(SearchChannelMessagesRequestInternalDTO searchRequest)
        {
            var endpoint = MessageEndpoints.SearchMessages();
            return PostWithCustomHeader<SearchChannelMessagesRequestInternalDTO, SearchChannelMessagesResponseInternalDTO>(endpoint, searchRequest);
        }
        #endregion
    }
}