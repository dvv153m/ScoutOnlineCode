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

//todo
//pwa каждый раз заново загружает приложение из кеша не загружается
//добавить combobox telerik на выбор карт
//подрефачить аутентификацию index.razor удалить model
//попробовать при создании проекта поставить галочку у авторизации
//онлайн данные чоб загружались один раз
//вывод телериковской таблички
//при обновлении страницы редиректит на форму логин
//вывод отчета на js код взять из stimulsoft
//вывод на карте объектов и геозон
//signalr + blazor

//добавление telerik
//1) через нугет скачиваем Telerik.UI.for.Blazor.Trial
//2) в index.html добавляем 
//<link rel="stylesheet" href="_content/Telerik.UI.for.Blazor.Trial/css/kendo-theme-default/all.css" />
//< script src = "_content/Telerik.UI.for.Blazor.Trial/js/telerik-blazor.js" defer ></ script >
//3) в Imports.razor добавить 
//@using Telerik.Blazor
//@using Telerik.Blazor.Components
//5) добавить в MainLayout.razor
//@inherits LayoutComponentBase
//<TelerikRootComponent>
//.....
//</TelerikRootComponent >
//6) Program.cs
//builder.Services.AddTelerikBlazor();
////////////////////////////////////////////////////////////////////
//ScoutOnline.cproj
//<ManifestShortName>FIRE Calculator</ManifestShortName>
//<ManifestLongName > Blazor FIRE Calculator</ManifestLongName>
/////////////////////////////////////////////////////////////////////////