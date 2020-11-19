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
//< script src = "_content/Telerik.UI.for.Blazor.Trial/js/telerik-blazor.js" defer ></ script >
//3) � Imports.razor �������� 
//@using Telerik.Blazor
//@using Telerik.Blazor.Components
//5) �������� � MainLayout.razor
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