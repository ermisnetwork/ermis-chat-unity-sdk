﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ermis.Core.Exceptions;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.Web;
using Ermis.Libs.Http;
using Ermis.Libs.Logs;
using Ermis.Libs.Serialization;


namespace Ermis.Core.LowLevelClient.API.Internal
{
    /// <summary>
    /// Base Api client
    /// </summary>
    internal abstract class InternalApiClientBase
    {
        protected InternalApiClientBase(IHttpClient httpClient, ISerializer serializer, ILogs logs,
            IRequestUriFactory requestUriFactory, IErmisChatLowLevelClient lowLevelClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _requestUriFactory = requestUriFactory ?? throw new ArgumentNullException(nameof(requestUriFactory));
            _lowLevelClient = lowLevelClient ?? throw new ArgumentNullException(nameof(lowLevelClient));
        }

        protected Task<TResponse> Get<TPayload, TResponse>(string endpoint, TPayload payload)
            => HttpRequest<TResponse>(HttpMethodType.Get, endpoint, payload);

        protected Task<TResponse> GetWithCustomHeader<TPayload, TResponse>(string endpoint, TPayload payload)
            => HttpRequestWithCustomHeader<TResponse>(HttpMethodType.Get, endpoint, payload);

        protected Task<TResponse> Get<TResponse>(string endpoint, QueryParameters parameters = null)
            => HttpRequest<TResponse>(HttpMethodType.Get, endpoint, queryParameters: parameters);

        protected Task<TResponse> GetWithCustomHeader<TResponse>(string endpoint, QueryParameters parameters = null)
           => HttpRequestWithCustomHeader<TResponse>(HttpMethodType.Get, endpoint, queryParameters: parameters);

        protected Task<TResponse> Post<TRequest, TResponse>(string endpoint, TRequest request)
            => HttpRequest<TResponse>(HttpMethodType.Post, endpoint, request);

        protected Task<TResponse> PostWithCustomHeader<TRequest, TResponse>(string endpoint, TRequest request)
            => HttpRequestWithCustomHeader<TResponse>(HttpMethodType.Post, endpoint, request);

        protected Task<TResponse> PostWithCustomHeader<TResponse>(string endpoint, object request)
           => HttpRequestWithCustomHeader<TResponse>(HttpMethodType.Post, endpoint, request);

        protected Task<TResponse> PosFileWithCustomHeader<TResponse>(string endpoint, FileWrapper request)
          => HttpRequestFileWithCustomHeader<TResponse>(HttpMethodType.Post, endpoint, request);

        protected Task<TResponse> Post<TResponse>(string endpoint, object request)
            => HttpRequest<TResponse>(HttpMethodType.Post, endpoint, request);

        protected Task<TResponse> Put<TRequest, TResponse>(string endpoint, TRequest request)
            => HttpRequest<TResponse>(HttpMethodType.Put, endpoint, request);

        protected Task<TResponse> Patch<TRequest, TResponse>(string endpoint, TRequest request)
            => HttpRequest<TResponse>(HttpMethodType.Patch, endpoint, request);

        protected Task<TResponse> Delete<TResponse>(string endpoint, QueryParameters parameters = null)
            => HttpRequest<TResponse>(HttpMethodType.Delete, endpoint, queryParameters: parameters);

        protected Task<TResponse> DeleteWithCustomHeader<TResponse>(string endpoint, QueryParameters parameters = null)
           => HttpRequestWithCustomHeader<TResponse>(HttpMethodType.Delete, endpoint, queryParameters: parameters);

        protected Task<TResponse> PostEventAsync<TRequest, TResponse>(string channelType, string channelId, TRequest eventBodyDto)
            => PostWithCustomHeader<TRequest, TResponse>(
                $"/channels/{channelType}/{channelId}/event", eventBodyDto);

        private const int InvalidAuthTokenErrorCode = 40;

        private readonly IHttpClient _httpClient;
        private readonly ISerializer _serializer;
        private readonly ILogs _logs;
        private readonly IRequestUriFactory _requestUriFactory;
        private readonly StringBuilder _sb = new StringBuilder();
        private readonly IErmisChatLowLevelClient _lowLevelClient;

