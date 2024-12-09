using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.API.Internal;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.API
{
    internal class MessageApi : IMessageApi
    {
        public MessageApi(IInternalMessageApi internalMessageApi)
        {
            _internalMessageApi = internalMessageApi ?? throw new ArgumentNullException(nameof(internalMessageApi));
        }

        public async Task<FileDeleteResponse> DeleteFileAsync(string channelType, string channelId, string fileUrl)
        {
            var dto = await _internalMessageApi.DeleteFileAsync(channelType, channelId, fileUrl);
            return dto.ToDomain<FileDeleteResponseInternalDTO, FileDeleteResponse>();
        }

        public async Task<ImageUploadResponse> UploadImageAsync(string channelType, string channelId,
            byte[] fileContent, string fileName)
        {
            var dto = await _internalMessageApi.UploadImageAsync(channelType, channelId, fileContent, fileName);
            return dto.ToDomain<ImageUploadResponseInternalDTO, ImageUploadResponse>();
        }

        public async Task<SearchResponse> SearchMessagesAsync(SearchRequest searchRequest)
        {
            var dto = await _internalMessageApi.SearchMessagesAsync(searchRequest.TrySaveToDto());
            return dto.ToDomain<SearchResponseInternalDTO, SearchResponse>();
        }

        private readonly IInternalMessageApi _internalMessageApi;

        #region New
        public async Task<SendMessageResponse> SendNewMessageAsync(string channelType, string channelId,
           SendMessageRequest sendMessageRequest)
        {
            var dto = await _internalMessageApi.SendNewMessageAsync(channelType, channelId,
                sendMessageRequest.TrySaveToDto());
            return dto.ToDomain<SendMessageResponseInternalDTO, SendMessageResponse>();
        }

        public async Task<UpdateMessageResponse> UpdateMessageAsync( string channelType, string channelId, string messageId,
            UpdateMessageRequest updateMessageRequest)
        {
            var dto = await _internalMessageApi.UpdateMessageAsync( channelType, channelId, messageId,updateMessageRequest.TrySaveToDto());
            return dto.ToDomain<UpdateMessageResponseInternalDTO, UpdateMessageResponse>();
        }

        public async Task<FileUploadResponse> UploadFileAsync(string channelType, string channelId,
          byte[] fileContent, string fileName)
        {
            var dto = await _internalMessageApi.UploadFileAsync(channelType, channelId, fileContent, fileName);
            return dto.ToDomain<FileUploadResponseInternalDTO, FileUploadResponse>();
        }

        public async Task<DeleteMessageResponse> DeleteMessageAsync(string channelType, string channelId, string messageId)
        {
            var dto = await _internalMessageApi.DeleteMessageAsync(channelType, channelId,messageId);
            return dto.ToDomain<DeleteMessageResponseInternalDTO, DeleteMessageResponse>();
        }

        public async Task<SendReactionResponse> SendReactionAsync(string channelType, string channelId, string messageId, string reactionType)
        {
            var dto = await _internalMessageApi.SendReactionAsync(channelType, channelId,messageId,reactionType);
            return dto.ToDomain<SendReactionResponseInternalDTO, SendReactionResponse>();
        }

        public async Task<ReactionRemovalResponse> DeleteReactionAsync(string channelType, string channelId, string messageId, string reactionType)
        {
            var dto = await _internalMessageApi.DeleteReactionAsync(channelType,channelId, messageId, reactionType);
            return dto.ToDomain<ReactionRemovalResponseInternalDTO, ReactionRemovalResponse>();
        }

        public async Task<PinUnpinResponse> PinUnpinMessageAsync(string channelType, string channelId, string messageId, string action, PinUnpinRequest request)
        {
            var dto = await _internalMessageApi.PinUnpinMessageAsync(channelType, channelId, messageId, action, request.TrySaveToDto());
            return dto.ToDomain<PinUnpinResponseInternalDTO, PinUnpinResponse>();

        }
        public async Task<SearchChannelMessagesResponse> SearchChannelMessagesAsync(SearchChannelMessagesRequest searchRequest)
        {

            var dto = await _internalMessageApi.SearchChannelMessagesAsync(searchRequest.TrySaveToDto());
            return dto.ToDomain<SearchChannelMessagesResponseInternalDTO, SearchChannelMessagesResponse>();
        }
        #endregion
    }
}