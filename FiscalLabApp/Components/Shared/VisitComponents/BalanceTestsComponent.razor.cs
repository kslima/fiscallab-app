using FiscalLabApp.Features.Visits;
using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class BalanceTestsComponent : ComponentBase
{
    [Inject] private IVisitService VisitService { get; set; } = null!;
    [Parameter] public List<BalanceTest> Tests { get; set; } = [];
    private BalanceTest? _selectedTest;
    private EditContext? _editContext;
    private ValidationMessageStore? _messageStore;

    protected override void OnInitialized()
    {
        _selectedTest = new BalanceTest();
        _editContext = new EditContext(_selectedTest);
        _editContext.OnValidationRequested += HandleValidationRequested;
        _messageStore = new ValidationMessageStore(_editContext);
    }

    private void HandleValidationRequested(object? sender,
        ValidationRequestedEventArgs args)
    {
        _messageStore?.Clear();

        if (string.IsNullOrWhiteSpace(_selectedTest?.Identification))
        {
            _messageStore?.Add(() => _selectedTest!.Identification, "Identificação é obrigatório.");
        }
    }
    
    private void OnSubmitHandler()
    {
        if (_editContext!.Validate())
        {
            OnSaveButtonClickHandler();
            return;
        }

        _editContext.NotifyValidationStateChanged();
    }
    private void Edit(BalanceTest item) => _selectedTest = item;
    
    private void OnDeleteButtonClickHandler()
    {
        if (_selectedTest is null) return;
        
        Tests.Remove(_selectedTest);
        _selectedTest = new BalanceTest();
    }

    private bool DisableDeleteButton()
    {
        return !Tests.Exists(x => x.Identification.Equals(_selectedTest.Identification));
    }

    private void OnSaveButtonClickHandler()
    {
        if (_selectedTest is null) return;
        
        if (Tests.Exists(x => x.Identification.Equals(_selectedTest.Identification)))
        {
            _selectedTest = new BalanceTest();
            StateHasChanged();
            return;
        }

        Tests.Add(_selectedTest);
        _selectedTest = new BalanceTest();
        StateHasChanged();
    }

    private static void UpdateVariationBetweenBalances(BalanceTest balanceTest)
    {
        balanceTest.VariationBetweenBalances = balanceTest.InputBalanceWeight - balanceTest.OutputBalanceWeight;
    }

    // private async Task UpdateTests()
    // {
    //     if (_selectedVisit is null) return;
    //
    //     _selectedVisit.BalanceTests = Tests.Adapt<List<BalanceTest>>();
    //     await VisitService.UpdateAsync(_selectedVisit);
    //
    //     NavigationManager.NavigateTo($"visits/{_selectedVisit.Id}/sugarcane-balance");
    // }
}