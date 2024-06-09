using FiscalLabApp.Exceptions;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class AssociationService : IAssociationService
{
    private readonly IndexedDbAccessor _indexedDbAccessor;
    private readonly IApiService _apiService;

    public AssociationService(
        IndexedDbAccessor indexedDbAccessor,
        IApiService apiService)
    {
        _indexedDbAccessor = indexedDbAccessor;
        _apiService = apiService;
    }

    public async Task<Association> CreateAsync(Association association)
    {
        var apiResponse = await _apiService.CreateAssociationAsync(association);
        if (apiResponse.IsFailure())
        {
            throw new ApiErrorException(apiResponse.Error);
        }
        
        association = apiResponse.Data!;
        await _indexedDbAccessor.SetValueAsync(CollectionsHelper.AssociationsCollection, association);
        return association;
    }
    
    public async Task<Association> UpdateAsync(string id, Association association)
    {
        var apiResponse = await _apiService.UpdateAssociationAsync(id, association);
        if (apiResponse.IsFailure())
        {
            throw new ApiErrorException(apiResponse.Error);
        }

        association = apiResponse.Data!;
        await _indexedDbAccessor.SetValueAsync(CollectionsHelper.AssociationsCollection, association);
        return association;
    }
    
    public async Task CreateManyAsync(Association[] associations)
    {
        await _indexedDbAccessor.SetAllValuesAsync(CollectionsHelper.AssociationsCollection, associations);
    }
    
    public Task<Association> GetAsync(string id)
    {
        return _indexedDbAccessor.GetValueByIdAsync<Association>(CollectionsHelper.AssociationsCollection, id);
    }

    public async Task<Association[]> GetAllAsync()
    {
        var associations =  await _indexedDbAccessor.GetValueAsync<Association[]>(CollectionsHelper.AssociationsCollection);
        return associations
            .OrderBy(p => p.Name)
            .ToArray();
    }

    public async Task RestoreAsync()
    {
        var associations = await _apiService.ListAssociationsAsync();
        await CreateManyAsync(associations);
    }
}