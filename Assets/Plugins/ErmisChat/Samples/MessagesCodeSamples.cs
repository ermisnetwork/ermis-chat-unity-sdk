using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.Requests;
using Ermis.Core.StatefulModels;
using UnityEngine;

namespace ErmisChat.Samples
{
    internal sealed class MessagesCodeSamples
    {
       
        public async Task Overview()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var message = await channel.SendNewMessageAsync("Hello world!");
        }

       
        public async Task ComplexExample()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, "my-channel-id");

            IErmisUser someUser = null;

// Send simple message with text only
            var message3 = await channel.SendNewMessageAsync("Hello");

// Send simple message with text only
            var message2 = await channel.SendNewMessageAsync("Let's start a thread!");

            var message = await channel.SendNewMessageAsync(new ErmisSendMessageRequest
            {
                MentionedUsers = new List<IErmisUser> { someUser }, // Mention a user
                ParentId = message2.Id, // Write in thread
                PinExpires = new DateTimeOffset(DateTime.Now).AddDays(7), // Pin for 7 days
                Pinned = true,
                QuotedMessage = message3,
                ShowInChannel = true,
                Text = "Hello",
                CustomData = new ErmisCustomDataRequest
                {
                    { "my_lucky_numbers", new List<int> { 7, 13, 81 } }
                }
            });
        }

      
        public async Task GetMessageById()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
        }

 
        public async Task UpdateAMessage()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var message = await channel.SendNewMessageAsync("Hello world!");

// Edit message text and some custom data
            await message.UpdateAsync(new ErmisUpdateMessageRequest
            {
                Text = "Hi everyone!",
                CustomData = new ErmisCustomDataRequest
                {
                    { "tags", new[] { "Funny", "Unique" } }
                }
            });
        }

    
        public async Task PartialUpdate()
        {
            await Task.CompletedTask;
        }


        public async Task DeleteAMessage()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var message = await channel.SendNewMessageAsync("Hello world!");

// Soft delete
            await message.SoftDeleteAsync();

// Hard delete
            await message.HardDeleteAsync();
        }

   
        public async Task OpenGraphScrapper()
        {
            await Task.CompletedTask;
        }


        public async Task UploadFileOrImage()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

// Get file byte array however you want e.g. Addressables.LoadAsset, Resources.Load, etc.
            var sampleFile = File.ReadAllBytes("path/to/file");
            var fileUploadResponse = await channel.UploadFileAsync(sampleFile, "my-file-name");
            var fileWebUrl = fileUploadResponse.FileUrl;

// Get image byte array however you want e.g. Addressables.LoadAsset, Resources.Load, etc.
            var sampleImage = File.ReadAllBytes("path/to/file");
            var imageUploadResponse = await channel.UploadImageAsync(sampleFile, "my-image-name");
            var imageWebUrl = imageUploadResponse.FileUrl;
        }

  
        public async Task DeleteFileOrImage()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            await channel.DeleteFileOrImageAsync("file-url");
        }

 
        public async Task UsingYourOwnCdn()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

//Implement your own CDN upload and obtain the file URL
            var fileUrl = "file-url-to-your-cdn";

            await channel.SendNewMessageAsync(new ErmisSendMessageRequest
            {
                Text = "Message with file attachment",
                Attachments = new List<ErmisAttachmentRequest>
                {
                    new ErmisAttachmentRequest
                    {
                        AssetUrl = fileUrl,
                    }
                }
            });

            await Task.CompletedTask;
        }


        public async Task ReactionOverview()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var message = await channel.SendNewMessageAsync("Hello world!");

// Send simple reaction with a score of 1
            await message.SendReactionAsync("like");

// Send reaction with a custom score value
            await message.SendReactionAsync("clap", 10);

// Send reaction with a custom score value
            await message.SendReactionAsync("clap", 10);

