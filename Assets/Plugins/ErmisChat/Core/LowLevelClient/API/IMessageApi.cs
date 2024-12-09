using System.Net.Http;
using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Responses;

namespace Ermis.Core.LowLevelClient.API
{
    /// <summary>
    /// A client that can be used to retrieve, create and alter messages of a Ermis Chat application.
    /// </summary>
    public interface IMessageApi
    {
       

       

        /// <summary>
        /// <para>Deletes a file.</para>
        /// This functionality defaults to using the Ermis CDN. If you would like, you can
        /// easily change the logic to upload to your own CDN of choice.
        /// </summary>
        Task<FileDeleteResponse> DeleteFileAsync(string channelType, string channelId, string fileUrl);



        /// <summary>
        /// <para>Search messages</para>
        /// You can enable and/or disable the search indexing on a per channel
        /// type through the Ermis Dashboard.
        /// </summary>
        Task<SearchResponse> SearchMessagesAsync(SearchRequest searchRequest);

        Task<ImageUploadResponse> UploadImageAsync(string channelType, string channelId,
            byte[] fileContent, string fileName);


        #region New
        Task<SendMessageResponse> SendNewMessageAsync(string channelType, string channelId,
            SendMessageRequest sendMessageRequest);

        Task<UpdateMessageResponse> UpdateMessageAsync(string channelType, string channelId, string messageId,
           UpdateMessageRequest updateMessageRequest);

        Task<FileUploadResponse> UploadFileAsync(string channelType, string channelId,
            byte[] fileContent, string fileName);


        Task<DeleteMessageResponse> DeleteMessageAsync(string channelType, string channelId, string messageId);

        Task<SendReactionResponse> SendReactionAsync(string channelType, string channelId, string messageId, string reactionType);

        Task<ReactionRemovalResponse> DeleteReactionAsync(string channelType, string channelId, string messageId, string reactionType);

        Task<PinUnpinResponse> PinUnpinMessageAsync(string channelType, string channelId, string messageId, string action,PinUnpinRequest request);
        Task<SearchChannelMessagesResponse> SearchChannelMessagesAsync(SearchChannelMessagesRequest searchRequest);
        #endregion
    }
}