        private object TrySerializeRequestBodyContent(object content, out string serializedContent)
        {
            serializedContent = default;

            if (content == null)
            {
                return null;
            }

            if (content is FileWrapper fileWrapper)
            {
                return fileWrapper;
            }

            serializedContent = _serializer.Serialize(content);
            return serializedContent;
        }

        private async Task<TResponse> HttpRequest<TResponse>(HttpMethodType httpMethod, string endpoint,
            object requestBody = default, QueryParameters queryParameters = null, int attempt = 0)
        {

            var httpContent = TrySerializeRequestBodyContent(requestBody, out var serializedContent);
            var logContent = serializedContent ?? httpContent?.ToString();
            if (httpMethod == HttpMethodType.Get && serializedContent != null)
            {
                if (queryParameters == null)
                {
                    queryParameters = QueryParameters.Default;
                }

                if (queryParameters.ContainsKey("payload"))
                {
                    queryParameters["payload"] = serializedContent;
                }
                else
                {
                    queryParameters.Append("payload", serializedContent);
                }
            }

            var uri = _requestUriFactory.CreateEndpointUri(endpoint, queryParameters);
            LogFutureRequestIfDebug(uri, endpoint, httpMethod, logContent);
            var httpResponse = await _httpClient.SendHttpRequestAsync(httpMethod, uri, httpContent);
            var responseContent = httpResponse.Result;

            if (!httpResponse.IsSuccessStatusCode)
            {
                APIErrorInternalDTO apiError;
                try
                {
                    apiError = _serializer.Deserialize<APIErrorInternalDTO>(responseContent);
                }
                catch (Exception e)
                {
                    // ErmisTODO: verify where this error is coming from. This occurs when running tests in a docker container. Perhaps it's not returned from the API but by the network layer
                    if (responseContent == "upstream request timeout")
                    {
                        // Handle API error returned as plain text
                        apiError = new APIErrorInternalDTO
                        {
                            Message = responseContent,
                            Code = 504,
                        };

#if !ERMIS_TESTS_ENABLED
                        throw new ErmisApiException(apiError);
#else
                        if (attempt >= 20)
                        {
                            throw new ErmisApiException(apiError);
                        }

                        _logs.Warning($"API CLIENT, TESTS MODE, Upstream Request Timeout - Make another attempt");
                        return await HttpRequest<TResponse>(httpMethod, endpoint,
                            requestBody, queryParameters, ++attempt);
#endif
                    }

                    LogRestCall(uri, endpoint, httpMethod, responseContent, success: false, logContent);

#if ERMIS_TESTS_ENABLED || ERMIS_DEBUG_ENABLED
                    var sb = new StringBuilder();
                    sb.AppendLine("API Response Deserialization failed - ErmisDeserializationException:");
                    sb.AppendLine("Target type: " + typeof(APIErrorInternalDTO));
                    sb.AppendLine("Content:");
                    sb.AppendLine(responseContent);
                    _logs.Error(sb.ToString());
#endif

                    throw new ErmisDeserializationException(responseContent, typeof(TResponse), e);
                }

#if ERMIS_TESTS_ENABLED
                if (apiError.StatusCode == ErmisApiException.RateLimitErrorHttpStatusCode && attempt < 50)
                {
                    return await HandleRateLimit<TResponse>(httpMethod, endpoint, requestBody, queryParameters, attempt,
                        httpResponse);
                }
#endif

                if (apiError.Code != InvalidAuthTokenErrorCode)
                {
                    LogRestCall(uri, endpoint, httpMethod, responseContent, success: false, logContent);
                    throw new ErmisApiException(apiError);
                }

                if (_lowLevelClient.ConnectionState == ConnectionState.Connected)
                {
                    _logs.Info(
                        $"Http request failed due to expired token, connection id: {_lowLevelClient.ConnectionId}");
                    await _lowLevelClient.DisconnectAsync();
                }

                _logs.Info("New token required, connection state: " + _lowLevelClient.ConnectionState);

                const int maxMsToWait = 500;
                var i = 0;

                //ErmisTodo: we can create cancellation token instead of Task.Delay in loop
                while (_lowLevelClient.ConnectionState != ConnectionState.Connected)
                {
                    i++;
                    await Task.Delay(1);

                    if (i > maxMsToWait)
                    {
                        break;
                    }
                }

                if (_lowLevelClient.ConnectionState != ConnectionState.Connected)
                {
                    throw new TimeoutException(
                        "Request reached timout when waiting for client to reconnect after auth token refresh");
                }

                // Recreate the uri to include new connection id 
                uri = _requestUriFactory.CreateEndpointUri(endpoint, queryParameters);

                httpResponse = await _httpClient.SendHttpRequestAsync(httpMethod, uri, httpContent);
                responseContent = httpResponse.Result;
            }

