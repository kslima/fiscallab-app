using FiscalLabApp.Enums;
using FiscalLabApp.Features.Visits;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Layout;

public partial class EditVisitLayout : LayoutComponentBase
{
    [Inject]
    private IVisitService VisitService { get; set; } = null!;
    [Inject]
    private SelectedVisitEventNotifier SelectedVisitEventNotifier{ get; set; } = null!;

    private bool _isReadyOnly;
    
    public VisitViewModel? SelectedVisit { get; set; }
    private VisitPage[] _pages = [];
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SelectedVisitEventNotifier.SelectedVisit += HandlerVisitPages;
    }
    
    public Task UpdateVisitAsync()
    {
        var visit = SelectedVisit.Adapt<Visit>();
        return VisitService.UpdateAsync(visit);
    }
    
    public async Task<VisitViewModel> SyncVisit(string visitId)
    {
        if (SelectedVisit is not null)
        {
            return SelectedVisit;
        }
        
        var visit = await VisitService.GetByIdLocalAsync(visitId);
        SelectedVisit = visit.Adapt<VisitViewModel>();
        
        HandlerVisitPages();

        return SelectedVisit;
    }
    
    private void HandlerVisitPages()
    {
        if (SelectedVisit is null) return;
        var metadata = SelectedVisit.GetMetadata();
        _pages = metadata.Pages.OrderBy(v => v.Id).ToArray();

        _isReadyOnly = SelectedVisit.Status == VisitStatus.Done;
        StateHasChanged();
    }
    
    private string GetDisabledCssClass()
    {
        return _isReadyOnly ? "disabled-container" : string.Empty;
    }
}