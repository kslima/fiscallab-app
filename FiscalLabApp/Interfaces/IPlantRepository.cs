using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IPlantRepository
{
    Task<PlantModel> CreateAsync(PlantModel plantModel);
    Task<PlantModel> UpdateAsync(PlantModel plantModel);
    Task<PlantModel> GetAsync(string id);
    Task<List<PlantModel>> GetAllAsync();
}