// Send reaction and replace all previous reactions (if any) from this user
            await message.SendReactionAsync("love", enforceUnique: true);
        }

     
        public async Task RemoveReaction()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var message = await channel.SendNewMessageAsync("Hello world!");

            await message.DeleteReactionAsync("like");
        }

 
        public async Task PaginateReactions()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var message = await channel.SendNewMessageAsync("Hello world!");

            //ErmisTodo: IMPLEMENT reactions paginating
        }

 
        public async Task CumulativeReactions()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var message = await channel.SendNewMessageAsync("Hello world!");

            await message.SendReactionAsync("clap", score: 3);
        }

 
        public async Task ThreadsAndReplies()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, "my-channel-id");

            // Send simple message with text only
            var message3 = await channel.SendNewMessageAsync("Hello");

// Send simple message with text only
            var parentMessage = await channel.SendNewMessageAsync("Let's start a thread!");

            var messageInThread = await channel.SendNewMessageAsync(new ErmisSendMessageRequest
            {
                ParentId = parentMessage.Id, // Write in thread
                ShowInChannel = false, // Optionally send to both thread and the main channel like in Slack
                Text = "Hello",
            });
        }

   
        public async Task ThreadPagination()
        {
            await Task.CompletedTask;
        }

  
        public async Task QuoteMessage()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, "my-channel-id");

            // Send simple message with text only
            var message3 = await channel.SendNewMessageAsync("Hello");

// Send simple message with text only
            var quotedMessage = await channel.SendNewMessageAsync("Let's start a thread!");

            var messageWithQuote = await channel.SendNewMessageAsync(new ErmisSendMessageRequest
            {
                QuotedMessage = quotedMessage,
                Text = "Hello",
            });
        }

 
        public async Task Reminders()
        {
            await Task.CompletedTask;
        }

   
        public async Task SilentMessages()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, "my-channel-id");

// This message will not trigger events for channel members
            var silentMessage = await channel.SendNewMessageAsync(new ErmisSendMessageRequest
            {
                Text = "System message",
                Silent = true
            });
        }

 
        public async Task Search()
        {
// Access to low-level client is left for backward compatibility. Soon simplified syntax for searching will be implemented
            var searchResponse = await Client.LowLevelClient.MessageApi.SearchMessagesAsync(new SearchRequest
            {
                //Filter is required for search
                FilterConditions = new Dictionary<string, object>
                {
                    {
                        //Get channels that local user is a member of
                        "members", new Dictionary<string, object>
                        {
                            { "$in", new[] { "John" } }
                        }
                    }
                },

                //search phrase
                Query = "supercalifragilisticexpialidocious"
            });

            foreach (var searchResult in searchResponse.Results)
            {
                Debug.Log(searchResult.Message.Id); //Message ID
                Debug.Log(searchResult.Message.Text); //Message text
                Debug.Log(searchResult.Message.User); //Message author info
                Debug.Log(searchResult.Message.Channel); //Channel info
            }
        }

  
        public async Task SearchPagination()
        {
            await Task.CompletedTask;
        }

   
        public async Task PinAndUnpinMessage()
        {
            IErmisMessage message = null;

// Pin until unpinned
            await message.PinAsync();

// Pin for 7 days
          //  await message.PinAsync(new DateTime().AddDays(7));

// Unpin previously pinned message
            await message.UnpinAsync();
        }

     
        public async Task RetrievePinnedMessages()
        {
            await Task.CompletedTask;
        }

    
        public async Task PaginatePinnedMessages()
        {
            await Task.CompletedTask;
        }

    
        public async Task MessageTranslation()
        {
            await Task.CompletedTask;
        }

 
        public async Task EnableAutomaticTranslation()
        {
            await Task.CompletedTask;
        }

 
        public async Task SetUserLanguage()
        {
            await Task.CompletedTask;
        }

        private IErmisChatClient Client { get; } = ErmisChatClient.CreateDefaultClient();
    }
}