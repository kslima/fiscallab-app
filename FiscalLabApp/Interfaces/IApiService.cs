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
    Task<Plant[]> ListPlantsAsync();
    Task<ApiResponse<bool>> DeleteAssociationAsync(string associationId);
    Task<ApiResponse<bool>> DeletePlantAsync(string plantId);
    Task<Association[]> ListAssociationsAsync();
    Task<ApiResponse<Visit[]>> ListVisitsAsync(VisitParameters parameters);
    Task<bool> CreateManyVisits(Visit[] visits);
    Task<byte[]> GenerateVisitPdf(string visitId);
    Task<Visit> GetVisitByIdAsync(string visitId);
    Task<bool> DeleteVisitAsync(string visitId);
    Task<bool> UpsertVisitsAsync(Visit[] visits);
}