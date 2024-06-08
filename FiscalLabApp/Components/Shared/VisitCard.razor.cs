using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class VisitCard : ComponentBase
{
    [Parameter] public EventCallback<string> OnDeleteButtonClicked { get; set; }
    [Parameter] public EventCallback<string> OnEditButtonClicked { get; set; }
    [Parameter] public EventCallback<string> OnPdfButtonClicked { get; set; }
    [Parameter] public EventCallback<string> OnSendToEmailButtonClicked { get; set; }
    [Parameter] public VisitViewModel Visit { get; set; } = new();
    private int _pendingItemsCount;
    
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        _pendingItemsCount = Visit.GetMetadata().PendingItems;
    }

    private string GetBorderStyle()
    {
        return Visit.NotifiedByEmailAt is null
            ? "border-left: 0.750rem solid #b9b0ab;;border-right: 0;border-top: 0;border-bottom: 0; background-color: #ffffff;"
            : "border-left: 0.750rem solid #467b56;border-right: 0;border-top: 0;border-bottom: 0; background-color: #ffffff;";
    }

    private string GetBadgeStyle()
    {
        return _pendingItemsCount == 0 ? "background-color: #467b56;" : "background-color: #c74634;";
    }
    
    private async Task EditCallback()
    {
        await OnEditButtonClicked.InvokeAsync(Visit.Id);
    }

    private async Task DeleteCallback()
    {
        await OnDeleteButtonClicked.InvokeAsync(Visit.Id);
    }

    private async Task GeneratePdfCallback()
    {
        await OnPdfButtonClicked.InvokeAsync(Visit.Id);
    }
    
    private async Task SendToEmailCallback()
    {
        await OnSendToEmailButtonClicked.InvokeAsync(Visit.Id);
    }
}