            try
            {
                var response = _serializer.Deserialize<TResponse>(responseContent);
                LogRestCall(uri, endpoint, httpMethod, responseContent, success: true, logContent);
                return response;
            }
            catch (Exception e)
            {
                LogRestCall(uri, endpoint, httpMethod, responseContent, success: false, logContent);

#if ERMIS_TESTS_ENABLED || ERMIS_DEBUG_ENABLED
                var sb = new StringBuilder();
                sb.AppendLine("API Response Deserialization failed - ErmisDeserializationException:");
                sb.AppendLine("Target type: " + typeof(APIErrorInternalDTO));
                sb.AppendLine("Content:");
                sb.AppendLine(responseContent);
                _logs.Error(sb.ToString());
#endif

                throw new ErmisDeserializationException(responseContent, typeof(TResponse), e);
            }
        }

        private async Task<TResponse> HttpRequestWithCustomHeader<TResponse>(HttpMethodType httpMethod, string endpoint,
             object requestBody = default, QueryParameters queryParameters = null, int attempt = 0)
        {
            //ErmisTodo: perhaps remove this requirement, sometimes we send empty body without any properties
            //if (requestBody == null && IsRequestBodyRequiredByHttpMethod(httpMethod))
            //{
            //    throw new ArgumentException($"{nameof(requestBody)} is required by {httpMethod}");
            //}

            var httpContent = TrySerializeRequestBodyContent(requestBody, out var serializedContent);
            var logContent = serializedContent ?? httpContent?.ToString();
            if (httpMethod == HttpMethodType.Get && serializedContent != null)
            {
                if (queryParameters == null)
                {
                    queryParameters = QueryParameters.Default;
                }

                if (queryParameters.ContainsKey("payload"))
                {
                    queryParameters["payload"] = serializedContent;
                }
                else
                {
                    queryParameters.Append("payload", serializedContent);
                }
            }

