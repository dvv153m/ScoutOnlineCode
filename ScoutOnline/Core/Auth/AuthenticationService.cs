using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        //todo из конфига получать
        private static string baseUrl = "https://api.scout-gps.ru";

        public async Task Login(string username, string password)
        {
            username = "dvv";
            password = "Hulycar8266";

            string loginUrl = baseUrl + "/api/auth/token";            
            string postData = "grant_type=password&username=demo&password=demo&locale=ru&client_id=8b1fd704-096e-42d6-9ba5-6d98980e7cd1&client_secret=scout-online";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loginUrl);
            request.Method = "POST";
            request.Accept = "json";            
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            try
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            string responseFromServer = reader.ReadToEnd();

                            TokenResponse parseResponse = JsonConvert.DeserializeObject<TokenResponse>(responseFromServer);
                            var token = parseResponse.AccessToken;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }            
        }
    }
}
