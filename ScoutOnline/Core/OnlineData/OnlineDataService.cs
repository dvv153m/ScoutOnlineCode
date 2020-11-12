using Blazored.LocalStorage;
using Newtonsoft.Json;
using Scout.Utils.Paging;
using ScoutOnline.Core.Auth;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ScoutOnline.Core.OnlineData
{
    public interface IOnlineDataService
    {
        Task<IEnumerable<OnlineDataResponse>> GetOnlineData();
    }

    public class OnlineDataService : IOnlineDataService
    {
        //todo из конфига получать
        private static string baseUrl = "https://api.scout-gps.ru";        
        private ILocalStorageService _localStorageService;
        private IAuthenticationService _authenticationService;

        public OnlineDataService(IAuthenticationService authenticationService,
                                 ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _authenticationService = authenticationService;            
        }

        public async Task<IEnumerable<OnlineDataResponse>> GetOnlineData()
        {            
            string onlineDataUrl = $"{baseUrl}/api/onlinedata/getOnlineData";
            string requestData = @"{""skip"": 0, ""take"": 10, ""filter"": 1, ""orderDescending"": false}";
            var unitsOnlineData = await Get<Page<OnlineDataResponse>>(onlineDataUrl, requestData);
            return unitsOnlineData.Data;
        }

        private async Task<T> Get<T>(string requestUrl, string requestData, bool refreshToken = true)
        {
            var tokenResponse = await _localStorageService.GetItemAsync<TokenResponse>("tokenResponse");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            requestMessage.Content = new StringContent(requestData, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseFromServer = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var unitsOnlineData = JsonConvert.DeserializeObject<T>(responseFromServer);
                    return unitsOnlineData;
                }
                else if(response.StatusCode == HttpStatusCode.Unauthorized && refreshToken)
                {
                    await _authenticationService.RefreshTokensAsync();
                    return await Get<T>(requestUrl, requestData, false);
                }
            }
            return default(T);
        }
    }
}
