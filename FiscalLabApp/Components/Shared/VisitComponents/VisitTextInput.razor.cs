using FiscalLabApp.Enums;
using FiscalLabApp.Extensions;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class VisitTextInput : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IMenuService MenuService { get; set; } = null!;
    [Inject] private SelectedVisitEventNotifier SelectedVisitEventNotifier { get; set; } = null!;
    [Inject] private NetworkStatusEventNotifier NetworkStatusEventNotifier { get; set; } = null!;
    [Inject] private ApplicationContextAccessor ApplicationContextAccessor { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Parameter] public Menu? Menu { get; set; }

    private string _menuId = string.Empty;
    
    private int Rows { get; set; }
    
    [Parameter] public string Id { get; set; } = string.Empty;
    [Parameter] public bool IsReadyOnly { get; set; }

    [Parameter] public string Title { get; set; } = string.Empty;

    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public bool Required { get; set; }

    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public EventCallback ValueUpdated { get; set; }
    [Parameter] public EventCallback<Menu> OnEditButtonClick { get; set; }
    
    [CascadingParameter] public string Callback { get; set; } = "/";
    
    private string _modalDisplay = "none;";
    private string _modalClass = string.Empty;
    private bool _showBackdrop = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        NetworkStatusEventNotifier.NetworkStatusChangedEvent += OnNetworkStatusChanged;
    }
    
    private void OnNetworkStatusChanged(object? sender, NetworkStatus status)
    {
        StateHasChanged();
    }
    
    private void OpenOptions()
    {
        _modalDisplay = "block";
        _modalClass = "show";
        _showBackdrop = true;
    }

    private void CloseOptions()
    {
        _modalDisplay = "none";
        _modalClass = string.Empty;
        _showBackdrop = false;
    }

    private async Task UpdateParent(ChangeEventArgs e)
    {
        await ValueChanged.InvokeAsync(e.Value?.ToString());
    }

    private void OnBlur(FocusEventArgs e)
    {
        SelectedVisitEventNotifier.NotifyFill();
    }

    protected override void OnParametersSet()
    {
        if (Menu is null) return;

        Menu.Options.Sort((a, b) => string.Compare(a.Description, b.Description, StringComparison.Ordinal));

        if (Menu.HasPercentageOptions)
        {
            foreach (var value in Enumerable.Range(1, 100))
            {
                Menu.Options.Add(new Option { Description = $"{value}%" });
            }
        }

        _menuId = Menu.Id;
        CalculateSize();
    }
    
    private async Task OptionSelected(Option opt)
    {
        Value = opt.Description;
        await ValueChanged.InvokeAsync(opt.Description);
        CloseOptions();
    }

    private async Task EditButtonHandler()
    {
        await OnEditButtonClick.InvokeAsync(Menu);
    }
    
    private void CalculateSize()
    {
        Rows = Value.Length / 30 + 1;
    }
}