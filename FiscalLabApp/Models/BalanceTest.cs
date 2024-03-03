namespace FiscalLabApp.Models;

public class BalanceTest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TruckNumber { get; set; } = string.Empty;
    public float InputBalanceWeight { get; set; }
    public float OutputBalanceWeight { get; set; }
    public float VariationBetweenBalances { get; set; }
}