using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Pages.VisitComponents;

public partial class ClarificationSaccharimeterComponent : ComponentBase
{
    [Parameter]
    public Menu[] Menus { get; set; } = [];
    [Parameter]
    public ClarificationSaccharimeter ClarificationSaccharimeter { get; set; } = new();
}