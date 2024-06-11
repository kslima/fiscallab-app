using FiscalLabApp.Enums;
using FiscalLabApp.Extensions;
using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class SystemConsistencyComponent : ComponentBase
{
    [Parameter]
    public Menu[] Menus { get; set; } = [];
    
    [Parameter]
    public SystemConsistency SystemConsistency { get; set; } = new();
    private void OnClarifierSelected(ChangeEventArgs e)
    {
        if (e.Value is null) return;

        if (!Enum.TryParse<Clarify>(e.Value.ToString(), out var clarify)) return;
        SystemConsistency.Clarifier = clarify;
        CalculateAtr();
        CalculateAtrDifference();
        CalculatePurityDifference();
        CalculatePolDifference();
        CalculateFiberDifference();
        CalculatePccDifference();
        CalculateArDifference();
    }

    private void CalculateAtr()
    {
        if (SystemConsistency.Clarifier is null ||
            SystemConsistency.PlantSugarcaneAnalysis.Pbu == 0 ||
            SystemConsistency.PlantSugarcaneAnalysis.Brix == 0 ||
            SystemConsistency.PlantSugarcaneAnalysis.Ls == 0) return;

        var sugarcaneAnalysis = new SugarcaneAnalysisViewModel(
            SystemConsistency.Clarifier.Value,
            SystemConsistency.PlantSugarcaneAnalysis.Pbu,
            SystemConsistency.PlantSugarcaneAnalysis.Brix,
            SystemConsistency.PlantSugarcaneAnalysis.Ls);

        SystemConsistency.PlantSugarcaneAnalysis.Atr = sugarcaneAnalysis.Atr.RoundToEven(3);
        SystemConsistency.PlantSugarcaneAnalysis.Purity = sugarcaneAnalysis.Purity.RoundToEven(3);
        SystemConsistency.PlantSugarcaneAnalysis.Pol = sugarcaneAnalysis.Pol.RoundToEven(3);
        SystemConsistency.PlantSugarcaneAnalysis.Fiber = sugarcaneAnalysis.Fiber.RoundToEven(3);
        SystemConsistency.PlantSugarcaneAnalysis.Pcc = sugarcaneAnalysis.Pcc.RoundToEven(3);
        SystemConsistency.PlantSugarcaneAnalysis.Ar = sugarcaneAnalysis.Ar.RoundToEven(3);
    }

    private void CalculateAtrDifference()
    {
        SystemConsistency.DifferenceAtr = SystemConsistency.PlantSugarcaneAnalysis.Atr - SystemConsistency.ConsecanaSugarcaneAnalysis.Atr;
    }

    private void CalculatePurityDifference()
    {
        SystemConsistency.DifferencePurity = SystemConsistency.PlantSugarcaneAnalysis.Purity - SystemConsistency.ConsecanaSugarcaneAnalysis.Purity;
    }

    private void CalculatePolDifference()
    {
        SystemConsistency.DifferencePol = SystemConsistency.PlantSugarcaneAnalysis.Pol - SystemConsistency.ConsecanaSugarcaneAnalysis.Pol;
    }

    private void CalculateFiberDifference()
    {
        SystemConsistency.DifferenceFiber = SystemConsistency.PlantSugarcaneAnalysis.Fiber - SystemConsistency.ConsecanaSugarcaneAnalysis.Fiber;
    }

    private void CalculatePccDifference()
    {
        SystemConsistency.DifferencePcc = SystemConsistency.PlantSugarcaneAnalysis.Pcc - SystemConsistency.ConsecanaSugarcaneAnalysis.Pcc;
    }

    private void CalculateArDifference()
    {
        SystemConsistency.DifferenceAr = SystemConsistency.PlantSugarcaneAnalysis.Ar - SystemConsistency.ConsecanaSugarcaneAnalysis.Ar;
    }
}