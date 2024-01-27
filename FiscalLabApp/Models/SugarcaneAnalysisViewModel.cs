using FiscalLabApp.Enums;

namespace FiscalLabApp.Models;

public class SugarcaneAnalysisViewModel
{
    public decimal Pbu { get; set; }
    public decimal Bri { get; set; }
    public decimal Ls { get; set; }
    public decimal Atr { get; set; }
    public decimal Lsc { get; set; }
    public decimal Pol { get; set; }
    public decimal Purity { get; set; }
    public decimal Fiber { get; set; }
    public decimal Pcc { get; set; }
    public decimal Cs { get; set; }
    public decimal Ar { get; set; }

    public SugarcaneAnalysisViewModel(
        Clarify clarify,
        decimal pbu,
        decimal brix,
        decimal ls)
    {
        Lsc = CorrectedSaccharimetry(clarify, ls);
        Pol = (0.2605m - 0.0009882m * brix) * Lsc;
        Purity = Pol / brix * 100;
        Fiber = 0.08m * pbu + 0.876m;
        Cs = 1.0313m - 0.00575m * Fiber;
        Pcc = Pol * (1 - 0.01m * Fiber) * Cs;
        var arBroth = 3.641m - 0.0343m * Purity;
        Ar = arBroth * (1 - 0.01m * Fiber) * Cs;
        Atr = 9.6316m * Pcc + 9.15m * Ar;
    }

    private static decimal CorrectedSaccharimetry(Clarify clarify, decimal originalLs)
    {
        return clarify switch
        {
            Clarify.Octapol => 0.99879m * originalLs + 0.47374m,
            Clarify.Chiaro => 0.99879m * originalLs + 0.47374m,
            Clarify.Aluminio => 1.00621m * originalLs + 0.05117m,
            Clarify.Sugarpol => 1.00621m * originalLs + 0.05117m,
            Clarify.Filterpol => 0.9914m * originalLs + 1.444m,
            Clarify.Claripol => 0.98m * originalLs + 1.78m,
            _ => throw new ArgumentOutOfRangeException(nameof(clarify), clarify, null)
        };
    }
}