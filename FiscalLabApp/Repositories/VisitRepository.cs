using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;

namespace FiscalLabApp.Repositories;

public class VisitRepository : IVisitRepository
{
    private const string VisitCollectionName = "visits";
    private readonly IndexedDbAccessor _indexedDbAccessor;

    public VisitRepository(IndexedDbAccessor indexedDbAccessor)
    {
        _indexedDbAccessor = indexedDbAccessor;
    }

    public async Task<Visit> CreateAsync(Visit visit)
    {
        await _indexedDbAccessor.SetValueAsync(VisitCollectionName, visit);
        return visit;
    }

    public async Task<Visit> UpdateAsync(Visit visit)
    {
        await _indexedDbAccessor.SetValueAsync(VisitCollectionName, visit);
        return visit;
    }

    public async Task<Visit> GetByIdAsync(string id)
    {
        return await _indexedDbAccessor.GetValueByKeyAsync<Visit>(VisitCollectionName, id);
    }

    public async Task<List<Visit>> GetAllAsync()
    {
        // return new List<Visit>
        // {
        //     new()
        //     {
        //         CreatedAt = DateTime.Now,
        //         Plant = "Usina Coruripe Acucar e Alcool"
        //     },
        //     new()
        //     {
        //         CreatedAt = DateTime.Now,
        //         Plant = "Usina Coruripe Acucar e Alcool"
        //     },
        //     new()
        //     {
        //         CreatedAt = DateTime.Now,
        //         Plant = "Usina Coruripe Acucar e Alcool"
        //     }
        // };
        return await _indexedDbAccessor.GetValueAsync<List<Visit>>(VisitCollectionName);
    }
}