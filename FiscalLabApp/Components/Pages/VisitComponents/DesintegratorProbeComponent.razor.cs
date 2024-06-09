using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Pages.VisitComponents;

public partial class DesintegratorProbeComponent : ComponentBase
{
    [Parameter]
    public DesintegratorProbe DesintegratorProbe { get; set; } = new();

    [Parameter]
    public Menu[] Menus { get; set; } = [];
}