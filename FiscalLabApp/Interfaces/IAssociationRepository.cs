using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IAssociationRepository
{
    Task<AssociationViewModel> CreateAsync(AssociationViewModel viewModel);
    Task<AssociationViewModel> UpdateAsync(AssociationViewModel viewModel);
    Task<AssociationViewModel> GetAsync(string id);
    Task<List<AssociationViewModel>> GetAllAsync();
}