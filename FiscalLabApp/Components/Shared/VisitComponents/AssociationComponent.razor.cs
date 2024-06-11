using Blazored.Toast.Services;
using FiscalLabApp.Extensions;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class AssociationComponent : ComponentBase
{
    [Inject] private IAssociationService AssociationService { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private ApplicationContextAccessor ApplicationContextAccessor { get; set; } = null!;
    [Parameter] public Association? Association { get; set; }
    [Parameter] public EventCallback OnSaveOrUpdate { get; set; }

    private const string SplitToken = ";";

    private int Rows { get; set; }

    public AssociationViewModel? AssociationViewModel;
    private string SelectedState { get; set; } = string.Empty;
    private EditContext? _editContext;
    private ValidationMessageStore? _messageStore;
    
    protected override void OnInitialized()
    {
        AssociationViewModel ??= new AssociationViewModel();
        _editContext = new EditContext(AssociationViewModel);
        _editContext.OnValidationRequested += HandleValidationRequested;
        _messageStore = new ValidationMessageStore(_editContext);
    }
    
    private void HandleValidationRequested(object? sender,
        ValidationRequestedEventArgs args)
    {
        _messageStore?.Clear();
        
        if (string.IsNullOrWhiteSpace(AssociationViewModel?.Name))
        {
            _messageStore?.Add(() => AssociationViewModel!.Name, "Nome é obrigatório.");
        }
        
        if (string.IsNullOrWhiteSpace(AssociationViewModel?.City))
        {
            _messageStore?.Add(() => AssociationViewModel!.City, "Cidade é obrigatório.");
        }
        
        if (string.IsNullOrWhiteSpace(AssociationViewModel?.State))
        {
            _messageStore?.Add(() => AssociationViewModel!.State, "Estado é obrigatório.");
        }
        
        if (string.IsNullOrWhiteSpace(AssociationViewModel?.Emails))
        {
            _messageStore?.Add(() => AssociationViewModel!.Emails, "Email é obrigatório.");
        }
    }
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (Association is null)
        {
            ResetParameters();
            return;
        }

        AssociationViewModel = Association.AsAssociationViewModel();
        SelectedState = AssociationViewModel.State;

        AssociationViewModel.Emails = Association.Emails.Count == 0
            ? string.Empty
            : $"{string.Join($"{SplitToken}{Environment.NewLine}{Environment.NewLine}", Association.Emails.Select(e => e.Address))}";

        var emailIsEmptyAndNotContainsSplitToken = !string.IsNullOrWhiteSpace(AssociationViewModel.Emails) &&
                                                  !AssociationViewModel.Emails.EndsWith(SplitToken);
        if (emailIsEmptyAndNotContainsSplitToken)
        {
            AssociationViewModel.Emails += SplitToken;
        }

        Rows = Association.Emails.Count * 2;
    }

    private void ResetParameters()
    {
        AssociationViewModel = new AssociationViewModel();
        SelectedState = string.Empty;
        Rows = default;
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

        var emails = AssociationViewModel!.Emails
            .SplitByBar(SplitToken)
            .Select(e => new Email { Address = e })
            .ToList();

        if (Association is null)
        {
            var association = new Association
            {
                Id = Guid.NewGuid().ToString(),
                Name = AssociationViewModel.Name,
                Address = new Address
                {
                    City = AssociationViewModel.City,
                    State = AssociationViewModel.State
                },
                Emails = emails
            };
            await AssociationService.CreateAsync(association);
            ToastService.ShowSuccess(MessageHelper.SuccessOnCreateAssociation);
        }
        else
        {
            Association.Name = AssociationViewModel.Name;
            Association.Address.City = AssociationViewModel.City;
            Association.Address.State = AssociationViewModel.State;
            Association.Emails = emails;
            await AssociationService.UpdateAsync(Association.Id, Association);
            ToastService.ShowSuccess(MessageHelper.SuccessOnUpdateAssociation);
        }

        await OnSaveOrUpdate.InvokeAsync();
    }

    private void OnStateChangeHandler()
    {
        AssociationViewModel!.State = SelectedState;
    }
}