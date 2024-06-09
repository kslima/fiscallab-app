using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;

namespace FiscalLabApp.Features.Visits;

public class VisitService : IVisitService
{
    private readonly IndexedDbAccessor _indexedDbAccessor;
    private readonly IApiService _apiService;

    public VisitService(IndexedDbAccessor indexedDbAccessor, IApiService apiService)
    {
        _indexedDbAccessor = indexedDbAccessor;
        _apiService = apiService;
    }

    public async Task<Visit> CreateAsync(Visit visit)
    {
        await _indexedDbAccessor.SetValueAsync(CollectionsHelper.VisitsCollection, visit);
        return visit;
    }
    
    public async Task CreateManyAsync(Visit[] visits)
    {
        await _indexedDbAccessor.SetAllValuesAsync(CollectionsHelper.VisitsCollection, visits);
    }

    public async Task<bool> UpsertAsync(Visit[] visits)
    {
        return await _apiService.UpsertVisitsAsync(visits);
    }

    public async Task<Visit> UpdateAsync(Visit visit)
    {
        return await CreateAsync(visit);
    }

    public async Task<Visit> GetAsync(string id)
    {
        return await _indexedDbAccessor.GetValueByIdAsync<Visit>(CollectionsHelper.VisitsCollection, id);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var visit = await _indexedDbAccessor.GetValueByIdAsync<Visit>(CollectionsHelper.VisitsCollection, id);
        if (visit.SyncedAt is not null)
        {
            var successOnDelete = await _apiService.DeleteVisitAsync(id);
            if (!successOnDelete) return false;
            
            await _indexedDbAccessor.DeleteAsync(CollectionsHelper.VisitsCollection, id);
            return true;
        }
        
        await _indexedDbAccessor.DeleteAsync(CollectionsHelper.VisitsCollection, id);
        return true;
    }

    public async Task<Visit[]> GetAllLocalAsync()
    {
        return await _indexedDbAccessor.GetValueAsync<Visit[]>(CollectionsHelper.VisitsCollection);
    }

    public async Task<ApiResponse<Visit[]>> ListAsync(VisitParameters parameters)
    {
        return await _apiService.ListVisitsAsync(parameters);
    }

    public async Task RestoreAsync(VisitParameters parameters)
    {
        var visits = await _apiService.ListVisitsAsync(parameters);
        if (visits.IsFailure()) throw new InvalidOperationException();
        
        await CreateManyAsync(visits.Data!);
    }
}