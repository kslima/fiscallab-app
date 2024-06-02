using FiscalLabApp.Enums;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Pages.Visit;

public partial class Home : ComponentBase
{
    [Inject]
    private IVisitService VisitService { get; set; } = null!;
    [Inject]
    private SyncEventNotifier SyncEventNotifier { get; set; } = null!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    private HomeTabType HomeTabType { get; set; } = HomeTabType.InProgress;
    
    private void NewVisit()
    {
        NavigationManager.NavigateTo("/new-visit");
    }

    private void OnHomeTabButtonClicked(HomeTabType homeTabType)
    {
        HomeTabType = homeTabType;
        StateHasChanged();
    }
}