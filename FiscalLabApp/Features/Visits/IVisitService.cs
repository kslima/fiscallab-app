using FiscalLabApp.Models;

namespace FiscalLabApp.Features.Visits;

public interface IVisitService
{
    Task<Visit> CreateAsync(Visit visit);
    Task CreateManyAsync(Visit[] visits);
    Task<bool> UpsertAsync(Visit[] visits);
    Task<Visit> UpdateAsync(Visit visit);
    Task<Visit> GetByIdLocalAsync(string id);
    Task<Visit> GetByIdOnlineAsync(string id);
    Task<bool> DeleteAsync(string id);
    Task<bool> DeleteImageAsync(string id, string imageId);
    Task<bool> ReplaceImagesAsync(string id, List<Image> images);
    Task<Image[]> ListImagesAsync(string id);
    Task<Visit[]> GetAllLocalAsync();
    Task<ApiResponse<Visit[]>> ListAsync(VisitParameters parameters);
    Task RestoreAsync(VisitParameters parameters);
}