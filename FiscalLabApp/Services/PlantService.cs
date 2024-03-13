using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class PlantService(IndexedDbAccessor indexedDbAccessor) : IPlantService
{
    private const string PlantCollectionName = "plants";
    public async Task<Plant> CreateAsync(Plant plant)
    {
        await indexedDbAccessor.SetValueAsync(PlantCollectionName, plant);
        return plant;
    }
    
    public async Task CreateManyAsync(Plant[] plants)
    {
        await indexedDbAccessor.SetAllValuesAsync(PlantCollectionName, plants);
    }

    public async Task<Plant> UpdateAsync(Plant plant)
    {
        return await CreateAsync(plant);
    }

    public async Task<Plant> GetAsync(string id)
    {
        return await indexedDbAccessor.GetValueByIdAsync<Plant>(PlantCollectionName, id);
    }

    public async Task<Plant[]> GetAllAsync()
    {
        return await indexedDbAccessor.GetValueAsync<Plant[]>(PlantCollectionName);
    }
}