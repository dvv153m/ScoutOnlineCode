using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorChart
{
    public class ChartInterop
    {        
        public static ValueTask Init(IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeVoidAsync("chartBlazor.init");
        }
    }
}
