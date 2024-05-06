using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IApiService
{
    Task<ApiResponse<Plant>> CreatePlantAsync(Plant plant);
    Task<ApiResponse<Plant>> UpdatePlantAsync(string plantId, Plant plant);
    Task<ApiResponse<Association>> CreateAssociationAsync(Association association);
    Task<ApiResponse<Association>> UpdateAssociationAsync(string associationId, Association association);
    Task<VisitPage[]> GetAllVisitPagesAsync();
    Task<Menu[]> ListOptionsAsync();
    Task<Plant[]> ListPlantAsync();
    Task<Association[]> ListAssociationsAsync();
    Task<bool> CreateManyVisits(Visit[] visits);
    Task<byte[]> GenerateVisitPdf(string visitId);
    Task<SyncResult> SyncDataAsync(SyncModel syncModel);
}