            //if(httpMethod == HttpMethodType.Post &&)
            var uri = _requestUriFactory.CreateEndpointUri(endpoint, queryParameters);
            LogFutureRequestIfDebug(uri, endpoint, httpMethod, logContent);
            _httpClient.SetDefaultAuthenticationHeader("Bearer", _lowLevelClient.GetUserToken());
            _httpClient.AddDefaultCustomHeaderAccept("application/json");
            var httpResponse = await _httpClient.SendHttpRequestAsyncCustomHeader(httpMethod, uri, httpContent);
            var responseContent = httpResponse.Result;
            if (!httpResponse.IsSuccessStatusCode)
            {
                APIErrorInternalDTO apiError;
                try
                {
                    apiError = _serializer.Deserialize<APIErrorInternalDTO>(responseContent);
                }
                catch (Exception e)
                {
                    // ErmisTODO: verify where this error is coming from. This occurs when running tests in a docker container. Perhaps it's not returned from the API but by the network layer
                    if (responseContent == "upstream request timeout")
                    {
                        // Handle API error returned as plain text
                        apiError = new APIErrorInternalDTO
                        {
                            Message = responseContent,
                            Code = 504,
                        };

#if !ERMIS_TESTS_ENABLED
                        throw new ErmisApiException(apiError);
#else
                        if (attempt >= 20)
                        {
                            throw new ErmisApiException(apiError);
                        }

                        _logs.Warning($"API CLIENT, TESTS MODE, Upstream Request Timeout - Make another attempt");
                        return await HttpRequest<TResponse>(httpMethod, endpoint,
                            requestBody, queryParameters, ++attempt);
#endif
                    }

                    LogRestCall(uri, endpoint, httpMethod, responseContent, success: false, logContent);

#if ERMIS_TESTS_ENABLED || ERMIS_DEBUG_ENABLED
                    var sb = new StringBuilder();
                    sb.AppendLine("API Response Deserialization failed - ErmisDeserializationException:");
                    sb.AppendLine("Target type: " + typeof(APIErrorInternalDTO));
                    sb.AppendLine("Content:");
                    sb.AppendLine(responseContent);
                    _logs.Error(sb.ToString());
#endif

                    throw new ErmisDeserializationException(responseContent + ":" + httpContent, typeof(TResponse), e);
                }

#if ERMIS_TESTS_ENABLED
                if (apiError.StatusCode == ErmisApiException.RateLimitErrorHttpStatusCode && attempt < 50)
                {
                    return await HandleRateLimit<TResponse>(httpMethod, endpoint, requestBody, queryParameters, attempt,
                        httpResponse);
                }
#endif

                if (apiError.Code != InvalidAuthTokenErrorCode)
                {
                    LogRestCall(uri, endpoint, httpMethod, responseContent, success: false, logContent);
                    throw new ErmisApiException(apiError);
                }

                if (_lowLevelClient.ConnectionState == ConnectionState.Connected)
                {
                    _logs.Info(
                        $"Http request failed due to expired token, connection id: {_lowLevelClient.ConnectionId}");
                    await _lowLevelClient.DisconnectAsync();
                }

                _logs.Info("New token required, connection state: " + _lowLevelClient.ConnectionState);

                const int maxMsToWait = 500;
                var i = 0;

                //ErmisTodo: we can create cancellation token instead of Task.Delay in loop
                while (_lowLevelClient.ConnectionState != ConnectionState.Connected)
                {
                    i++;
                    await Task.Delay(1);

                    if (i > maxMsToWait)
                    {
                        break;
                    }
                }

                if (_lowLevelClient.ConnectionState != ConnectionState.Connected)
                {
                    throw new TimeoutException(
                        "Request reached timout when waiting for client to reconnect after auth token refresh");
                }

                // Recreate the uri to include new connection id 
                uri = _requestUriFactory.CreateEndpointUri(endpoint, queryParameters);

                httpResponse = await _httpClient.SendHttpRequestAsync(httpMethod, uri, httpContent);
                responseContent = httpResponse.Result;
            }

            try
            {
                var response = _serializer.Deserialize<TResponse>(responseContent);
                LogRestCall(uri, endpoint, httpMethod, responseContent, success: true, logContent);
                return response;
            }
            catch (Exception e)
            {
                LogRestCall(uri, endpoint, httpMethod, responseContent, success: false, logContent);

#if ERMIS_TESTS_ENABLED || ERMIS_DEBUG_ENABLED
                var sb = new StringBuilder();
                sb.AppendLine("API Response Deserialization failed - ErmisDeserializationException:");
                sb.AppendLine("Target type: " + typeof(APIErrorInternalDTO));
                sb.AppendLine("Content:");
                sb.AppendLine(responseContent);
                _logs.Error(sb.ToString());
#endif

                throw new ErmisDeserializationException(responseContent + ":" + httpContent, typeof(TResponse), e);
            }
        }

