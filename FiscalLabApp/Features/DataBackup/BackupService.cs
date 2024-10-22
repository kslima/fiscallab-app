using FiscalLabApp.Features.Visits;
using FiscalLabApp.Interfaces;

namespace FiscalLabApp.Features.DataBackup;

public class BackupService : IBackupService
{
    private readonly IPlantService _plantService;
    private readonly IAssociationService _associationService;
    private readonly IMenuService _menuService;
    private readonly IVisitService _visitService ;

    public BackupService(
        IPlantService plantService,
        IAssociationService associationService,
        IMenuService menuService,
        IVisitService visitService)
    {
        _plantService = plantService;
        _associationService = associationService;
        _menuService = menuService;
        _visitService = visitService;
    }

    public async Task<BackupResult> CreateAsync()
    {
        var plants = await _plantService.ListAllLocalAsync();
        var associations = await _associationService.ListAllLocalAsync();
        var menus = await _menuService.GetAllAsync();
        var visits = await _visitService.GetAllLocalAsync();
        
        return new BackupResult
        {
            CreatedAt = DateTime.UtcNow,
            Plants = plants,
            Associations = associations,
            Menus = menus,
            Visits = visits
        };
    }
}