namespace FiscalLabApp.Models;

public class SyncResult
{
    public Plant[] Plants { get; set; } = Array.Empty<Plant>();
    public Association[] Associations { get; set; } = Array.Empty<Association>();
    public Menu[] Menus { get; set; } = Array.Empty<Menu>();
    public Visit[] Visits { get; set; } = Array.Empty<Visit>();
}