﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ermis.Core.Auth;
using Ermis.Core.LowLevelClient.API;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Libs.Auth;

namespace Ermis.Core.LowLevelClient
{
    /// <summary>
    /// Handler delegate for a connection state change
    /// </summary>
    public delegate void ConnectionStateChangeHandler(ConnectionState previous, ConnectionState current);
    
    /// <summary>
    /// Ermis Low-Level Chat Client - maintains WebSockets connection, executes API calls and exposes Ermis events to which you can subscribe.
    /// There should be only one instance of this client in your application. This client does NOT maintain state.
    ///
    /// Unless you have a good reason to use the low-level client, you should be using the stateful <see cref="IErmisChatClient"/> which maintain client state
    /// </summary>
    public interface IErmisChatLowLevelClient : IAuthProvider, IConnectionProvider, IErmisRealtimeEventsProvider, IDisposable
    {
        /// <summary>
        /// Client established WebSockets connection and is ready to send and receive data
        /// </summary>
        event ConnectionHandler Connected;
        
        /// <summary>
        /// Client is attempting to reconnect after lost connection
        /// </summary>
        event Action Reconnecting;

        /// <summary>
        /// Client lost connection with the server. if ReconnectStrategy is Exponential or Constant it will attempt to reconnect.
        /// Once Connected event is raised again you should re-init watch state for previously observed channels and re-fetch potentially missed data
        /// </summary>
        event Action Disconnected;

        /// <summary>
        /// Raised when connection state changes. Returns previous and the current connection state respectively
        /// </summary>
        event ConnectionStateChangeHandler ConnectionStateChanged;

        ConnectionState ConnectionState { get; }
        ReconnectStrategy ReconnectStrategy { get; }
        float ReconnectConstantInterval { get; }
        float ReconnectExponentialMinInterval { get; }
        float ReconnectExponentialMaxInterval { get; }
        double? NextReconnectTime { get; }

        IChannelApi ChannelApi { get; }
        IMessageApi MessageApi { get; }
        IModerationApi ModerationApi { get; }
        IUserApi UserApi { get; }
        IDeviceApi DeviceApi { get; }

        [Obsolete(
            "This property presents only initial state of the LocalUser when connection is made and is not being updated any further. " +
            "Please use the OwnUser object returned via ErmisChatClient.Connected event. This property will  be removed in the future.")]
        OwnUser LocalUser { get; }

        /// <summary>
        /// Per frame update of the ErmisChatClient. This method triggers sending and receiving data between the client and the server. Make sure to call it every frame.
        /// </summary>
        /// <param name="deltaTime"></param>
        void Update(float deltaTime);

        /// <summary>
        /// Initiate WebSocket connection with Ermis Server.
        /// Use <see cref="IConnectionProvider.Connected"/> to be notified when connection is established
        /// </summary>
        void Connect();

        bool IsLocalUser(User user);

        bool IsLocalUser(ChannelMember channelMember);

        /// <summary>
        /// Set parameters for ErmisChatClient reconnect strategy
        /// </summary>
        /// <param name="reconnectStrategy">Defines how Client will react to Disconnected state</param>
        /// <param name="exponentialMinInterval">Defines min reconnect interval for <see cref="Ermis.Core.ReconnectStrategy.Exponential"/></param>
        /// <param name="exponentialMaxInterval">Defines max reconnect interval for <see cref="Ermis.Core.ReconnectStrategy.Exponential"/></param>
        /// <param name="constantInterval">Defines reconnect interval for <see cref="Ermis.Core.ReconnectStrategy.Constant"/></param>
        /// <exception cref="ArgumentException">throws exception if intervals are less than or equal to zero</exception>
        void SetReconnectStrategySettings(ReconnectStrategy reconnectStrategy, float? exponentialMinInterval,
            float? exponentialMaxInterval, float? constantInterval);

        void ConnectUser(AuthCredentials userAuthCredentials);

        Task DisconnectAsync(bool permanent = false);

        Task FetchAndProcessEventsSinceLastReceivedEvent(IEnumerable<string> channelCids);

        /// <summary>
        /// Set authorization credentials for the client to use when connecting to the API
        /// </summary>
        /// <param name="authCredentials">Credentials containing: api key, user ID, and a user Token</param>
        void SeAuthorizationCredentials(AuthCredentials authCredentials);

        string GetUserToken();

        string GetProjectId();

    }
}