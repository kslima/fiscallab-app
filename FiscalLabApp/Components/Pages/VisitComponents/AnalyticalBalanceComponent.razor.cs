using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Pages.VisitComponents;

public partial class AnalyticalBalanceComponent : ComponentBase
{
    [Parameter]
    public Menu[] Menus { get; set; } = [];
    
    [Parameter]
    public AnalyticalBalance AnalyticalBalance { get; set; } = new();
}