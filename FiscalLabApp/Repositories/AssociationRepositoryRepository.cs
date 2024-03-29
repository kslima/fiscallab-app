using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;

namespace FiscalLabApp.Repositories;

public class AssociationRepository(IndexedDbAccessor indexedDbAccessor) : IAssociationRepository
{
    private const string AssociationCollectionName = "associations";
    public async Task<PlantModel> CreateAsync(PlantModel plantModel)
    {
        await indexedDbAccessor.SetValueAsync(AssociationCollectionName, plantModel);
        return plantModel;
    }
    
    public async Task<AssociationModel> CreateAsync(AssociationModel model)
    {
        await indexedDbAccessor.SetValueAsync(AssociationCollectionName, model);
        return model;
    }

    public Task<AssociationModel> UpdateAsync(AssociationModel model)
    {
        return CreateAsync(model);
    }

    public Task<AssociationModel> GetAsync(string id)
    {
        return indexedDbAccessor.GetValueByIdAsync<AssociationModel>(AssociationCollectionName, id);
    }

    public Task<List<AssociationModel>> GetAllAsync()
    {
        return indexedDbAccessor.GetValueAsync<List<AssociationModel>>(AssociationCollectionName);
    }
}