        private async Task<TResponse> HttpRequestFileWithCustomHeader<TResponse>(HttpMethodType httpMethod, string endpoint,
            FileWrapper requestBody = default, QueryParameters queryParameters = null, int attempt = 0)
        {

            var uri = _requestUriFactory.CreateEndpointUri(endpoint, queryParameters);
            _httpClient.SetDefaultAuthenticationHeader("Bearer", _lowLevelClient.GetUserToken());
            _httpClient.AddDefaultCustomHeaderAccept("application/json");

            var formData = new MultipartFormDataContent();
            formData.Add(new ByteArrayContent(requestBody.FileContent), "file", Path.GetFileName(requestBody.FileName));

            var httpResponse = await _httpClient.SendHttpRequestAsyncCustomHeader(httpMethod, uri, formData);
            var responseContent = httpResponse.Result;
            if (!httpResponse.IsSuccessStatusCode)
            {
                APIErrorInternalDTO apiError;
                try
                {
                    apiError = _serializer.Deserialize<APIErrorInternalDTO>(responseContent);
                }
                catch (Exception e)
                {
                    // ErmisTODO: verify where this error is coming from. This occurs when running tests in a docker container. Perhaps it's not returned from the API but by the network layer
                    if (responseContent == "upstream request timeout")
                    {
                        // Handle API error returned as plain text
                        apiError = new APIErrorInternalDTO
                        {
                            Message = responseContent,
                            Code = 504,
                        };

#if !ERMIS_TESTS_ENABLED
                        throw new ErmisApiException(apiError);
#else
                        if (attempt >= 20)
                        {
                            throw new ErmisApiException(apiError);
                        }

                        _logs.Warning($"API CLIENT, TESTS MODE, Upstream Request Timeout - Make another attempt");
                        return await HttpRequest<TResponse>(httpMethod, endpoint,
                            requestBody, queryParameters, ++attempt);
#endif
                    }


#if ERMIS_TESTS_ENABLED || ERMIS_DEBUG_ENABLED
                    var sb = new StringBuilder();
                    sb.AppendLine("API Response Deserialization failed - ErmisDeserializationException:");
                    sb.AppendLine("Target type: " + typeof(APIErrorInternalDTO));
                    sb.AppendLine("Content:");
                    sb.AppendLine(responseContent);
                    _logs.Error(sb.ToString());
#endif

                    throw new ErmisDeserializationException(responseContent, typeof(TResponse), e);
                }

#if ERMIS_TESTS_ENABLED
                if (apiError.StatusCode == ErmisApiException.RateLimitErrorHttpStatusCode && attempt < 50)
                {
                    return await HandleRateLimit<TResponse>(httpMethod, endpoint, requestBody, queryParameters, attempt,
                        httpResponse);
                }
#endif

                if (apiError.Code != InvalidAuthTokenErrorCode)
                {
                    throw new ErmisApiException(apiError);
                }

                if (_lowLevelClient.ConnectionState == ConnectionState.Connected)
                {
                    _logs.Info(
                        $"Http request failed due to expired token, connection id: {_lowLevelClient.ConnectionId}");
                    await _lowLevelClient.DisconnectAsync();
                }

                _logs.Info("New token required, connection state: " + _lowLevelClient.ConnectionState);

                const int maxMsToWait = 500;
                var i = 0;

                //ErmisTodo: we can create cancellation token instead of Task.Delay in loop
                while (_lowLevelClient.ConnectionState != ConnectionState.Connected)
                {
                    i++;
                    await Task.Delay(1);

                    if (i > maxMsToWait)
                    {
                        break;
                    }
                }

                if (_lowLevelClient.ConnectionState != ConnectionState.Connected)
                {
                    throw new TimeoutException(
                        "Request reached timout when waiting for client to reconnect after auth token refresh");
                }

                // Recreate the uri to include new connection id 
                uri = _requestUriFactory.CreateEndpointUri(endpoint, queryParameters);

                httpResponse = await _httpClient.SendHttpRequestAsync(httpMethod, uri, formData);
                responseContent = httpResponse.Result;
            }

            try
            {
                var response = _serializer.Deserialize<TResponse>(responseContent);
                return response;
            }
            catch (Exception e)
            {

#if ERMIS_TESTS_ENABLED || ERMIS_DEBUG_ENABLED
                var sb = new StringBuilder();
                sb.AppendLine("API Response Deserialization failed - ErmisDeserializationException:");
                sb.AppendLine("Target type: " + typeof(APIErrorInternalDTO));
                sb.AppendLine("Content:");
                sb.AppendLine(responseContent);
                _logs.Error(sb.ToString());
#endif

                throw new ErmisDeserializationException(responseContent, typeof(TResponse), e);
            }
        }

