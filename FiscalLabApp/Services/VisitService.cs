using FiscalLabApp.Enums;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

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

    public async Task<Visit> UpdateAsync(Visit visit)
    {
        return await CreateAsync(visit);
    }

    public async Task<Visit> GetByIdAsync(string id)
    {
        return await _indexedDbAccessor.GetValueByIdAsync<Visit>(CollectionsHelper.VisitsCollection, id);
    }

    public async Task DeleteAsync(string id)
    {
        var visit = await _indexedDbAccessor.GetValueByIdAsync<Visit>(CollectionsHelper.VisitsCollection, id);
        if (visit.SyncedAt is not null)
        {
            visit.Status = VisitStatus.Cancelled;
            await UpdateAsync(visit);
            return;
        }
        await _indexedDbAccessor.DeleteAsync(CollectionsHelper.VisitsCollection, id);
    }

    public async Task<Visit[]> GetAllAsync()
    {
        return await _indexedDbAccessor.GetValueAsync<Visit[]>(CollectionsHelper.VisitsCollection);
    }

    public async Task<ApiResponse<Visit[]>> ListAsync(VisitParameters parameters)
    {
        return await _apiService.ListVisitsAsync(parameters);
    }
}