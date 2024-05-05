using FiscalLabApp.Exceptions;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class PlantService : IPlantService
{
    private readonly IndexedDbAccessor _indexedDbAccessor;
    private readonly IApiService _apiService;
    public PlantService(IndexedDbAccessor indexedDbAccessor, IApiService apiService)
    {
        _indexedDbAccessor = indexedDbAccessor;
        _apiService = apiService;
    }

    public async Task<Plant> CreateAsync(Plant plant)
    {
        var apiResponse = await _apiService.CreatePlantAsync(plant);
        if (apiResponse.IsFailure())
        {
            throw new ApiErrorException(apiResponse.Error);
        }
        
        plant = apiResponse.Data!;
        await _indexedDbAccessor.SetValueAsync(CollectionsHelper.PlantsCollection, plant);
        return plant;
    }
    
    public async Task<Plant> UpdateAsync(string id, Plant plant)
    {
        var apiResponse = await _apiService.UpdatePlantAsync(id, plant);
        if (apiResponse.IsFailure())
        {
            throw new ApiErrorException(apiResponse.Error);
        }
        
        plant = apiResponse.Data!;
        await _indexedDbAccessor.SetValueAsync(CollectionsHelper.PlantsCollection, plant);
        return plant;
    }
    
    public async Task CreateManyAsync(Plant[] plants)
    {
        await _indexedDbAccessor.SetAllValuesAsync(CollectionsHelper.PlantsCollection, plants);
    }

    public async Task<Plant> GetAsync(string id)
    {
        return await _indexedDbAccessor.GetValueByIdAsync<Plant>(CollectionsHelper.PlantsCollection, id);
    }

    public async Task<Plant[]> GetAllAsync()
    {
        return await _indexedDbAccessor.GetValueAsync<Plant[]>(CollectionsHelper.PlantsCollection);
    }
}