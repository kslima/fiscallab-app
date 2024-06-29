using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class VisitCardButton : ComponentBase
{
    [Parameter] public RenderFragment ChildContent { get; set; } = null!;
    [Parameter] public string CssClass { get; set; } = string.Empty;
    [Parameter]
    public bool IsDisabled { get; set; }
    [Parameter] public EventCallback<string> OnClick { get; set; }
    
    private async Task OnClickHandler()
    {
        await OnClick.InvokeAsync();
    }
}