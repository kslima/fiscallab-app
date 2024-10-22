using FiscalLabApp.Enums;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class LoadingButton : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = "Button";
    [Parameter]
    public string CssClass { get; set; } = string.Empty;
    [Parameter]
    public bool IsDisabled { get; set; }
    [Parameter]
    public ButtonStyle StyleType { get; set; } = ButtonStyle.Default;
    [Parameter]
    public bool IsLoading { get; set; }
    [Parameter] public EventCallback<string> OnClick { get; set; }
    private async Task OnClickHandler()
    {
        await OnClick.InvokeAsync();
    }
    
}