        private static bool IsRequestBodyRequiredByHttpMethod(HttpMethodType httpMethod)
            => httpMethod == HttpMethodType.Post || httpMethod == HttpMethodType.Put ||
               httpMethod == HttpMethodType.Patch;

        private void LogFutureRequestIfDebug(Uri uri, string endpoint, HttpMethodType httpMethod, string request = null)
        {
#if ERMIS_DEBUG_ENABLED
            _sb.Clear();

            _sb.Clear();
            _sb.Append("API Call: ");
            _sb.Append(httpMethod);
            _sb.Append(" ");
            _sb.Append(endpoint);
            _sb.Append(Environment.NewLine);
            _sb.Append("Full uri: ");
            _sb.Append(uri);
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);

            if (request != null)
            {
                _sb.AppendLine("Request:");
                _sb.AppendLine(request);
                _sb.Append(Environment.NewLine);
            }

            _logs.Info(_sb.ToString());
#endif
        }

        private void LogRestCall(Uri uri, string endpoint, HttpMethodType httpMethod, string response, bool success,
            string request = null)
        {
            _sb.Clear();
            _sb.Append("API Call: ");
            _sb.Append(httpMethod);
            _sb.Append(" ");
            _sb.Append(endpoint);
            _sb.Append(Environment.NewLine);
            _sb.Append("Status: ");
            _sb.Append(success ? "<color=green>SUCCESS</color>" : "<color=red>FAILURE</color>");
            _sb.Append(Environment.NewLine);
            _sb.Append("Full uri: ");
            _sb.Append(uri);
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);

            if (request != null)
            {
                _sb.AppendLine("Request:");
                _sb.AppendLine(request);
                _sb.Append(Environment.NewLine);
            }

            _sb.AppendLine("Response:");
            _sb.AppendLine(response);
            _sb.Append(Environment.NewLine);

            _logs.Info(_sb.ToString());
        }

        private async Task<TResponse> HandleRateLimit<TResponse>(HttpMethodType httpMethod, string endpoint,
            object requestBody, QueryParameters queryParameters, int attempt, HttpResponse httpResponse)
        {
            if (attempt >= 50)
            {
                throw new ErmisApiException(new APIErrorInternalDTO
                { Code = ErmisApiException.RateLimitErrorHttpStatusCode });
            }

            var delaySeconds = GetBackoffDelay(attempt, httpResponse, out var resetHeaderTimestamp);
            var now = (int)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            _logs.Warning($"API CLIENT, TESTS MODE, Rate Limit API Error - Wait for {delaySeconds} seconds. " +
                          $"Timestamp reset header: {resetHeaderTimestamp}, Current timestamp: {now}, Dif: {resetHeaderTimestamp - now}");
            await Task.Delay(delaySeconds * 1000);
            return await HttpRequest<TResponse>(httpMethod, endpoint, requestBody, queryParameters, ++attempt);
        }

        private int GetBackoffDelay(int attempt, HttpResponse httpResponse, out int resetHeaderTimestamp)
        {
            resetHeaderTimestamp = -1;
            // ErmisTodo: Backoff based on the header doesn't seem to work. Perhaps concurrency is conflicting with this approach
            if (httpResponse.TryGetHeader("x-ratelimit-reset", out var values))
            {
                var resetTimestamp = values.FirstOrDefault();

                if (int.TryParse(resetTimestamp, out var rateLimitTimestamp))
                {
                    resetHeaderTimestamp = rateLimitTimestamp;
                    var now = (int)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                    var secondsLeft = rateLimitTimestamp - now;
                    // if (secondsLeft > 0)
                    // {
                    //     return secondsLeft + 5;
                    // }
                }
            }

            return 61 + attempt * 20;
        }
    }
}