using Microsoft.JSInterop;

namespace FiscalLabApp.Extensions;

public static class JsRuntimeExtensions
{
    public static async Task RemoveFocusFromAllElementsAsync(this IJSRuntime jsRuntime)
    {
        await jsRuntime.InvokeVoidAsync("removeFocusFromAllElements");
    }
}