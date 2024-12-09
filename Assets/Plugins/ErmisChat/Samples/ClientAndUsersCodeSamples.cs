using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.QueryBuilders.Filters;
using Ermis.Core.QueryBuilders.Filters.Users;
using Ermis.Core.QueryBuilders.Sort;
using Ermis.Core.Requests;
using Ermis.Libs.Auth;
using UnityEngine;

namespace ErmisChat.Samples
{
    internal sealed class ClientAndUsersCodeSamples
    {
      
        public async Task DeveloperTokens()
        {
            var userName = "The Amazing Tom";
            var userId = ErmisChatClient.SanitizeUserId(userName); // Remove disallowed characters
            var userToken = ErmisChatClient.CreateDeveloperAuthToken(userId);
            var credentials = new AuthCredentials("API_KEY", userId, userToken);

// Create chat client
            var client = ErmisChatClient.CreateDefaultClient();

// Connect user
            var localUserData = await client.ConnectUserAsync("API_KEY", userId, userToken);
        }

     
        public void InitClient()
        {
            var client = ErmisChatClient.CreateDefaultClient();
        }

    
        public async Task ConnectUser()
        {
            var client = ErmisChatClient.CreateDefaultClient();

            var localUserData = await client.ConnectUserAsync("api_key", "chat_user", "chat_user_token");
// After await is complete the user is connected

        }
 
        public async Task ConnectUser2()
        {
            var client = ErmisChatClient.CreateDefaultClient();

            await client.ConnectUserAsync("api_key", "chat_user", "chat_user_token");
// After await is complete the user is connected

// Alternatively, you subscribe to the IErmisChatClient.Connected event
            client.Connected += localUserData =>
            {
                // User is connected
            };
        }

      
        public async Task DisconnectUser()
        {
            var client = ErmisChatClient.CreateDefaultClient();
            await client.DisconnectUserAsync();
        }

 
        public void DeleteUser()
        {
            //ErmisTodo: Implement user delete
        }

     
        public async Task LogoutUser()
        {
            await Client.DisconnectUserAsync();
        }


        #region Managing Users

        
        public async Task UserUpdates()
        {
// Only Id field is required, the rest is optional
            var createOrUpdateUser = new ErmisUserUpsertRequest
            {
                Id = "my-user-id",
                // BanExpires = DateTimeOffset.Now.AddDays(7),
                // Banned = true,
                // Invisible = true,
                // Role = "user",
                // Name = "David",
                // Image = "image-url", // You can upload image to Ermis CDN or your own
                // CustomData = new ErmisCustomDataRequest
                //{
                //    { "Age", 24 },
                //    { "Passions", new string[] { "Tennis", "Football", "Basketball" } }
                //}
            };

// Upsert means: update user with a given ID or create a new one if it doesn't exist
            var users = await Client.UpsertUsers(new[] { createOrUpdateUser });
        }

        public async Task UserUpdatesMultiple()
        {
            var usersToCreateOrUpdate = new[]
            {
                new ErmisUserUpsertRequest
                {
                    Id = "my-user-id",
                    Role = "user",
                },
                new ErmisUserUpsertRequest
                {
                    Id = "my-user-id-2",
                    // BanExpires = DateTimeOffset.Now.AddDays(7),
                    // Banned = true,
                    // Invisible = true,
                    // Role = "user",
                    // Name = "David",
                    // Image = "image-url", // You can upload image to Ermis CDN or your own
                    // CustomData = new ErmisCustomDataRequest
                    //{
                    //    { "Age", 24 },
                    //    { "Passions", new string[] { "Tennis", "Football", "Basketball" } }
                    //}
                },
            };

// Upsert means: update user with a given ID or create a new one if it doesn't exist
            var users = await Client.UpsertUsers(usersToCreateOrUpdate);
        }

        #endregion

        #region Querying Users

     
        public async Task QueryUsers()
        {
            var filters = new IFieldFilterRule[]
            {
                UserFilter.Id.In("user-1", "user-2", "user-3")
            };
// Returns collection of IErmisUser
            var users = await Client.QueryUsersAsync(filters);
        }

    
        public async Task QueryUsersPagination()
        {
            var lastWeek = DateTime.Now.AddDays(-7);
            var filters = new IFieldFilterRule[]
            {
                UserFilter.CreatedAt.GreaterThanOrEquals(lastWeek)
            };

            // Order results by one or multiple fields e.g
            var sort = UsersSort.OrderByDescending(UserSortField.CreatedAt);

            var limit = 30; // How many records per page
            var offset = 0; // How many records to skip e.g. offset = 30 -> page 2, offset = 60 -> page 3, etc.
            
            // Returns collection of IErmisUser
            var users = await Client.QueryUsersAsync(filters, sort, offset, limit);
        }

   
        public async Task QueryUsersUsingAutocompleteByName()
        {
            var filters = new IFieldFilterRule[]
            {
                UserFilter.Name.Autocomplete("Ro")
            };
// Returns collection of IErmisUser
            var users = await Client.QueryUsersAsync(filters);
        }


        public async Task QueryUsersUsingAutocompleteById()
        {
            var filters = new IFieldFilterRule[]
            {
                // Return all users with Id starting with `Ro` like: Roxy, Roxanne, Rover
                UserFilter.Name.Autocomplete("Ro")
            };
// Returns collection of IErmisUser
            var users = await Client.QueryUsersAsync(filters);
        }

  
        public async Task QueryBannedUsers()
        {
// Returns collection of ErmisUserBanInfo
            var usersBanInfo = await Client.QueryBannedUsersAsync(new ErmisQueryBannedUsersRequest
            {
                CreatedAtAfter = null, // Optional Banned after this date
                CreatedAtAfterOrEqual = null, // Optional Banned after or equal this date
                CreatedAtBefore = null, // Optional Banned before this date
                CreatedAtBeforeOrEqual = null, // Optional Banned before or equal this date
                FilterConditions = null, // Optional filter
                Limit = 30,
                Offset = 60,
                Sort = new List<ErmisSortParam> // Optional sort
                {
                    new ErmisSortParam
                    {
                        Field = "created_at",
                        Direction = -1,
                    }
                },
            });

            foreach (var banInfo in usersBanInfo)
            {
                Debug.Log(banInfo.User); // Which user
                Debug.Log(banInfo.Channel); // From which channel
                Debug.Log(banInfo.Reason); // Reason why banned
                Debug.Log(banInfo.Expires); // Optional expiry date
                Debug.Log(banInfo.Shadow); // Is this a shadow ban
                Debug.Log(banInfo.BannedBy); // Who created a ban
                Debug.Log(banInfo.CreatedAt); // Date when banned
            }
        }

        #endregion

        private IErmisChatClient Client { get; } = ErmisChatClient.CreateDefaultClient();
    }
}