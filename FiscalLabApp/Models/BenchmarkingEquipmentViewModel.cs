namespace FiscalLabApp.Models;

public class BenchmarkingEquipmentViewModel
{
    public string LoadCell { get; set; } = string.Empty;
    public string Thermometer { get; set; } = string.Empty;
    public string Tachometer { get; set; } = string.Empty;
    public string Pachymeter { get; set; } = string.Empty;
    public string Gm500 { get; set; } = string.Empty;
    public string Gm100 { get; set; } = string.Empty;
    public string Gm1 { get; set; } = string.Empty;
    public string SucroseTest { get; set; } = string.Empty;
    public string Range10 { get; set; } = string.Empty;
    public string Range20 { get; set; } = string.Empty;
    public string Range30 { get; set; } = string.Empty;
    public string Z25 { get; set; } = string.Empty;
    public string Z50 { get; set; } = string.Empty;
    public string Z75 { get; set; } = string.Empty;
    public string Z100 { get; set; } = string.Empty;
    public string Observations11 { get; set; } = string.Empty;
    public decimal ExpectedCrop { get; set; }
    public decimal AccomplishedCrop { get; set; }
    public decimal PreviousCrop { get; set; }
    public decimal PercentageRealized { get; set; }
    public decimal VariationBetweenCrops { get; set; }
    public decimal CurrentFiber { get; set; }
    public decimal PreviousFiber { get; set; }
    public decimal FiberVariation { get; set; }
    public decimal CurrentAtr { get; set; }
    public decimal PreviousAtr { get; set; }
    public decimal AtrVariation { get; set; }
    public string Observations12 { get; set; } = string.Empty;
}