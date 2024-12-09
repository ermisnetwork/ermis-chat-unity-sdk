using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.Models;
using Ermis.Core.QueryBuilders.Filters;
using Ermis.Core.QueryBuilders.Filters.Channels;
using Ermis.Core.QueryBuilders.Filters.Users;
using Ermis.Core.QueryBuilders.Sort;
using Ermis.Core.Requests;
using Ermis.Core.StatefulModels;
using UnityEngine;

namespace ErmisChat.Samples
{
    internal sealed class ChannelsCodeSamples
    {
        #region Creating Channels

       
        public async Task CreateChannelUsingId()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, "my-channel-id");

// Once you get or query a channel it is also added to Client.WatchedChannels list
        }

        
        public async Task CreateChannelForListOfMembers()
        {
            var filters = new IFieldFilterRule[]
            {
                UserFilter.Id.EqualsTo("other-user-id")
            };
// Find user you want to start a chat with
            var users = await Client.QueryUsersAsync(filters);

            var otherUser = users.First();
            var localUser = Client.LocalUserData.User;

// Start direct channel between 2 users
            var channel = await Client.CreateChannelWithMembersAsync(ChannelType.Messaging,
                new[] { localUser, otherUser });
        }

        #endregion

        #region Watch a channel

      
        public async Task StartWatchingChannel()
        {
            var filters = new IFieldFilterRule[]
            {
                UserFilter.Id.EqualsTo("other-user-id")
            };
// find user you want to start chat with
            var users = await Client.QueryUsersAsync(filters);

            var otherUser = users.First();
            var localUser = Client.LocalUserData.User;

// Get channel by ID
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

// Get channel with users combination
            var channelWithUsers
                = await Client.CreateChannelWithMembersAsync(ChannelType.Messaging,
                    new[] { localUser, otherUser });

// Access properties
            Debug.Log(channel.Name);
            Debug.Log(channel.Members);
            Debug.Log(channel.Name);
            Debug.Log(channel.Name);

// Subscribe to events so you can react to updates
            channel.MessageReceived += OnMessageReceived;
            channel.MessageUpdated += OnMessageUpdated;
            channel.MessageDeleted += OnMessageDeleted;
            channel.ReactionAdded += OnReactionAdded;
            channel.ReactionUpdated += OnReactionUpdated;
            channel.ReactionRemoved += OnReactionRemoved;

// You can also access all currently watched channels via
            foreach (var c in Client.WatchedChannels)
            {
                // Every queried channel is automatically watched and starts receiving updates from the server
            }
        }

        private void OnReactionRemoved(IErmisChannel channel, IErmisMessage message, ErmisReaction reaction)
        {
        }

        private void OnReactionUpdated(IErmisChannel channel, IErmisMessage message, ErmisReaction reaction)
        {
        }

        private void OnReactionAdded(IErmisChannel channel, IErmisMessage message, ErmisReaction reaction)
        {
        }

        private void OnMessageDeleted(IErmisChannel channel, IErmisMessage message, bool isharddelete)
        {
        }

        private void OnMessageUpdated(IErmisChannel channel, IErmisMessage message)
        {
        }

        private void OnMessageReceived(IErmisChannel channel, IErmisMessage message)
        {
        }

      
        public async Task StopWatchingChannel()
        {
            var channel = Client.WatchedChannels.First();

            await channel.StopWatchingAsync();
        }

   
        public async Task WatcherCount()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            Debug.Log(channel.WatcherCount);
        }

     
        public async Task PaginateChannelWatchers()
        {
            //ErmisTodo: IMPLEMENT watchers pagination
            await Task.CompletedTask;
        }

     
        public async Task ListenToChangesInWatchers()
        {
            // Get IErmisChannel reference by Client.GetOrCreateChannel or Client.QueryChannels
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            // Subscribe to events
            channel.WatcherAdded += OnWatcherAdded;
            channel.WatcherRemoved += OnWatcherRemoved;
        }

        private void OnWatcherAdded(IErmisChannel channel, IErmisUser user)
        {
        }

        private void OnWatcherRemoved(IErmisChannel channel, IErmisUser user)
        {
        }

        #endregion

        #region Updating a Channel

    
        public async Task PartialUpdate()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            var setClanInfo = new ClanData
            {
                MaxMembers = 50,
                Name = "Wild Boards",
                Tags = new List<string>
                {
                    "Competitive",
                    "Legendary",
                }
            };

            var setFields = new Dictionary<string, object>();

            // Set custom values
            setFields.Add("frags", 5);
            // Set custom arrays
            setFields.Add("items", new[] { "sword", "shield" });
            // Set custom class objects
            setFields.Add("clan_info", setClanInfo);

            // Send data
            await channel.UpdatePartialAsync(setFields);

            // Data is now available via CustomData property
            var frags = channel.CustomData.Get<int>("frags");
            var items = channel.CustomData.Get<List<string>>("items");
            var clanInfo = channel.CustomData.Get<ClanData>("clan_info");
        }

