using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class DisableContainer : ComponentBase
{
    [Parameter]
    public bool IsDisabled { get; set; }
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;
}