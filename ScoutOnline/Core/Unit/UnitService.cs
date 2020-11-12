using Blazored.LocalStorage;
using ScoutOnline.Core.Auth;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Unit
{
    public interface IUnitService
    {
        Task<ScopeModel[]> GetScopes();
    }

    public class UnitService : ServiceBase, IUnitService
    {
        //todo из конфига получать
        private static string baseUrl = "https://api.scout-gps.ru";        

        public UnitService(IAuthenticationService authenticationService,
                           ILocalStorageService localStorageService) : base(authenticationService, localStorageService)
        {            
        }

        public async Task<ScopeModel[]> GetScopes()
        {
            string scopesUrl = $"{baseUrl}/api/units/getScopes";
            var scopes = await Get<ScopeModel[]>(scopesUrl, string.Empty);
            return scopes;
        }
    }
}
