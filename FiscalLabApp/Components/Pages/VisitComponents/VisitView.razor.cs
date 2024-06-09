using System.Text.Json;
using FiscalLabApp.Features.Visits;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Pages.VisitComponents;

public partial class VisitView : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IVisitService VisitService { get; set; } = null!;
    [Inject] private IMenuService MenuService { get; set; } = null!;
    [Inject] private SelectedVisitEventNotifier SelectedVisitEventNotifier { get; set; } = null!;
    [Parameter] public string VisitId { get; set; } = string.Empty;
    [Parameter]
    [SupplyParameterFromQuery]
    public bool IsReadyOnly { get; set; }
    private Menu[] _menus = [];
    private string _selectedPage = PageHelper.BasicInformationPageName;

    public Models.Visit Visit { get; set; } = new();
    private VisitPage[] _pages = PageHelper.GetPages();
    

    //TODO alterar data para utc antes de salvar visita
    // if (DesintegratorProbe.SharpenedOrReplacedKnifeAt.HasValue)
    // {
    //     DesintegratorProbe.SharpenedOrReplacedKnifeAt = DesintegratorProbe.SharpenedOrReplacedKnifeAt.Value.ToUniversalTime();
    // }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        if (!string.IsNullOrEmpty(uri.Fragment))
        {
            _selectedPage = uri.Fragment.TrimStart('#');
        }
    }
    
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (string.IsNullOrWhiteSpace(VisitId)) return;

        Visit = IsReadyOnly ? await VisitService.GetByIdOnlineAsync(VisitId) : await VisitService.GetByIdLocalAsync(VisitId);
        _menus = await MenuService.GetAllAsync();
    }

    public void OnPageChangeHandler(string page)
    {
        _pages = _pages
            .Select(x => new VisitPage
            {
                Id = x.Id,
                DisplayName = x.DisplayName,
                Name = x.Name,
                TotalItems = x.TotalItems,
                PendingItems = x.PendingItems - 1
            })
            .ToArray();
        
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
    
    private void ShowState()
    {
        Console.WriteLine(JsonSerializer.Serialize(Visit.BasicInformation));
    }
    
    private void OnBalanceTestsClickHandler()
    {
        Console.WriteLine("abrindo testes de balanca");
        // if (Layout.SelectedVisit is null) return;
        // NavigationManager.NavigateTo($"visits/{Layout.SelectedVisit.Id}/balance-tests");
    }
}