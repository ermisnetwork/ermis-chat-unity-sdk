﻿using System;
using System.Collections.Generic;
using Ermis.Libs.Serialization;
using Ermis.Libs.Utils;
using Ermis.Core.Auth;
using Ermis.Core.LowLevelClient;
using Ermis.Core.LowLevelClient.Requests;
using Ermis.Core.LowLevelClient.Requests.DTO;

namespace Ermis.Core.Web
{
    /// <summary>
    /// Requests Uri Factory
    /// </summary>
    internal class RequestUriFactory : IRequestUriFactory
    {
        public RequestUriFactory(IAuthProvider authProvider, IErmisChatLowLevelClient connectionProvider,
            ISerializer serializer)
        {
            _authProvider = authProvider ?? throw new ArgumentNullException(nameof(authProvider));
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public Uri CreateConnectionUri()
        {
            var connectPayloadDTO = new ConnectPayload
            {
                UserId = _authProvider.UserId,
                User = new UserObjectRequest()
                {
                    Id = _authProvider.UserId
                },
                UserToken = _authProvider.UserToken,
                ServerDeterminesConnectionId = true
            };

            var serializedPayload = _serializer.Serialize(connectPayloadDTO);

            var uriParams = new Dictionary<string, string>
            {
                { "json", Uri.EscapeDataString(serializedPayload) },
                { "api_key", _authProvider.ApiKey },
                { "authorization", _authProvider.UserToken },
                { "ermis-auth-type", _authProvider.ErmisAuthType },
            };

            var uriBuilder = new UriBuilder(_connectionProvider.ServerUri)
            { Path = "connect", Query = uriParams.ToQueryParameters() };

            return uriBuilder.Uri;
        }

        public Uri CreateEndpointUri(string endpoint, Dictionary<string, string> parameters = null)
        {
            var requestParameters = GetDefaultParameters();

            if (parameters != null)
            {
                foreach (var p in parameters.Keys)
                {
                    requestParameters[p] = parameters[p];
                }
            }

            return CreateRequestUri(endpoint, requestParameters);
        }

        private readonly IAuthProvider _authProvider;
        private readonly ISerializer _serializer;
        private readonly IErmisChatLowLevelClient _connectionProvider;

        private Dictionary<string, string> GetDefaultParameters() =>
            new Dictionary<string, string>
            {
                { "project_id", _authProvider.UserId },
                { "api_key", _authProvider.ApiKey },
                { "connection_id", _connectionProvider.ConnectionId },
            };

        private Uri CreateRequestUri(string endPoint, IReadOnlyDictionary<string, string> parameters)
        {
            if (parameters != null)
                return CreateRequestUri(endPoint, parameters.ToQueryParameters());
            else
                return CreateRequestUri(endPoint, "");
        }


        private Uri CreateRequestUri(string endPoint, string query)
        {
            var uriBuilder = new UriBuilder(_connectionProvider.ServerUri)
            { Path = endPoint, Scheme = "https", Query = query };

            return uriBuilder.Uri;
        }
    }
}