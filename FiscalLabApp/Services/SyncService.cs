using System.Text.Json;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class SyncService(
    IApiService apiService,
    IPlantService plantService,
    IAssociationService associationService,
    IMenuService menuService,
    IVisitService visitService,
    IndexedDbAccessor indexedDbAccessor)
{
    private const string LastSyncInfoCollectionName = "last_sync_info";
    private const string LastSyncInfoKey = "last_sync_at";
    private const string LastUpdateInfoCollectionName = "last_update_info";
    private const string LastUpdateInfoKey = "last_update_at";
    
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

        if (syncResult.Plants.Length > 0)
        {
            await plantService.CreateManyAsync(syncResult.Plants);
        }
        
        if (syncResult.Associations.Length > 0)
        {
            await associationService.CreateManyAsync(syncResult.Associations);
        }
        
        if (syncResult.Menus.Length > 0)
        {
            await menuService.CreateManyAsync(syncResult.Menus);
        }
        
        if (syncResult.Visits.Length > 0)
        {
            await visitService.CreateManyAsync(syncResult.Visits);
        }
    }

    public async Task<bool> NeedSync()
    {
        var lastSyncInfo = await indexedDbAccessor.GetValueByIdAsync<SyncInfoModel>(LastSyncInfoCollectionName, LastSyncInfoKey);
        Console.WriteLine(JsonSerializer.Serialize(lastSyncInfo));
        var lastUpdateInfo = await indexedDbAccessor.GetValueByIdAsync<SyncInfoModel>(LastUpdateInfoCollectionName, LastUpdateInfoKey);
        Console.WriteLine(JsonSerializer.Serialize(lastUpdateInfo));
        return lastUpdateInfo.Value > lastSyncInfo.Value;
    }
}