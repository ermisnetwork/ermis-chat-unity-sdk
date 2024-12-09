using Ermis.Libs.AppInfo;
using Ermis.Libs.Auth;
using Ermis.Libs.ChatInstanceRunner;
using Ermis.Libs.Http;
using Ermis.Libs.Logs;
using Ermis.Libs.NetworkMonitors;
using Ermis.Libs.Serialization;
using Ermis.Libs.Time;
using Ermis.Libs.Websockets;
using UnityEngine;

namespace Ermis.Libs
{
    /// <summary>
    /// Factory that provides external dependencies for the Ermis Chat Client.
    /// Ermis chat client depends only on the interfaces therefore you can provide your own implementation for any of the dependencies
    /// </summary>
    public static class ErmisDependenciesFactory
    {
        public static ILogs CreateLogger(LogLevel logLevel = LogLevel.All)
            => new UnityLogs(logLevel);

        public static IWebsocketClient CreateWebsocketClient(ILogs logs, bool isDebugMode = false)
        {

#if UNITY_WEBGL
            //ErmisTodo: handle debug mode
            return new NativeWebSocketWrapper(logs, isDebugMode: isDebugMode);
#else
            return new WebsocketClient(logs, isDebugMode: isDebugMode);
#endif
        }

        public static IHttpClient CreateHttpClient()
        {
#if UNITY_WEBGL
            return new UnityWebRequestHttpClient();
#else
            return new HttpClientAdapter();
#endif
        }

        public static ISerializer CreateSerializer() => new NewtonsoftJsonSerializer();

        public static ITimeService CreateTimeService() => new UnityTime();

        public static IApplicationInfo CreateApplicationInfo() => new UnityApplicationInfo();
        
        public static ITokenProvider CreateTokenProvider(TokenProvider.TokenUriHandler urlFactory) => new TokenProvider(CreateHttpClient(), urlFactory);

        public static IErmisChatClientRunner CreateChatClientRunner()
        {
            var go = new GameObject
            {
                name = "Ermis Chat Client Runner",
#if !ERMIS_DEBUG_ENABLED
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideAndDontSave
#endif
            };
            return go.AddComponent<ErmisMonoBehaviourWrapper.UnityErmisChatClientRunner>();
        }

        public static INetworkMonitor CreateNetworkMonitor() => new UnityNetworkMonitor();
    }
}