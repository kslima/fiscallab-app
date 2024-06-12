using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class SugarcaneBalanceComponent : ComponentBase
{
    [Parameter]
    public SugarcaneBalance SugarcaneBalance { get; set; } = new();

    [Parameter]
    public Menu[] Menus { get; set; } = [];
    
    [Parameter]
    public EventCallback OnBalanceTestsClick { get; set; }
    [Parameter] public EventCallback<Menu> OnEditOptionsButtonClick { get; set; }
    
    private async Task OnBalanceTestsClickHandler()
    {
        await OnBalanceTestsClick.InvokeAsync();
    }
    
    private async Task OnEditOptionsButtonClickHandler(Menu menu)
    {
        await OnEditOptionsButtonClick.InvokeAsync(menu);
    }
}