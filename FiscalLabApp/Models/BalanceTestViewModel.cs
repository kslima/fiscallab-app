namespace FiscalLabApp.Models;

public class BalanceTestViewModel
{
    public string Id { get; set; } = string.Empty;
    public string TruckNumber { get; set; } = string.Empty;
    public float InputBalanceWeight { get; set; }
    public float OutputBalanceWeight { get; set; }
    public float VariationBetweenBalances { get; set; }
}