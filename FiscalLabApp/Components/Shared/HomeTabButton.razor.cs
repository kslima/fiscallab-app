using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class HomeTabButton : ComponentBase
{
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public bool IsSelected { get; set; }
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter]
    public EventCallback OnTabSelected { get; set; }
     
    private async Task OnTabSelectedAsync()
    {
        await OnTabSelected.InvokeAsync();
    }

    private string GetId()
    {
        return Title.GetHashCode().ToString();
    }
}