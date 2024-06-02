using FiscalLabApp.Enums;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FiscalLabApp.Components.Pages.Visit;

public partial class InProgressVisits : ComponentBase
{
    [Parameter]
    public VisitViewModel[] Visits { get; set; } = [];

    [Inject]
    private IVisitService VisitService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject]
    private SyncEventNotifier SyncEventNotifier { get; set; } = null!;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            SyncEventNotifier.SyncEvent += async (sender, args) => { await SyncEventHandler(sender, args); };
            await GetInProgressVisits();
        }
    }

    private async Task SyncEventHandler(object? sender, EventArgs e)
    {
        await GetInProgressVisits();
        StateHasChanged();
    }
    
    private async Task OnVisitDeleted()
    {
        await GetInProgressVisits();
    }
    
    private async Task GetInProgressVisits()
    {
        var visits = await VisitService.GetAllAsync();
        Visits = visits
            .Where(x => x.Status == VisitStatus.InProgress)
            .OrderByDescending(x => x.CreatedAt).Adapt<VisitViewModel[]>();
        StateHasChanged();
    }
    
    [JSInvokable]
    public Task SyncCompleted(string message)
    {
        StateHasChanged();
        return Task.CompletedTask;
    }
}