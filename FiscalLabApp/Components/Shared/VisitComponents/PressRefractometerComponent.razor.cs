using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class PressRefractometerComponent : ComponentBase
{
    [Parameter]
    public Menu[] Menus { get; set; } = [];
    [Parameter]
    public PressRefractometer PressRefractometer { get; set; } = new();
}