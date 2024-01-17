using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IVisitService
{
    Task<Visit> CreateAsync(Visit visit);
    Task<Visit> UpdateAsync(Visit visit);
    Task<Visit> GetByIdAsync(string id);
    Task DeleteAsync(string id);
    Task<List<Visit>> GetAllAsync();
}