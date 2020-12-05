using Microsoft.JSInterop;
using System.Threading.Tasks;
using BlazorChart.Models;

namespace BlazorChart
{
    public class ChartInterop
    {
        //public static ValueTask Init(IJSRuntime jsRuntime, ChartModel[] chartModels)
        public static ValueTask Init(IJSRuntime jsRuntime, ChartControl chartControl)
        {            
            var chartModelsJson = System.Text.Json.JsonSerializer.Serialize(chartControl.ChartModels);
            return jsRuntime.InvokeVoidAsync("chartBlazor.init", chartControl.CountPoints, chartModelsJson);
        }
    }
}
