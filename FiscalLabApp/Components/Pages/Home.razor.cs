using Blazored.Toast.Services;
using FiscalLabApp.Components.Shared;
using FiscalLabApp.Enums;
using FiscalLabApp.Extensions;
using FiscalLabApp.Features.Visits;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FiscalLabApp.Components.Pages;

public partial class Home : ComponentBase
{
    private const string OfflineVisitsTabName = "offline-visits";
    private const string OnlineVisitsTabName = "online-visits";

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject]
    private ApplicationContextAccessor ApplicationContextAccessor { get; set; } = null!;
    [Inject] private IVisitContextAccessor VisitContextAccessor { get; set; } = null!;
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
    private ModalDialog _imagesModalDialog = null!;
    private bool _isImagesProcessing;
    private bool _isGetOnlineVisitsProcessing;

    [Parameter]
    public string ActiveTab { get; set; } = OfflineVisitsTabName;
    private List<Visit> _visits = [];
    private Visit _selectedVisit = new();
    private  IReadOnlyList<Image> _selectedVisitImages = [];
    
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
        VisitContextAccessor.SelectedVisit = new Visit
        {
            BasicInformation =
            {
                VisitDate = DateOnly.FromDateTime(DateTime.UtcNow),
                VisitTime = TimeOnly.FromDateTime(DateTime.UtcNow)
            }
        };
        NavigationManager.NavigateTo("/visit-view");
    }
    
    private async Task LoadingOfflineVisitsAsync()
    {
        var visits = await VisitService.GetAllLocalAsync();
        _visits = visits
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }
    
    private async Task LoadingOnlineVisitsAsync()
    {
        if (!_hasNextPage) return;

        _isGetOnlineVisitsProcessing = true;
        
        await ListVisitsAsync();
        
        _isGetOnlineVisitsProcessing = false;
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
        
        _visits.AddRange(response.Data!);
    }
    
    public async Task OnDeleteButtonClickHandler(Visit visit)
    {
        var confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Remover visita ?");
        if (!confirmed) return;
        
        if (visit.SyncedAt is null)
        {
            await VisitService.DeleteLocalAsync(visit.Id);
        }
        else
        {
            if (ApplicationContextAccessor.IsOffline())
            {
                ToastService.ShowError(MessageHelper.NoInternetConnection);
                return;
            }
            await VisitService.DeleteAsync(visit.Id);
        }
        
        await JsRuntime.RemoveFocusFromAllElementsAsync();
        await LoadingOfflineVisitsAsync();
        StateHasChanged();
    }
    
    public void OpenVisitButtonClickHandler(Visit visit)
    {
        _selectedVisit = visit;
        VisitContextAccessor.SelectedVisit = visit;
        NavigationManager.NavigateTo("/visit-view");
    }
    
    private async Task OnPdfButtonClickHandler(string visitId)
    {
        if (ApplicationContextAccessor.IsOffline())
        {
            ToastService.ShowError(MessageHelper.NoInternetConnection);
            return;
        }
    
        var visit = await VisitService.GetByIdLocalAsync(visitId);
        if (visit.SyncedAt is null)
        {
            ToastService.ShowError(MessageHelper.UnSynchronizedData);
            return;
        }
    
        var bytes = await ApiService.GenerateVisitPdf(visitId);
        var content = Convert.ToBase64String(bytes);
    
        await JsRuntime.InvokeVoidAsync("savePdf", content, $"{visitId}.pdf");
    }
    
    private async Task OnSendToEmailButtonClickHandler(string visitId)
    {
        var visit = await VisitService.GetByIdLocalAsync(visitId);
        visit.NotifyByEmail = !visit.NotifyByEmail;
        visit.FinishedAt = visit.NotifyByEmail ? DateTime.UtcNow : null;
        
        await JsRuntime.RemoveFocusFromAllElementsAsync();
        await VisitService.UpdateAsync(visit);
    }
    
    private async Task OpenImagesClickHandler(Visit visit)
    {
        if (visit.NotifiedByEmailAt is not null)
        {
            ToastService.ShowError(MessageHelper.VisitAlreadyEndedError);
            return;
        }

        if (ApplicationContextAccessor.IsOffline())
        {
            ToastService.ShowError(MessageHelper.NoInternetConnection);
            return;
        }
        _selectedVisit = visit;
        await _imagesModalDialog.Open(LoadingImagesCallback);
    }
    
    private async Task LoadingImagesCallback()
    {
        var images = await VisitService.ListImagesAsync(_selectedVisit.Id);
        _selectedVisitImages = images.ToList();
    }
    
    private async Task OnSaveImagesButtonClickHandler(List<Image> images)
    {
        if (images.Count == 0) return;
        _isImagesProcessing = true;
        
        var successOnUpsertImages = await VisitService.ReplaceImagesAsync(_selectedVisit.Id, images);
        if (successOnUpsertImages)
        {
            ToastService.ShowSuccess(MessageHelper.SuccessOnUpsertImagens);
        }
        else
        {
            ToastService.ShowError(MessageHelper.ErrorOnUpsertImagens);
        }
        
        _isImagesProcessing = false;
        _imagesModalDialog.Close();
    }
}