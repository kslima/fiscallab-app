using System.Text.Json;

namespace FiscalLabApp.Helpers;

public static class JsonHelper
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
        WriteIndented = true
    };
}