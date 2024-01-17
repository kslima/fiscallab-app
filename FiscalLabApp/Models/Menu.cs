namespace FiscalLabApp.Models;

public class Menu
{
    public string Code { get; set; } = string.Empty;
    public string Page { get; set; } = string.Empty;
    public bool AddPercentageOptions { get; set; }
    public List<string> Options { get; set; } = new ();
}