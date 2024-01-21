namespace FiscalLabApp.Models;

public class Menu
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Page { get; set; } = string.Empty;
    public bool HasPercentageOptions { get; set; }
    public List<Option> Options { get; set; } = [];
}