using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class AssociationService(IndexedDbAccessor indexedDbAccessor) : IAssociationService
{
    private const string AssociationCollectionName = "associations";
    
    public async Task<Association> CreateAsync(Association association)
    {
        await indexedDbAccessor.SetValueAsync(AssociationCollectionName, association);
        return association;
    }

    public Task<Association> UpdateAsync(Association association)
    {
        return CreateAsync(association);
    }

    public Task<Association> GetAsync(string id)
    {
        return indexedDbAccessor.GetValueByIdAsync<Association>(AssociationCollectionName, id);
    }

    public Task<Association[]> GetAllAsync()
    {
        return indexedDbAccessor.GetValueAsync<Association[]>(AssociationCollectionName);
    }
}