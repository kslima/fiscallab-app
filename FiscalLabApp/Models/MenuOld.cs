namespace FiscalLabApp.Models;

public class MenuOld
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<MenuOptionOld> Options { get; set; } = new();
}