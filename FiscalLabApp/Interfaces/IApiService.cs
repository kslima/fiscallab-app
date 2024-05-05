using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IApiService
{
    Task<ApiResponse<Plant>> CreatePlantAsync(Plant plant);
    Task<ApiResponse<Plant>> UpdatePlantAsync(string plantId, Plant plant);
    Task<VisitPage[]> GetAllVisitPagesAsync();
    Task<Menu[]> GetAllMenusAsync();
    Task<Plant[]> GetAllPlantsAsync();
    Task<Association[]> GetAllAssociationsAsync();
    Task<bool> CreateManyVisits(Visit[] visits);
    Task<byte[]> GenerateVisitPdf(string visitId);
    Task<SyncResult> SyncDataAsync(SyncModel syncModel);
}