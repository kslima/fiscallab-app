using System.Text.Json;
using FiscalLabApp.Enums;
using FiscalLabApp.Features.Backup;
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
        var syncData = await GetSyncDataAsync();
        
        if (syncData.Menus.Length == 0)
        {
            var options = await apiService.ListOptionsAsync();
            await menuService.CreateManyAsync(options);
        }
        
        if (syncData.Associations.Length == 0)
        {
            var associations = await apiService.ListAssociationsAsync();
            await associationService.CreateManyAsync(associations);
        }
        
        if (syncData.Plants.Length == 0)
        {
            var plants = await apiService.ListPlantAsync();
            await plantService.CreateManyAsync(plants);
        }
        
        var visits = await visitService.GetAllAsync();
        if (visits.Length == 0)
        {
            var response = await apiService.ListVisitsAsync(new VisitParameters{Status = VisitStatus.InProgress});
            visits = response.Data!;
            await visitService.CreateManyAsync(visits);
            return;
        }
        
        var syncModel = new SyncModel
        {
            Visits = visits
                .Where(v => v.Status == VisitStatus.InProgress)
                .ToArray()
        };

        var syncResult = await apiService.SyncDataAsync(syncModel);
        if (syncResult.Visits.Length > 0)
        {
            await visitService.CreateManyAsync(syncResult.Visits);
        }
    }
    
    public async Task<SyncModel> GetSyncDataAsync()
    {
        var visits = await visitService.GetAllAsync();
        visits = visits
            .Where(v => v.Status != VisitStatus.Done)
            .ToArray();

        var syncModel = new SyncModel
        {
            Visits = visits
        };

        return syncModel;
    }
}