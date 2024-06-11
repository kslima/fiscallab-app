using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IAssociationService
{
    Task<Association> CreateAsync(Association association);
    Task CreateManyAsync(Association[] associations);
    Task<Association> UpdateAsync(string id, Association association);
    Task<Association> GetAsync(string id);
    Task<bool> DeleteAsync(string id);
    Task<Association[]> ListAllLocalAsync();
    Task RestoreAsync();
}