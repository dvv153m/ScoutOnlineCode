using Blazored.LocalStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        //todo из конфига получать
        private static string baseUrl = "https://api.scout-gps.ru";

        private string _accessToken;
        private string _refreshToken;

        public TokenResponse TokenResponse { get; private set; }

        private ILocalStorageService _localStorageService;

        public AuthenticationService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {             
                username = "dvv";
                password = "Hulycar8266";
                TokenResponse = null;

                string loginUrl = baseUrl + "/api/auth/token";
                string postData = $"grant_type=password&username={username}&password={password}&locale=ru&client_id=8b1fd704-096e-42d6-9ba5-6d98980e7cd1&client_secret=scout-online";

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, loginUrl);
                requestMessage.Content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");

                using (var client = new HttpClient())
                {
                    var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseFromServer = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        TokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseFromServer);
                        _accessToken = TokenResponse.AccessToken;
                        _refreshToken = TokenResponse.RefreshToken;
                        await _localStorageService.SetItemAsync<TokenResponse>("tokenResponse", TokenResponse);
                        return true;
                    }                    
                }
            }
            catch (Exception ex)
            { 
                
            }
            return false;
        }

        public async Task<string> GetTokenAsync(string username, string password, bool refresh)
        {            
            if (_accessToken != null)
            {
                // token out of date 
                if (!refresh) return _accessToken;

                // gets the new token
                if (await RefreshTokensAsync())
                {
                    // if refreshed - return token
                    return _accessToken;
                }
            }

            if (await Login(username, password) == false)
            {
                return null;
            }

            return _accessToken;
        }

        private async Task<bool> RefreshTokensAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string loginUrl = baseUrl + "/api/auth/token";
                    string postData = $"grant_type=refresh_token&refresh_token={_refreshToken}&client_id=scout-online&client_secret=scout-online";

                    var requestMessage = new HttpRequestMessage(HttpMethod.Post, loginUrl);
                    requestMessage.Content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");

                    var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseFromServer = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        TokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseFromServer);
                        _accessToken = TokenResponse.AccessToken;
                        _refreshToken = TokenResponse.RefreshToken;
                        await _localStorageService.SetItemAsync<TokenResponse>("tokenResponse", TokenResponse);
                    }
                    else
                    {
                        //HttpStatusCode.BadRequest                        
                        //не верный логин и пас
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
