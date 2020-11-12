using Blazored.LocalStorage;
using Scout.Utils.Paging;
using ScoutOnline.Core.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoutOnline.Core.OnlineData
{
    public interface IOnlineDataService
    {
        Task<IEnumerable<OnlineDataResponse>> GetOnlineData();
    }

    public class OnlineDataService : ServiceBase, IOnlineDataService
    {
        //todo из конфига получать
        private static string baseUrl = "https://api.scout-gps.ru";        
        
        public OnlineDataService(IAuthenticationService authenticationService,
                                 ILocalStorageService localStorageService) : base(authenticationService, localStorageService)
        {            
        }

        public async Task<IEnumerable<OnlineDataResponse>> GetOnlineData()
        {            
            string onlineDataUrl = $"{baseUrl}/api/onlinedata/getOnlineData";
            string requestData = @"{""skip"": 0, ""take"": 10, ""filter"": 1, ""orderDescending"": false}";
            var unitsOnlineData = await Post<Page<OnlineDataResponse>>(onlineDataUrl, requestData);
            return unitsOnlineData.Data;
        }

        
    }
}
