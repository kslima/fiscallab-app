using Blazored.Toast.Services;
using FiscalLabApp.Extensions;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class PlantComponent : ComponentBase
{
    [Inject] private IPlantService PlantService { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private ApplicationContextAccessor ApplicationContextAccessor { get; set; } = null!;
    
    [Parameter] public EventCallback OnSaveOrUpdate { get; set; }
    [Parameter] public Plant? Plant { get; set; }
    public PlantViewModel? PlantViewModel;
    private string SelectedState { get; set; } = string.Empty;
    private EditContext? _editContext;
    private ValidationMessageStore? _messageStore;

    protected override void OnInitialized()
    {
        PlantViewModel ??= new PlantViewModel();
        _editContext = new EditContext(PlantViewModel);
        _editContext.OnValidationRequested += HandleValidationRequested;
        _messageStore = new ValidationMessageStore(_editContext);
    }
    
    protected override void OnParametersSet()
    {
        if (Plant is null)
        {
            ResetParameters();
            return;
        }
        
        PlantViewModel = Plant.AsPlantViewModel();
        SelectedState = PlantViewModel.State;
    }
    
    private void HandleValidationRequested(object? sender,
        ValidationRequestedEventArgs args)
    {
        _messageStore?.Clear();
        
        if (string.IsNullOrWhiteSpace(PlantViewModel?.Name))
        {
            _messageStore?.Add(() => PlantViewModel!.Name, "Nome é obrigatório.");
        }
        
        if (string.IsNullOrWhiteSpace(PlantViewModel?.City))
        {
            _messageStore?.Add(() => PlantViewModel!.City, "Cidade é obrigatório.");
        }
        
        if (string.IsNullOrWhiteSpace(PlantViewModel?.State))
        {
            _messageStore?.Add(() => PlantViewModel!.State, "Estado é obrigatório.");
        }
    }
    
    private void ResetParameters()
    {
        PlantViewModel = new PlantViewModel();
        SelectedState = string.Empty;
    }

    private async Task OnSubmitHandler()
    {
        if (_editContext!.Validate())
        {
            await OnSaveClickHandler();
            return;
        }
        
        _editContext.NotifyValidationStateChanged();
    }
    
    private async Task OnSaveClickHandler()
    {
        if (ApplicationContextAccessor.IsOffline())
        {
            ToastService.ShowError(MessageHelper.NoInternetConnection);
            return;
        }
        
        if (Plant is null)
        {
            PlantViewModel!.Id = Guid.NewGuid().ToString();
            var plant = PlantViewModel.AsPlant();
            await PlantService.CreateAsync(plant);
            ToastService.ShowSuccess(MessageHelper.SuccessOnCreatePlant);
        }
        else
        {
            Plant.Name = PlantViewModel!.Name;
            Plant.Address.City = PlantViewModel!.City;
            Plant.Address.State = PlantViewModel!.State;
            await PlantService.UpdateAsync(Plant.Id, Plant);
            ToastService.ShowSuccess(MessageHelper.SuccessOnUpdatePlant);
        }
        
        await OnSaveOrUpdate.InvokeAsync();
    }

    private void OnStateChangeHandler()
    {
        PlantViewModel!.State = SelectedState;
    }
}