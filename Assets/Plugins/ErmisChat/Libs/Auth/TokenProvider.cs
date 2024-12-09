using System;
using System.Threading.Tasks;
using Ermis.Libs.Http;

namespace Ermis.Libs.Auth
{
    /// <summary>
    /// Simple implementation of <see cref="ITokenProvider"/>
    /// You can create instance of this object using <see cref="ErmisDependenciesFactory.CreateTokenProvider"/>
    /// </summary>
    public class TokenProvider : ITokenProvider
    {
        public delegate Uri TokenUriHandler(string apiKey);

        public TokenProvider(IHttpClient httpClient, TokenUriHandler urlFactory)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _urlFactory = urlFactory ?? throw new ArgumentNullException(nameof(urlFactory));
        }

        public async Task<string> GetTokenAsync(string apiKey, string tokenHeader)
        {
            var uri = _urlFactory(apiKey);
            if (!string.IsNullOrEmpty(tokenHeader))
            {
                _httpClient.SetDefaultAuthenticationHeader("Bearer", tokenHeader);
            }
            var response = await _httpClient.GetAsync(uri);
            var responseContent = response.Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Token provider failed with status code: " + response.StatusCode +
                                    " and response body: " + responseContent);
            }

            return responseContent;
        }

        public Task<string> GetTokenAsync(string userId)
        {
            throw new NotImplementedException();
        }

        private readonly IHttpClient _httpClient;
        private readonly TokenUriHandler _urlFactory;
    }
}