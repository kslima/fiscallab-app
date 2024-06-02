using FiscalLabApp.Enums;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Pages.Visit;

public partial class DoneVisits : ComponentBase
{
    [Inject] private IVisitService VisitService { get; set; } = null!;
    private int _currentPage = 1;
    private const int PageSize = 10;
    private bool _hasNextPage = true;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnInitializedAsync();
        if (firstRender)
        {
            await LoadingNextPageAsync();
        }
    }
    
    protected readonly List<VisitViewModel> Visits = [];

    private async Task LoadingNextPageAsync()
    {
        if (!_hasNextPage) return;
        
        StateHasChanged();
        
        var parameters = new VisitParameters
        {
            Status = VisitStatus.Done,
            PageIndex = _currentPage,
            PageSize = PageSize
        };
        
        var response = await VisitService.ListAsync(parameters);
        _hasNextPage = response.Metadata?.HasNextPage == true;
        _currentPage = _hasNextPage ? _currentPage + 1 : _currentPage;
        
        var visits =  response.Data!.Adapt<List<VisitViewModel>>();
        Visits.AddRange(visits);
        StateHasChanged();
    }
}