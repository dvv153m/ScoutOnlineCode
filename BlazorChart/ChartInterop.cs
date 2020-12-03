using Microsoft.JSInterop;
using System.Threading.Tasks;
using BlazorChart.Models;

namespace BlazorChart
{
    public class ChartInterop
    {        
        public static ValueTask Init(IJSRuntime jsRuntime, ChartModel[] chartModels)
        {            
            var chartModelsJson = System.Text.Json.JsonSerializer.Serialize(chartModels);
            return jsRuntime.InvokeVoidAsync("chartBlazor.init", chartModelsJson);
        }
    }
}
