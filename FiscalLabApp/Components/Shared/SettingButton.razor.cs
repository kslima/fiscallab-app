using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class SettingButton : ComponentBase
{
    [Parameter] public string Icon { get; set; } = string.Empty;

    [Parameter]
    public string Name { get; set; } = string.Empty;

    [Parameter]
    public string Description { get; set; } = string.Empty;
    
    [Parameter]
    public string ButtonColor { get; set; } = "#f8f9fa";
    [Parameter] public EventCallback OnClick { get; set; }
    [Parameter]
    public bool IsLoading { get; set; }
    private string LoadingClass => IsLoading ? "show-loading" : "hide-loading";
    private int TabIndex => IsLoading ? -1 : 0;

    private async Task ExecuteTask()
    {
        await OnClick.InvokeAsync();
    }
}