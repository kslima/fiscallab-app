namespace FiscalLabApp.Models;

public class Menu
{
    public string Code { get; set; } = string.Empty;
    public string Page { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new ();
}