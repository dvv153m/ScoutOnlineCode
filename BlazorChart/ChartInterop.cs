using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorChart
{
    public class ChartInterop
    {        
        public static ValueTask Init(IJSRuntime jsRuntime, string id)
        {
            return jsRuntime.InvokeVoidAsync("chartBlazor.init", id);
        }
    }
}
