using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class PressRefractometerComponent : ComponentBase
{
    [Parameter]
    public Menu[] Menus { get; set; } = [];
    [Parameter]
    public PressRefractometer PressRefractometer { get; set; } = new();
    [Parameter] public EventCallback<Menu> OnEditOptionsButtonClick { get; set; }
    
    private async Task OnEditOptionsButtonClickHandler(Menu menu)
    {
        await OnEditOptionsButtonClick.InvokeAsync(menu);
    }
}