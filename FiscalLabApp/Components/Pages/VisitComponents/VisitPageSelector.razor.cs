using FiscalLabApp.Features.Visits;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Pages.VisitComponents;

public partial class VisitPageSelector : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public IVisitService VisitService { get; set; } = null!;
    [Inject]
    public SelectedPageEventNotifier SelectedPageEventNotifier { get; set; } = null!;
    [Parameter]
    public EventCallback<string> OnPageChange { get; set; }

    [Parameter]
    public VisitPage[] Pages { get; set; } = [];

    private VisitViewModel _selectedVisit = new();
    [Parameter] 
    public string InitialPage { get; set; } = string.Empty;
    
    private string _selectedPageName = string.Empty;

    [Parameter] public EventCallback<decimal> ValueUpdated { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SelectedPageEventNotifier.SelectedPageEvent += SelectedPageHandler;
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        _selectedPageName = InitialPage;
    }

    private void SelectedPageHandler(string currentPage)
    {
        _selectedPageName = currentPage;
        StateHasChanged();
    }
    
    private async Task OnPageChangeHandler()
    {
        await OnPageChange.InvokeAsync(_selectedPageName);
    }
}