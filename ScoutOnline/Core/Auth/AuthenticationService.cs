using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Auth
{
    public class AuthenticationService : IAuthenticationService
    {        
        private readonly string BaseUrl;

        private string _accessToken;
        private string _refreshToken;

        public TokenResponse TokenResponse { get; private set; }

        private ILocalStorageService _localStorageService;

        public AuthenticationService(ILocalStorageService localStorageService,
                                     IConfiguration config)
        {
            _localStorageService = localStorageService;
            BaseUrl = config["BaseURI"];
        }

        public async Task<bool> Login(string username, string password)
        {
            username = "dvv";
            password = "Hulycar8266";
            string requestData = $"grant_type=password&username={username}&password={password}&locale=ru&client_id=8b1fd704-096e-42d6-9ba5-6d98980e7cd1&client_secret=scout-online";

            return await RequestAuth(requestData);            
        }        

        public async Task<bool> RefreshTokensAsync()
        {
            string requestData = $"grant_type=refresh_token&refresh_token={_refreshToken}&client_id=scout-online&client_secret=scout-online";
            return await RequestAuth(requestData);            
        }

        private async Task<bool> RequestAuth(string requestData)
        {
            string loginUrl = $"{BaseUrl}/api/auth/token";
            TokenResponse = await Post<TokenResponse>(loginUrl, requestData);
            if (TokenResponse != default(TokenResponse))
            {
                _accessToken = TokenResponse.AccessToken;
                _refreshToken = TokenResponse.RefreshToken;
                await _localStorageService.SetItemAsync<TokenResponse>("tokenResponse", TokenResponse);
                return true;
            }
            return false;
        }

        private async Task<T> Post<T>(string requestUrl, string requestData, bool refreshToken = true)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            requestMessage.Content = new StringContent(requestData, Encoding.UTF8, "application/x-www-form-urlencoded");

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseFromServer = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var unitsOnlineData = JsonConvert.DeserializeObject<T>(responseFromServer);
                    return unitsOnlineData;
                }

            }
            return default(T);
        }
    }
}
