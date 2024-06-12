using FiscalLabApp.Extensions;
using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class OptionsComponent : ComponentBase
{
    private const string SplitToken = ";";
    [Parameter] public Menu Menu { get; set; } = null!;
    [Parameter]
    public EventCallback<Menu> OnSaveButtonClick { get; set; }
    public string RawOptions { get; set; } = string.Empty;
    private int Rows { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (Menu.Options.Count == 0) return;
        
        RawOptions = $"{string.Join($"{SplitToken}{Environment.NewLine}{Environment.NewLine}", Menu.Options.Select(o => o.Description))}";
        if (!RawOptions.EndsWith(';'))
        {
            RawOptions += ";";
        }
        
        Rows = Menu.Options.Count * 2;
    }

    private async Task OnSaveButtonClickHandler()
    {
        var options = RawOptions
            .SplitByBar(SplitToken)
            .Select(o => new Option { Description = o })
            .ToList();

        Menu.Options = options;
        await OnSaveButtonClick.InvokeAsync(Menu);
    }
}