// Example class with data that you can assign as Channel custom data
        private class ClanData
        {
            public int MaxMembers;
            public string Name;
            public List<string> Tags;
        }

      
        public async Task FullUpdate()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            var updateRequest = new ErmisUpdateOverwriteChannelRequest
            {
                Name = "New name",
                CustomData = new ErmisCustomDataRequest
                {
                    { "my-custom-int", 12 },
                    { "my-custom-array", new string[] { "one", "two" } }
                }
            };

// This request will have all channel data removed except what is being passed in the request
            await channel.UpdateOverwriteAsync(updateRequest);

// You can also pass an instance of channel to the request constructor to have all of the date copied over
// This way you alter only the fields you wish to change
            var updateRequest2 = new ErmisUpdateOverwriteChannelRequest(channel)
            {
                Name = "Bran new name"
            };

// This will update only the name because all other data was copied over
            await channel.UpdateOverwriteAsync(updateRequest2);
        }

        #endregion

        #region Updating Channel Members

      
        public async Task AddingAndRemovingChannelMembers()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            var filters = new IFieldFilterRule[]
            {
                UserFilter.Id.In("other-user-id-1", "other-user-id-2", "other-user-id-3")
            };

            var users = await Client.QueryUsersAsync(filters);

// Add IErmisUser collection as a members
            await channel.AddMembersAsync(users);

// Or add by ID
            await channel.AddMembersAsync(hideHistory: default, optionalMessage: default, "some-user-id-1",
                "some-user-id-2");

// Access channel members via channel.Members, let's remove the first member as an example
            var member = channel.Members.First();
            await channel.RemoveMembersAsync(member);

// Remove local user from a channel by user ID
            var localUser = Client.LocalUserData.User;
            await channel.RemoveMembersAsync(localUser.Id);

// Remove multiple users by their ID
            await channel.RemoveMembersAsync("some-user-id-1", "some-user-id-2");
        }

       
        public async Task AddMembersWithMessage()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            var filters = new IFieldFilterRule[]
            {
                UserFilter.Id.In("other-user-id-1", "other-user-id-2", "other-user-id-3")
            };

            var users = await Client.QueryUsersAsync(filters);

            await channel.AddMembersAsync(users, hideHistory: default, new ErmisMessageRequest
            {
                Text = "John has joined the channel"
            });
        }

      
        public async Task AddMembersAndHideHistory()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            var filters = new IFieldFilterRule[]
            {
                UserFilter.Id.In("other-user-id-1", "other-user-id-2", "other-user-id-3")
            };

            var users = await Client.QueryUsersAsync(filters);

            await channel.AddMembersAsync(users, hideHistory: true);
        }

        public async Task LeaveChannel()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var member = channel.Members.First();

            await channel.RemoveMembersAsync(member);
        }

  
        public async Task AddAndRemoveModeratorsToChanel()
        {
            // Only Server-side SDK
            await Task.CompletedTask;
        }

        #endregion

        #region Querying Channels

  
    

        #endregion

        #region Querying Members

      
        public async Task QueryMembers()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            var filters = new Dictionary<string, object>
            {
                {
                    "id", new Dictionary<string, object>
                    {
                        { "$in", new[] { "user-id-1", "user-id-2" } }
                    }
                }
            };

