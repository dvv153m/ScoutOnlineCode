using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScoutOnline.Core.Map;
using ScoutOnline.Core.Auth;
using ScoutOnline.Core.OnlineData;
using Blazored.LocalStorage;
using ScoutOnline.Core.Unit;

namespace ScoutOnline
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services
                .AddSingleton<MapsService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()          
                .AddScoped<OnlineDataService>()
                .AddScoped<UnitService>()
                .AddBlazoredLocalStorage();

            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp =>
            
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
                
            );

            //string serverlessBaseURI = builder.Configuration["BaseURI"];
            await builder.Build().RunAsync();
        }
    }
}
