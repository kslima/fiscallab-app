using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;

namespace FiscalLabApp.Repositories;

public class AssociationRepository(IndexedDbAccessor indexedDbAccessor) : IAssociationRepository
{
    private const string AssociationCollectionName = "associations";
    public async Task<PlantViewModel> CreateAsync(PlantViewModel plantViewModel)
    {
        await indexedDbAccessor.SetValueAsync(AssociationCollectionName, plantViewModel);
        return plantViewModel;
    }
    
    public async Task<AssociationViewModel> CreateAsync(AssociationViewModel viewModel)
    {
        await indexedDbAccessor.SetValueAsync(AssociationCollectionName, viewModel);
        return viewModel;
    }

    public Task<AssociationViewModel> UpdateAsync(AssociationViewModel viewModel)
    {
        return CreateAsync(viewModel);
    }

    public Task<AssociationViewModel> GetAsync(string id)
    {
        return indexedDbAccessor.GetValueByIdAsync<AssociationViewModel>(AssociationCollectionName, id);
    }

    public Task<List<AssociationViewModel>> GetAllAsync()
    {
        return indexedDbAccessor.GetValueAsync<List<AssociationViewModel>>(AssociationCollectionName);
    }
}