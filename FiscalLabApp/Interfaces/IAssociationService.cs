using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IAssociationService
{
    Task<Association> CreateAsync(Association association);
    Task CreateManyAsync(Association[] associations);
    Task<Association> UpdateAsync(Association association);
    Task<Association> GetAsync(string id);
    Task<Association[]> GetAllAsync();
}