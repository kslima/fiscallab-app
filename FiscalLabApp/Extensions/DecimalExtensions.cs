namespace FiscalLabApp.Extensions;

public static class DecimalExtensions
{
    public static decimal RoundToEven(this decimal value, int decimals)
    {
        return decimal.Round(value, decimals, MidpointRounding.ToEven);
    }
}