namespace FiscalLabApp.Extensions;

public static class StringExtensions
{
    public static string RemoveSpacesAndNewLines(this string source)
    {
        return source
            .Replace("\n", string.Empty)
            .Replace("\r", string.Empty)
            .Trim();
    }
}