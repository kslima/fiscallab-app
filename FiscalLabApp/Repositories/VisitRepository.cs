using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;

namespace FiscalLabApp.Repositories;

public class VisitRepository(IndexedDbAccessor indexedDbAccessor) : IVisitRepository
{
    private const string VisitCollectionName = "visits";

    public async Task<Visit> CreateAsync(Visit visit)
    {
        await indexedDbAccessor.SetValueAsync(VisitCollectionName, visit);
        return visit;
    }

    public async Task<Visit> UpdateAsync(Visit visit)
    {
        await indexedDbAccessor.SetValueAsync(VisitCollectionName, visit);
        return visit;
    }

    public async Task<Visit> GetByIdAsync(string id)
    {
        return await indexedDbAccessor.GetValueByKeyAsync<Visit>(VisitCollectionName, id);
    }

    public async Task<List<Visit>> GetAllAsync()
    {
        return await indexedDbAccessor.GetValueAsync<List<Visit>>(VisitCollectionName);
    }
}