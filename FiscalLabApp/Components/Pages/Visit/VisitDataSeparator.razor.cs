using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Pages.Visit;

public partial class VisitDataSeparator : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = string.Empty;
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}