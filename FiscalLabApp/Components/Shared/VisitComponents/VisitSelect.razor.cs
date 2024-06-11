using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class VisitSelect : ComponentBase
{
    [Parameter]
    public EventCallback<string> OnItemSelected { get; set; }
    [Parameter]
    public EventCallback OnAddButtonClick { get; set; }
    [Parameter]
    public EventCallback OnEditButtonClick { get; set; }
    [Parameter]
    public bool IsDisabled { get; set; }
    [Parameter]
    public string Label { get; set; } = string.Empty;
    [Parameter]
    public string Title { get; set; } = string.Empty;
    [Parameter]
    public string SelectedId { get; set; }= string.Empty;
    [Parameter]
    public IReadOnlyCollection<SelectModel> Items { get; set; } = [];
    
    private async Task OnItemChangeHandler()
    {
        await OnItemSelected.InvokeAsync(SelectedId);
    }
    
    private async Task OnAddButtonClickHandler()
    {
        await OnAddButtonClick.InvokeAsync();
    }
    
    private async Task OnEditButtonClickHandler()
    {
        await OnEditButtonClick.InvokeAsync();
    }

    public record SelectModel(string Id, string DisplayName);
}