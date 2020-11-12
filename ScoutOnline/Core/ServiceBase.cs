using Blazored.LocalStorage;
using Newtonsoft.Json;
using ScoutOnline.Core.Auth;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ScoutOnline.Core
{
    public class ServiceBase
    {
        private ILocalStorageService _localStorageService;
        private IAuthenticationService _authenticationService;

        public ServiceBase(IAuthenticationService authenticationService,
                           ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _authenticationService = authenticationService;
        }

        protected async Task<T> Post<T>(string requestUrl, string requestData, bool refreshToken = true)
        {
            return await Request<T>(HttpMethod.Post, requestUrl, requestData);            
        }

        protected async Task<T> Get<T>(string requestUrl, string requestData, bool refreshToken = true)
        {
            return await Request<T>(HttpMethod.Get, requestUrl, requestData);
        }

        private async Task<T> Request<T>(HttpMethod httpMethod, string requestUrl, string requestData, bool refreshToken = true)
        {
            var tokenResponse = await _localStorageService.GetItemAsync<TokenResponse>("tokenResponse");
            var requestMessage = new HttpRequestMessage(httpMethod, requestUrl);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            if (requestData.Length > 0)
            {
                requestMessage.Content = new StringContent(requestData, Encoding.UTF8, "application/json");
            }

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseFromServer = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(responseFromServer);                    
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized && refreshToken)
                {
                    await _authenticationService.RefreshTokensAsync();
                    return await Request<T>(httpMethod, requestUrl, requestData, false);
                }
            }
            return default(T);
        }
    }
}
