using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class ConclusionComponent : ComponentBase
{
    [Parameter]
    public Conclusion Conclusion { get; set; } = new();
    
    [Parameter]
    public Menu[] Menus { get; set; } = [];
    
    private ModalDialog _modalDialog = null!;
    [Parameter] public EventCallback<Menu> OnEditOptionsButtonClick { get; set; }
    [Parameter] public EventCallback OnOpenImagesButtonClick { get; set; }
    
    private async Task OnOpenImagesButtonClickHandler()
    {
        await OnOpenImagesButtonClick.InvokeAsync();
    }
    
    private async Task OnEditOptionsButtonClickHandler(Menu menu)
    {
        await OnEditOptionsButtonClick.InvokeAsync(menu);
    }
}