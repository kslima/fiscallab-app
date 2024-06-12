using Blazored.Toast.Services;
using FiscalLabApp.Components.Shared;
using FiscalLabApp.Extensions;
using FiscalLabApp.Features.Visits;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FiscalLabApp.Components.Pages;

public partial class VisitView : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IVisitService VisitService { get; set; } = null!;
    [Inject] private IAssociationService AssociationService { get; set; } = null!;
    [Inject] private IPlantService PlantService { get; set; } = null!;
    [Inject] private IMenuService MenuService { get; set; } = null!;
    [Inject] private SelectedVisitEventNotifier SelectedVisitEventNotifier { get; set; } = null!;
    [Inject] private IVisitContextAccessor VisitContextAccessor { get; set; } = null!;
    private ModalDialog _associationModalDialog = null!;
    private ModalDialog _plantModalDialog = null!;
    private bool _isReadyOnly;
    private Menu[] _menus = [];
    private string _selectedPage = PageHelper.BasicInformationPageName;
    public Visit Visit { get; set; } = new();
    private VisitPage[] _pages = [];

    private Association[] _associations = [];
    private Association? _selectedAssociation;
    private Plant? _selectedPlant;
    private Plant[] _plants = [];


    //TODO alterar data para utc antes de salvar visita
    // if (DesintegratorProbe.SharpenedOrReplacedKnifeAt.HasValue)
    // {
    //     DesintegratorProbe.SharpenedOrReplacedKnifeAt = DesintegratorProbe.SharpenedOrReplacedKnifeAt.Value.ToUniversalTime();
    // }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (VisitContextAccessor.SelectedVisit is null)
        {
            Visit = new Visit
            {
                BasicInformation =
                {
                    VisitDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    VisitTime = TimeOnly.FromDateTime(DateTime.UtcNow)
                }
            };
            _selectedAssociation = null;
            _selectedPlant = null;
        }
        else
        {
            Visit = VisitContextAccessor.SelectedVisit;
            _selectedAssociation = Visit.BasicInformation.Association;
            _selectedPlant = Visit.BasicInformation.Plant;
        }
        
        _isReadyOnly = Visit.NotifiedByEmailAt is not null;
        _menus = await MenuService.GetAllAsync();
        _pages = Visit.CreatePages();
        _associations = await AssociationService.ListAllLocalAsync();
        _plants = await PlantService.ListAllLocalAsync();

        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        if (!string.IsNullOrEmpty(uri.Fragment))
        {
            _selectedPage = uri.Fragment.TrimStart('#');
        }
    }
    
    public void OnPageChangeHandler(string page)
    {
        _pages = Visit.CreatePages();
        _selectedPage = page;

        var uri = new Uri(NavigationManager.Uri);
        var path = uri.GetLeftPart(UriPartial.Path);
        var query = uri.Query;
        var newUri = $"{path}{query}#{_selectedPage}";
        NavigationManager.NavigateTo(newUri);
    }

    private Menu[] GetMenuOptions(PageType pageType)
    {
        return _menus
            .Where(x => x.Page.Equals(pageType.ToString()))
            .ToArray();
    }

    private void OnBalanceTestsClickHandler()
    {
        Console.WriteLine("abrindo testes de balanca");
        // if (Layout.SelectedVisit is null) return;
        // NavigationManager.NavigateTo($"visits/{Layout.SelectedVisit.Id}/balance-tests");
    }

    private void OnAddPlantClickHandler()
    {
        _selectedPlant = null;
        _plantModalDialog.Open();
    }

    private void OnEditPlantClickHandler()
    {
        _selectedPlant = Visit.BasicInformation.Plant;
        _plantModalDialog.Open();
    }

    private async Task OnSaveOrUpdatePlantHandler()
    {
        _plants = await PlantService.ListAllLocalAsync();
        _plantModalDialog.Close();
    }

    private void OnAddAssociationClickHandler()
    {
        _selectedAssociation = null;
        _associationModalDialog.Open();
    }

    private void OnEditAssociationClickHandler()
    {
        _selectedAssociation = Visit.BasicInformation.Association;
        _associationModalDialog.Open();
    }

    private async Task OnSaveOrUpdateAssociationHandler()
    {
        _associations = await AssociationService.ListAllLocalAsync();
        _associationModalDialog.Close();
    }

    private async Task OnSaveClickHandler()
    {
        if (string.IsNullOrWhiteSpace(Visit.BasicInformation.Plant.Id))
        {
            ToastService.ShowError(MessageHelper.RequiredPlant);
            return;
        }
        
        if (string.IsNullOrWhiteSpace(Visit.BasicInformation.Association.Id))
        {
            ToastService.ShowError(MessageHelper.RequiredAssociation);
            return;
        }
        
        Visit = await VisitService.CreateAsync(Visit);
        _pages = Visit.CreatePages();
        await JsRuntime.RemoveFocusFromAllElementsAsync();
        ToastService.ShowSuccess(MessageHelper.SuccessOnUpdateVisit);
    }
}