using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class DataSeparator : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = string.Empty;
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}