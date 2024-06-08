using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace FiscalLabApp.Extensions;

public static class StringExtensions
{
    private static readonly NumberFormatInfo NumberFormatter =  new NumberFormatInfo
    {
        NumberGroupSeparator = ".",
        NumberDecimalSeparator = ","
    };
    private static string RemoveSpacesAndNewLines(this string source)
    {
        return source
            .Replace("\n", string.Empty)
            .Replace("\r", string.Empty)
            .Trim();
    }
    
    public static IEnumerable<string> SplitByBar(this string source, string token)
    {
        return source.Split(token)
            .Select(o => o.RemoveSpacesAndNewLines())
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToArray();
    }
    
    public static string FormatToBrazil(this float value)
    {
        return value.ToString("n2", NumberFormatter);
    }
}