using FiscalLabApp.Models;

namespace FiscalLabApp.Features.DataBackup;

public class BackupResult
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Plant[] Plants { get; set; } = [];
    public Association[] Associations { get; set; } = [];
    public Menu[] Menus { get; set; } = [];
    public Visit[] Visits { get; set; } = [];
}