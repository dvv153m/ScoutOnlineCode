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

//�������
//https://www.amcharts.com/demos/line-graph/
//https://www.amcharts.com/docs/v4/tutorials/plugin-range-selector/
//https://www.amcharts.com/docs/v4/tutorials/zooming-axis-via-api-or-external-scrollbar/
//https://www.amcharts.com/docs/v4/tutorials/configuring-the-zoom-out-button/
//https://www.amcharts.com/docs/v4/tutorials/3d-line-series/

//https://www.amcharts.com/docs/v4/concepts/performance/
//https://www.amcharts.com/docs/v4/tutorials/stacked-axes/            ��������� �������� � ����� ��������
//https://www.amcharts.com/docs/v4/concepts/axes/date-axis/#Dynamic_data_item_grouping  �������� ��� �����������
//https://www.amcharts.com/docs/v4/tutorials/zooming-axis-via-api-or-external-scrollbar/
//https://www.amcharts.com/docs/v4/concepts/chart-cursor/
//https://www.amcharts.com/demos/line-chart-with-range-slider/?theme=dataviz

//�������� � �����������
//https://github.com/SveNord/Sve-Blazor-DataTable

//���������� ����������
//https://wellsb.com/csharp/aspnet/create-pwa-from-blazor-app/

//pwa with version
//https://wellsb.com/csharp/aspnet/create-pwa-from-blazor-app/

//������������� ������
//Leaflet.markercluster

//�������� � �������������� ������
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


//pwa ������ ��� ������ ��������� ���������� �� ���� �� �����������
//�������� combobox telerik �� ����� ����
//����������� �������������� index.razor ������� model
//����������� ��� �������� ������� ��������� ������� � �����������
//������ ������ ��� ����������� ���� ���
//����� ������������� ��������
//��� ���������� �������� ���������� �� ����� �����
//����� ������ �� js ��� ����� �� stimulsoft
//����� �� ����� �������� � ������
//signalr + blazor

//���������� telerik
//1) ����� ����� ��������� Telerik.UI.for.Blazor.Trial
//2) � index.html ��������� 
//<link rel="stylesheet" href="_content/Telerik.UI.for.Blazor.Trial/css/kendo-theme-default/all.css" />
//<script src = "_content/Telerik.UI.for.Blazor.Trial/js/telerik-blazor.js" defer ></ script >
//3) � Imports.razor �������� 
//@using Telerik.Blazor
//@using Telerik.Blazor.Components
//5) �������� � MainLayout.razor
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