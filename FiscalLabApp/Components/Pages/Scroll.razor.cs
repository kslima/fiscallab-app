using FiscalLabApp.Enums;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FiscalLabApp.Components.Pages;

public partial class Scroll : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IVisitService VisitService { get; set; } = null!;
    private int _currentPage = 1;
    private bool _hasNextPage = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnInitializedAsync();
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync(
                "checkDivScrollEnd",
                "myDiv",
                "OnDivScrollToEnd",
                DotNetObjectReference.Create(this)
            );

            await OnDivScrollToEnd();
        }
    }

    
    protected readonly List<VisitViewModel> Visits = [];

    [JSInvokable]
    public async Task OnDivScrollToEnd()
    {
        Console.WriteLine("Div scrolled to the end!");
        if (!_hasNextPage) return;
        
        var parameters = new VisitParameters
        {
            Status = VisitStatus.Done,
            PageIndex = _currentPage,
            PageSize = 1
        };
        
        var response = await VisitService.ListAsync(parameters);
        _hasNextPage = response.Metadata?.HasNextPage == true;
        _currentPage = _hasNextPage ? _currentPage + 1 : _currentPage;
        
        var visits =  response.Data!.Adapt<List<VisitViewModel>>();
        Visits.AddRange(visits);
        StateHasChanged();
    }
}