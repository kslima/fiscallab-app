using FiscalLabApp.Extensions;
using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class BenchmarkingEquipmentComponent : ComponentBase
{
    [Parameter]
    public Menu[] Menus { get; set; } = [];
    
    [Parameter]
    public BenchmarkingEquipment BenchmarkingEquipment { get; set; } = new();
    [Parameter] public EventCallback<Menu> OnEditOptionsButtonClick { get; set; }
    
    public void ExpectedCropChanged(decimal value)
    {
        CalculateCropPercentageRealized();
    }
    
    private void CalculateCropPercentageRealized()
    {
        if (BenchmarkingEquipment.ExpectedCrop == 0) return; 
        var value = BenchmarkingEquipment.AccomplishedCrop / BenchmarkingEquipment.ExpectedCrop * 100;
        BenchmarkingEquipment.PercentageRealized = value.RoundToEven(2);
    }

    private void CalculateCropVariation()
    {
        BenchmarkingEquipment.VariationBetweenCrops = BenchmarkingEquipment.AccomplishedCrop - BenchmarkingEquipment.PreviousCrop;
    }
    
    private void CalculateFiberVariation()
    {
        BenchmarkingEquipment.FiberVariation = BenchmarkingEquipment.CurrentFiber - BenchmarkingEquipment.PreviousFiber;
    }
    
    private void CalculateAtrVariation()
    {
        BenchmarkingEquipment.AtrVariation = BenchmarkingEquipment.CurrentAtr - BenchmarkingEquipment.PreviousAtr;
    }
    
    private async Task OnEditOptionsButtonClickHandler(Menu menu)
    {
        await OnEditOptionsButtonClick.InvokeAsync(menu);
    }
}