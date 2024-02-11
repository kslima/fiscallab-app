using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class SyncService(IApiService apiService,
    IPlantService plantService,
    IAssociationService associationService,
    IMenuService menuService,
    IVisitService visitService)
{
    public async Task SyncAsync()
    {
        var plants = await plantService.GetAllAsync();
        var associations = await associationService.GetAllAsync();
        var menus = await menuService.GetAllAsync();
        var visits = await visitService.GetAllAsync();

        var syncModel = new SyncModel
        {
            Plants = plants,
            Associations = associations,
            Menus = menus,
            Visits = visits
        };

        var syncResult = await apiService.SyncDataAsync(syncModel);
    }
}