using Blazored.LocalStorage;
using Newtonsoft.Json;
using ScoutOnline.Core.Auth;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Unit
{
    public interface IUnitService
    {
        Task<ScopeModel[]> GetScopes();
    }

    public class UnitService : IUnitService
    {
        //todo из конфига получать
        private static string baseUrl = "https://api.scout-gps.ru";
        private ILocalStorageService _localStorageService;
        private IAuthenticationService _authenticationService;

        public UnitService(IAuthenticationService authenticationService,
                           ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _authenticationService = authenticationService;
        }

        public async Task<ScopeModel[]> GetScopes()
        {
            string scopesUrl = $"{baseUrl}/api/units/getScopes";
            var scopes = await Get<ScopeModel[]>(scopesUrl, string.Empty);
            return scopes;
        }

        private async Task<T> Get<T>(string requestUrl, string requestData, bool refreshToken = true)
        {
            var tokenResponse = await _localStorageService.GetItemAsync<TokenResponse>("tokenResponse");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);
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
                    var unitsOnlineData = JsonConvert.DeserializeObject<T>(responseFromServer);
                    return unitsOnlineData;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized && refreshToken)
                {
                    await _authenticationService.RefreshTokensAsync();
                    return await Get<T>(requestUrl, requestData, false);
                }
            }
            return default(T);
        }
    }
}
