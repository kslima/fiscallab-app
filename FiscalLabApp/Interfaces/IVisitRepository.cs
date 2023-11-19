using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IVisitRepository
{
    Task<Visit> CreateAsync(Visit visit);
    Task<Visit> UpdateAsync(Visit visit);
    Task<Visit> GetByIdAsync(string id);
    Task<List<Visit>> GetAllAsync();
}