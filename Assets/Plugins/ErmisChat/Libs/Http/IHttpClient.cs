using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ermis.Libs.Http
{
    /// <summary>
    /// Http client
    /// </summary>
    public interface IHttpClient
    {
        void SetDefaultAuthenticationHeader(string value);

        void SetDefaultAuthenticationHeader(string scheme,string param);

        void AddDefaultCustomHeader(string key, string value);

        void AddDefaultCustomHeaderAccept(string value);

        Task<HttpResponse> GetAsync(Uri uri);

        Task<HttpResponse> PostAsync(Uri uri, object content);

        Task<HttpResponse> PutAsync(Uri uri, object content);

        Task<HttpResponse> PatchAsync(Uri uri, object content);

        Task<HttpResponse> DeleteAsync(Uri uri);

        Task<HttpResponse> SendHttpRequestAsync(HttpMethodType methodType, Uri uri, object optionalRequestContent);

        Task<HttpResponse> SendHttpRequestAsyncCustomHeader(HttpMethodType methodType, Uri uri, object optionalRequestContent);
    }
}