using Blazored.LocalStorage;
using Newtonsoft.Json;
using Scout.Utils.Paging;
using ScoutOnline.Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //private string _token = string.Empty;
        private ILocalStorageService _localStorageService;

        public OnlineDataService(IAuthenticationService authenticationService,
                                 ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            //_token = authenticationService.TokenResponse?.AccessToken;
        }


        public async Task<IEnumerable<OnlineDataResponse>> GetOnlineData()
        {
            var tokenResponse = await _localStorageService.GetItemAsync<TokenResponse>("tokenResponse");
            string positionUrl = $"{baseUrl}/api/onlinedata/getOnlineData";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, positionUrl);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            string postData = @"{""skip"": 0, ""take"": 10, ""filter"": 1, ""orderDescending"": false}";

            requestMessage.Content = new StringContent(postData, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseFromServer = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var unitsOnlineData = JsonConvert.DeserializeObject<Page<OnlineDataResponse>>(responseFromServer);
                    return unitsOnlineData.Data;
                    /*foreach (var data in p1.Data)
                    {
                        var vvv = data.OnlineData;
                    }*/
                    /*var data = parseResponse["data"][0];
                    var unit = data["unit"];
                    var onlineData = data["onlineData"];

                    var unitId = unit["id"];
                    var address = onlineData["address"];*/
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
