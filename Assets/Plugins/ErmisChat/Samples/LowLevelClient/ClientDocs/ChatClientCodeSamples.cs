using Ermis.Core.LowLevelClient;
using Ermis.Libs.Auth;

namespace ErmisChat.Samples.LowLevelClient.ClientDocs
{

    public class ChatClientCodeSamples
    {
        public void Initialize()
        {
            var authCredentials = new AuthCredentials(
                apiKey: "ERMIS_CHAT_API_KEY",
                userId: "USER_ID",
                userToken: "USER_TOKEN");

            var client = ErmisChatLowLevelClient.CreateDefaultClient(authCredentials);

            //Initialize connection with the Ermis Chat server
            client.Connect();
        }

        // public async Task ConnectUser()
        // {
        //     //TodoL implement switching users
        // }

        public void Disconnect()
        {
            //ErmisTodo: implement disconnecting user without disposing a client

            var authCredentials = new AuthCredentials(
                apiKey: "ERMIS_CHAT_API_KEY",
                userId: "USER_ID",
                userToken: "USER_TOKEN");

            var client = ErmisChatLowLevelClient.CreateDefaultClient(authCredentials);

            //Initialize connection with the Ermis Chat server
            client.Connect();

            client.Dispose();
        }
    }
}