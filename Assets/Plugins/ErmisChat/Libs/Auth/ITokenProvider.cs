using System.Threading.Tasks;

namespace Ermis.Libs.Auth
{
    /// <summary>
    /// Providers JWT authorization token for Ermis Chat
    /// </summary>
    public interface ITokenProvider
    {
        
        Task<string> GetTokenAsync(string apiKey, string tokenHeader);
        Task<string> GetTokenAsync(string userId);
    }
}
    
    //ErmisTodo: we could split this into IAsyncTokenProvider for async/await syntax and IEnumeratorTokenProvider for coroutines? 
