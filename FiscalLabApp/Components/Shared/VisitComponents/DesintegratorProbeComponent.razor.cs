using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class DesintegratorProbeComponent : ComponentBase
{
    [Parameter]
    public DesintegratorProbe DesintegratorProbe { get; set; } = new();

    [Parameter]
    public Menu[] Menus { get; set; } = [];
    [Parameter] public EventCallback<Menu> OnEditOptionsButtonClick { get; set; }
    
    private async Task OnEditOptionsButtonClickHandler(Menu menu)
    {
        await OnEditOptionsButtonClick.InvokeAsync(menu);
    }
}