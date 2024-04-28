using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class AssociationService(IndexedDbAccessor indexedDbAccessor) : IAssociationService
{
    public async Task<Association> CreateAsync(Association association)
    {
        await indexedDbAccessor.SetValueAsync(CollectionsHelper.AssociationsCollection, association);
        return association;
    }
    
    public async Task CreateManyAsync(Association[] associations)
    {
        await indexedDbAccessor.SetAllValuesAsync(CollectionsHelper.AssociationsCollection, associations);
    }

    public Task<Association> UpdateAsync(Association association)
    {
        return CreateAsync(association);
    }

    public Task<Association> GetAsync(string id)
    {
        return indexedDbAccessor.GetValueByIdAsync<Association>(CollectionsHelper.AssociationsCollection, id);
    }

    public Task<Association[]> GetAllAsync()
    {
        return indexedDbAccessor.GetValueAsync<Association[]>(CollectionsHelper.AssociationsCollection);
    }
}