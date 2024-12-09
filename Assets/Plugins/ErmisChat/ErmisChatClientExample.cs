using Ermis.Core.StatefulModels;
using Ermis.Libs.Utils;
using UnityEngine;

namespace Ermis.Core
{
    /// <summary>
    /// Example showing how to create instance of <see cref="IErmisChatClient"/>
    /// </summary>
    public class ErmisChatClientExample : MonoBehaviour
    {
        public IErmisChatClient Client { get; private set; }
        
        void Start()
        {
            // Init 
            Client = ErmisChatClient.CreateDefaultClient();
            Client.Connected += OnConnected;

            
            Client.ConnectUserAsync("API_KEY", "USER_ID", "USER_TOKEN").LogIfFailed();
        }

        private void OnConnected(IErmisLocalUserData localUserData)
        {
            Debug.Log($"user {localUserData.User.Id} is now connected");
        }
    }
}

