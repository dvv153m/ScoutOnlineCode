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

            builder.Services.AddTelerikBlazor();

            //string serverlessBaseURI = builder.Configuration["BaseURI"];//
            await builder.Build().RunAsync();
        }
    }
}

//todo//

//графики
//https://www.amcharts.com/demos/line-graph/
//https://www.amcharts.com/docs/v4/tutorials/plugin-range-selector/
//https://www.amcharts.com/docs/v4/tutorials/zooming-axis-via-api-or-external-scrollbar/
//https://www.amcharts.com/docs/v4/tutorials/configuring-the-zoom-out-button/
//https://www.amcharts.com/docs/v4/tutorials/3d-line-series/

//https://www.amcharts.com/docs/v4/concepts/performance/
//https://www.amcharts.com/docs/v4/tutorials/stacked-axes/            несколько графиков в одном контроле
//https://www.amcharts.com/docs/v4/concepts/axes/date-axis/#Dynamic_data_item_grouping  подробно про группировку
//https://www.amcharts.com/docs/v4/tutorials/zooming-axis-via-api-or-external-scrollbar/
//https://www.amcharts.com/docs/v4/concepts/chart-cursor/
//https://www.amcharts.com/demos/line-chart-with-range-slider/?theme=dataviz

//табличка с фильтрацией
//https://github.com/SveNord/Sve-Blazor-DataTable

//Обновление приложения
//https://wellsb.com/csharp/aspnet/create-pwa-from-blazor-app/

//pwa with version
//https://wellsb.com/csharp/aspnet/create-pwa-from-blazor-app/

//кластеризация иконок
//Leaflet.markercluster

//создание и редактирование геозон
//https://jsfiddle.net/user2314737/Lscupxqp/

/*var aa = L.marker([48.185556, 11.620278]).bindPopup('AA'),
bb = L.marker([48.152222, 11.592778]).bindPopup('BB'),
cc = L.marker([48.161209, 11.597989]).bindPopup('CC'),
dd = L.marker([48.14350, 11.58775]).bindPopup('DD'),
ee = L.marker([48.14989, 11.59094]).bindPopup('EE'),
ff = L.marker([48.15958, 11.60608]).bindPopup('FF');

var restaurants = L.layerGroup([aa, bb]);
var sport = L.layerGroup([cc, dd]);
var sights = L.layerGroup([ee, ff]);*/


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
//<script src = "_content/Telerik.UI.for.Blazor.Trial/js/telerik-blazor.js" defer ></ script >
//3) в Imports.razor добавить 
//@using Telerik.Blazor
//@using Telerik.Blazor.Components
//5) добавить в MainLayout.razor
//@inherits LayoutComponentBase
//<TelerikRootComponent>
//.....
//</TelerikRootComponent>
//6) Program.cs
//builder.Services.AddTelerikBlazor();
////////////////////////////////////////////////////////////////////
//ScoutOnline.cproj
//<ManifestShortName>FIRE Calculator</ManifestShortName>
//<ManifestLongName > Blazor FIRE Calculator</ManifestLongName>
/////////////////////////////////////////////////////////////////////////