using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;

namespace FiscalLabApp.Repositories;

public class PlantRepository(IndexedDbAccessor indexedDbAccessor) : IPlantRepository
{
    private const string PlantCollectionName = "plants";
    public async Task<PlantModel> CreateAsync(PlantModel plantModel)
    {
        await indexedDbAccessor.SetValueAsync(PlantCollectionName, plantModel);
        return plantModel;
    }

    public async Task<PlantModel> UpdateAsync(PlantModel plantModel)
    {
        return await CreateAsync(plantModel);
    }

    public async Task<PlantModel> GetAsync(string id)
    {
        return await indexedDbAccessor.GetValueByIdAsync<PlantModel>(PlantCollectionName, id);
    }

    public async Task<List<PlantModel>> GetAllAsync()
    {
        return await indexedDbAccessor.GetValueAsync<List<PlantModel>>(PlantCollectionName);
    }
}