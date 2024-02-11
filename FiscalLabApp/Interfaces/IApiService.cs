using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IApiService
{
    Task<VisitPage[]> GetAllVisitPagesAsync();
    Task<Menu[]> GetAllMenusAsync();
    Task<Plant[]> GetAllPlantsAsync();
    Task<Association[]> GetAllAssociationsAsync();
    Task<bool> CreateManyVisits(Visit[] visits);
    Task<SyncResult> SyncDataAsync(SyncModel syncModel);
}