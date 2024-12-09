using Ermis.Core.LowLevelClient;
using Ermis.Libs.Auth;
using UnityEngine;

namespace ErmisChat.Samples.LowLevelClient
{
    /// <summary>
    /// Sample MonoBehaviour that creates and updates instance of <see cref="ErmisChatLowLevelClient"/>
    /// </summary>
    public class ChatBehaviourSample : MonoBehaviour
    {
        public IErmisChatLowLevelClient LowLevelClient { get; private set; }

        protected void Awake()
        {
            //Create AuthCredentials or optionally you can set them in asset and use _authCredentialsAsset.Credentials
            var authCredentials = new AuthCredentials(
                apiKey: "ERMIS_CHAT_API_KEY",
                userId: "USER_ID",
                userToken: "USER_TOKEN");

            LowLevelClient = ErmisChatLowLevelClient.CreateDefaultClient(authCredentials);

            //Initialize connection with the Ermis Chat server
            LowLevelClient.Connect();
        }

        //Client needs to be updated per frame in order to maintain websocket connection and exchange data with API
        protected void Update() => LowLevelClient?.Update(Time.deltaTime);

        //Client needs to be disposed once its no longer needed in order to close the connection
        protected void OnDestroy() => LowLevelClient?.Dispose();

        //Optionally you can use this asset to store credentials
        [SerializeField]
        private AuthCredentialsAsset _authCredentialsAsset;
    }
}