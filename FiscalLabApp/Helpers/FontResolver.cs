using System.Reflection;
using PdfSharp.Fonts;

namespace FiscalLabApp.Helpers;

public class FontResolver : IFontResolver
{
    public string DefaultFontName => throw new NotImplementedException();

    public byte[] GetFont(string faceName)
    {
        using var ms = new MemoryStream();
        using var fs = File.Open(faceName, FileMode.Open);
        fs.CopyTo(ms);
        ms.Position = 0;
        return ms.ToArray();
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        if (!familyName.Equals("Verdana", StringComparison.CurrentCultureIgnoreCase)) return null;
        return isBold switch
        {
            true when isItalic => new FontResolverInfo("Fonts/Verdana-BoldItalic.ttf"),
            true => new FontResolverInfo("Fonts/Verdana-Bold.ttf"),
            _ => isItalic switch
            {
                true => new FontResolverInfo("Fonts/Verdana-Italic.ttf"),
                _ => new FontResolverInfo("Fonts/Verdana-Regular.ttf")
            }
        };
    }
}