using FiscalLabApp.Extensions;
using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class VisitCard : ComponentBase
{
    [Parameter] public EventCallback<string> OnDeleteButtonClick { get; set; }
    [Parameter] public EventCallback<Visit> OnEditButtonClick { get; set; }
    [Parameter] public EventCallback<string> OnPdfButtonClick { get; set; }
    [Parameter] public EventCallback<string> OnSendToEmailButtonClick { get; set; }
    [Parameter] public EventCallback<Visit> OnImagensButtonClicked { get; set; }
    [Parameter] public Visit Visit { get; set; } = new();
    private int _pendingItemsCount;
    private int _totalItemsCount;
    
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        _pendingItemsCount = Visit
            .CreatePages()
            .Select(x => x.PendingItems)
            .Sum();
        
        _totalItemsCount = Visit
            .CreatePages()
            .Select(x => x.TotalItems)
            .Sum();
    }
    
    private string GetBorderStyle()
    {
        return Visit.NotifiedByEmailAt is null
            ? "border-left: 0.750rem solid #b9b0ab;;border-right: 0;border-top: 0;border-bottom: 0; background-color: #ffffff;"
            : "border-left: 0.750rem solid #467b56;border-right: 0;border-top: 0;border-bottom: 0; background-color: #ffffff;";
    }
    
    private string GetPendingItemsStyle()
    {
        return _pendingItemsCount == _totalItemsCount
            ? "card-body-container-item-text-success"
            : "card-body-container-item-text-warning";
    }
    
    private string GetSyncAtStyle()
    {
        return Visit.SyncedAt.HasValue
            ? "card-body-container-item-text-success"
            : "card-body-container-item-text-neutral";
    }
    
    private string GetNotifiedByEmailAtStyle()
    {
        if (!Visit.NotifyByEmail) return "card-body-container-item-text-neutral";
        
        return Visit.NotifiedByEmailAt.HasValue
            ? "card-body-container-item-text-success"
            : "card-body-container-item-text-warning";
    }

    private string GetBadgeStyle()
    {
        return _pendingItemsCount == 0 ? "background-color: #467b56;" : "background-color: #c74634;";
    }
    
    private async Task ClickHandler()
    {
        await OnEditButtonClick.InvokeAsync(Visit);
    }

    private async Task OnDeleteButtonClickHandler()
    {
        await OnDeleteButtonClick.InvokeAsync(Visit.Id);
    }

    private async Task OnGeneratePdfButtonClickHandler()
    {
        await OnPdfButtonClick.InvokeAsync(Visit.Id);
    }
    
    private async Task OnImagensButtonClickHandler()
    {
        await OnImagensButtonClicked.InvokeAsync(Visit);
    }
    
    private async Task SendToEmailCallback()
    {
        await OnSendToEmailButtonClick.InvokeAsync(Visit.Id);
    }
}