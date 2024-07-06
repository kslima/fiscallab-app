using FiscalLabApp.Extensions;
using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class VisitCard : ComponentBase
{
    [Parameter] public EventCallback<Visit> OnDelete { get; set; }
    [Parameter] public EventCallback<Visit> OnOpenEditVisitPage { get; set; }
    [Parameter] public EventCallback<string> OnGeneratePdf { get; set; }
    [Parameter] public EventCallback<string> OnSendByEmail { get; set; }
    [Parameter] public EventCallback<Visit> OnOpenAddImageModal { get; set; }
    [Parameter] public bool AddImageButtonDisabled { get; set; }
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
            ? "badge-success"
            : "badge-warning";
    }
    
    private string GetSyncAtStyle()
    {
        return Visit.SyncedAt.HasValue
            ? "badge-success"
            : "badge-neutral";
    }
    
    private string GetNotifiedByEmailAtStyle()
    {
        if (!Visit.NotifyByEmail) return "badge-neutral";
        
        return Visit.NotifiedByEmailAt.HasValue
            ? "badge-success"
            : "badge-warning";
    }

    private string GetBadgeStyle()
    {
        return _pendingItemsCount == 0 ? "background-color: #467b56;" : "background-color: #c74634;";
    }
    
    private async Task OnOpenEditVisitPageHandlerAsync()
    {
        await OnOpenEditVisitPage.InvokeAsync(Visit);
    }

    private async Task OnDeleteHandlerAsync()
    {
        await OnDelete.InvokeAsync(Visit);
    }

    private async Task OnGeneratePdfHandlerAsync()
    {
        await OnGeneratePdf.InvokeAsync(Visit.Id);
    }
    
    private async Task OnOpenAddImageModalHandlerAsync()
    {
        await OnOpenAddImageModal.InvokeAsync(Visit);
    }
    
    private async Task OnSendByEmailHandlerAsync()
    {
        await OnSendByEmail.InvokeAsync(Visit.Id);
    }
}