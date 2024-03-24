using FiscalLabApp.Enums;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class VisitService(IndexedDbAccessor indexedDbAccessor, IApiService apiService) : IVisitService
{
    private const string VisitCollectionName = "visits";
    
    public async Task<Visit> CreateAsync(Visit visit)
    {
        await indexedDbAccessor.SetValueAsync(VisitCollectionName, visit);
        return visit;
    }
    
    public async Task CreateManyAsync(Visit[] visits)
    {
        await indexedDbAccessor.SetAllValuesAsync(VisitCollectionName, visits);
    }

    public async Task<Visit> UpdateAsync(Visit visit)
    {
        return await CreateAsync(visit);
    }

    public async Task<Visit> GetByIdAsync(string id)
    {
        return await indexedDbAccessor.GetValueByIdAsync<Visit>(VisitCollectionName, id);
    }

    public async Task DeleteAsync(string id)
    {
        var visit = await indexedDbAccessor.GetValueByIdAsync<Visit>(VisitCollectionName, id);
        if (visit.SyncedAt is not null)
        {
            visit.Status = VisitStatus.Cancelled;
            await UpdateAsync(visit);
            return;
        }
        await indexedDbAccessor.DeleteAsync(VisitCollectionName, id);
    }

    public async Task<Visit[]> GetAllAsync()
    {
        return await indexedDbAccessor.GetValueAsync<Visit[]>(VisitCollectionName);
    }
}