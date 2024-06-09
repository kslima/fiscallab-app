using Blazored.Toast.Services;
using FiscalLabApp.Enums;
using FiscalLabApp.Features.Visits;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FiscalLabApp.Components.Pages.Visit;

public partial class Home : ComponentBase
{
    private const string OfflineVisitsTabName = "offline-visits";
    private const string OnlineVisitsTabName = "online-visits";

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject]
    private ApplicationContextAccessor ApplicationContextAccessor { get; set; } = null!;
    [Inject]
    private IToastService ToastService { get; set; } = null!;
    [Inject]
    private IVisitService VisitService { get; set; } = null!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    private IApiService ApiService { get; set; } = null!;
    [Inject] 
    private NetworkStatusEventNotifier NetworkStatusEventNotifier { get; set; } = null!;

    [Parameter]
    public string ActiveTab { get; set; } = OfflineVisitsTabName;
    
    private List<VisitViewModel> _visits = [];
    
    private int _currentPage = 1;
    private const int PageSize = 10;
    private bool _hasNextPage = true;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        NetworkStatusEventNotifier.NetworkStatusChangedEvent += OnNetworkStatusChanged;
        
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        if (!string.IsNullOrEmpty(uri.Fragment))
        {
            ActiveTab = uri.Fragment.TrimStart('#');
        }

        await LoadingSelectedTabContent();
    }
    
    private void OnNetworkStatusChanged(object? sender, NetworkStatus status)
    {
        StateHasChanged();
    }
    
    private async Task SetSelectedTab(string tab)
    {
        ActiveTab = tab;
        var newUri = new Uri(NavigationManager.Uri).GetLeftPart(UriPartial.Path) + "#" + tab;
        NavigationManager.NavigateTo(newUri);
        
        await LoadingSelectedTabContent();
    }

    private bool IsSelectedTab(string tabName)
    {
        return ActiveTab.Equals(tabName);
    }

    private async Task LoadingSelectedTabContent()
    {
        _visits = [];
        ResetPaginationSettings();
        if (ActiveTab.Equals(OnlineVisitsTabName))
        {
            await LoadingOnlineVisitsAsync();
            return;
        }
        
        await LoadingOfflineVisitsAsync();
    }

    private void ResetPaginationSettings()
    {
        _currentPage = 1;
        _hasNextPage = true;
    }
    
    private void NewVisit()
    {
        NavigationManager.NavigateTo("/new-visit");
    }
    
    private async Task LoadingOfflineVisitsAsync()
    {
        var visits = await VisitService.GetAllLocalAsync();
        _visits = visits.Adapt<List<VisitViewModel>>();
    }
    
    private async Task LoadingOnlineVisitsAsync()
    {
        if (!_hasNextPage) return;
        
        await ListVisitsAsync();
        StateHasChanged();
    }
    
    private async Task ListVisitsAsync()
    {
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
        _visits.AddRange(visits);
    }
    
    public async Task DeleteVisitCallback(string visitId)
    {
        if (ApplicationContextAccessor.IsOffline())
        {
            ToastService.ShowError(MessageHelper.NoInternetConnection);
            return;
        }
        
        var confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Remover visita ?");
        if (!confirmed) return;
        
        await VisitService.DeleteAsync(visitId);
        await JsRuntime.InvokeVoidAsync("removeFocusFromAllElements");
        
        await LoadingOfflineVisitsAsync();
        StateHasChanged();
    }
    
    public void EditVisitCallback(string visitId)
    {
        NavigationManager.NavigateTo($"/visits/{visitId}/main");
    }
    
    private async Task PdfVisitCallback(string visitId)
    {
        if (ApplicationContextAccessor.IsOffline())
        {
            ToastService.ShowError(MessageHelper.NoInternetConnection);
            return;
        }
    
        var visit = await VisitService.GetAsync(visitId);
        if (visit.SyncedAt is null)
        {
            ToastService.ShowError(MessageHelper.UnSynchronizedData);
            return;
        }
    
        var bytes = await ApiService.GenerateVisitPdf(visitId);
        var content = Convert.ToBase64String(bytes);
    
        await JsRuntime.InvokeVoidAsync("savePdf", content, $"{visitId}.pdf");
    }
    
    private async Task SendVisitToEmailCallback(string visitId)
    {
        var visit = await VisitService.GetAsync(visitId);
        visit.NotifyByEmail = !visit.NotifyByEmail;
        visit.FinishedAt = visit.NotifyByEmail ? DateTime.UtcNow : null;
        
        await JsRuntime.InvokeVoidAsync("removeFocusFromAllElements");
        await VisitService.UpdateAsync(visit);
    }
}