// Pass limit and offset to control the page or results returned
// Limit - how many records per page
// Offset - how many records to skip
            var membersResult = await channel.QueryMembersAsync(filters, limit: 30, offset: 0);
        }

        #endregion

        #region Channel Pagination

     
        public async Task ChannelPaginateMessages()
        {
// Channel is loaded with the most recent messages
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

// Every call will load 1 more page of messages
            await channel.LoadOlderMessagesAsync();
        }

        #endregion

     
        public async Task ChannelPaginateMembers()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            // ErmisTodo: IMPLEMENT channel members pagination
        }

       
        public async Task ChannelPaginateWatchers()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            // ErmisTodo: IMPLEMENT channel watchers pagination
        }

       
        public async Task ChannelCapabilities()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            if (channel.OwnCapabilities.Contains("update-own-message"))
            {
                // User can update own message
            }

        }

      
        public async Task Invite()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

            var filters = new IFieldFilterRule[]
            {
                UserFilter.Id.In("other-user-id-1", "other-user-id-2", "other-user-id-3")
            };

            var users = await Client.QueryUsersAsync(filters);

            await channel.InviteMembersAsync(users);

            await channel.InviteMembersAsync("some-user-id-1", "some-user-id-2");

            //ErmisTodo: IMPLEMENT send invite
            await Task.CompletedTask;
        }

    
        public async Task AcceptInvite()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            await channel.AcceptInviteAsync();
        }

       
        public async Task RejectInvite()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            await channel.RejectInviteAsync();
        }

     
      

       
      
        public async Task MuteChannel()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            await channel.MuteChannelAsync();
        }

 
        public async Task QueryMutedChannels()
        {
            //ErmisTodo: IMPLEMENT query muted channels
            await Task.CompletedTask;
        }

        
        public async Task RemoveChannelMute()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            await channel.UnmuteChannelAsync();
        }

    
        public async Task HideAndShowChannel()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

// Hide a channel
            await channel.HideAsync();

// Show previously hidden channel
            await channel.ShowAsync();
        }

   
        public async Task DisableChannel()
        {
            // Feature only available via a server-side SDK
            await Task.CompletedTask;
        }

  
        public async Task FreezeChannel()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            await channel.FreezeAsync();
        }

 
        public async Task UnfreezeChannel()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            await channel.UnfreezeAsync();
        }

 
        public async Task GrantingTheFrozenChannelPermission()
        {
            //ErmisTodo: IMPLEMENT granting frozen channel permissions
            await Task.CompletedTask;
        }

    
        public async Task DeleteChannel()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");

// In Unity SDK you can only soft delete the channel. If you wish to hard delete you can only do it with server-side SDK
            await channel.DeleteAsync();
        }

    
        public async Task DeleteManyChannels()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");
            var channel2
                = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id-2");

// Hard delete removes the channel entirely from the database while soft delete removes the from users to see but it's still accessible via server-side SDK as an archive
            await Client.DeleteMultipleChannelsAsync(new[] { channel, channel2 }, isHardDelete: true);
        }


        public async Task TruncateChannel()
        {
            var channel = await Client.CreateChannelWithIdAsync(ChannelType.Messaging, channelId: "my-channel-id");


            await channel.TruncateAsync(systemMessage: "Clearing up the history!");
        }


        public async Task ThrottleAndSlowMode()
        {
            //ErmisTodo: IMPLEMENT Throttle and slow mode
            await Task.CompletedTask;
        }

        private IErmisChatClient Client { get; } = ErmisChatClient.CreateDefaultClient();
    }
}