using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IPlantService
{
    Task<Plant> CreateAsync(Plant plant);
    Task CreateManyAsync(Plant[] plants);
    Task<Plant> UpdateAsync(Plant plant);
    Task<Plant> GetAsync(string id);
    Task<Plant[]> GetAllAsync();
}