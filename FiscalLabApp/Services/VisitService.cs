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

    public async Task<Visit> UpdateAsync(Visit visit)
    {
        return await CreateAsync(visit);
    }

    public async Task<Visit> GetByIdAsync(string id)
    {
        return await indexedDbAccessor.GetValueByIdAsync<Visit>(VisitCollectionName, id);
    }

    public Task DeleteAsync(string id)
    {
        return indexedDbAccessor.DeleteAsync(VisitCollectionName, id);
    }

    public async Task<List<Visit>> GetAllAsync()
    {
        return await indexedDbAccessor.GetValueAsync<List<Visit>>(VisitCollectionName);
    }
}