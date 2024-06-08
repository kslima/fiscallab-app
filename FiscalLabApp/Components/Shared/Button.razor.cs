using FiscalLabApp.Enums;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class Button : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = "Button";
    [Parameter]
    public ButtonStyle Style { get; set; } = ButtonStyle.Default;
    [Parameter] public EventCallback<string> OnClicked { get; set; }
    private async Task ClickCallback()
    {
        await OnClicked.InvokeAsync();
    }
    
}