using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IAssociationRepository
{
    Task<AssociationModel> CreateAsync(AssociationModel model);
    Task<AssociationModel> UpdateAsync(AssociationModel model);
    Task<AssociationModel> GetAsync(string id);
    Task<List<AssociationModel>> GetAllAsync();
}