using FiscalLabApp.Enums;
using FiscalLabApp.Features.Visits;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Features.Restore;

public class RestoreService : IRestoreService
{
    private readonly IPlantService _plantService;
    private readonly IAssociationService _associationService;
    private readonly IMenuService _menuService;
    private readonly IVisitService _visitService ;
    private readonly IAuthenticationService _authenticationService ;

    public RestoreService(
        IPlantService plantService,
        IAssociationService associationService,
        IMenuService menuService,
        IVisitService visitService,
        IAuthenticationService authenticationService)
    {
        _plantService = plantService;
        _associationService = associationService;
        _menuService = menuService;
        _visitService = visitService;
        _authenticationService = authenticationService;
    }
    
    public async Task RestoreAsync(VisitParameters visitParameters)
    {
        await _plantService.RestoreAsync();
        await _associationService.RestoreAsync();
        await _menuService.RestoreAsync();
        
        await _visitService.RestoreAsync(visitParameters);

        await _authenticationService.LogoutAsync();
    }
}