namespace FiscalLabApp.Models;

public class MenuOption
{
    public string Page { get; set; } = string.Empty;
    public string Menu { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
}