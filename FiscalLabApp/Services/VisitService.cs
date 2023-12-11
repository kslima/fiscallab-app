using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class VisitService(IVisitRepository visitRepository) : IVisitService
{
    public async Task<Visit> CreateAsync(Visit visit)
    {
        return await visitRepository.CreateAsync(visit);
    }

    public async Task<Visit> UpdateAsync(Visit visit)
    {
        return await visitRepository.UpdateAsync(visit);
    }

    public async Task<Visit> GetByIdAsync(string id)
    {
        return await visitRepository.GetByIdAsync(id);
    }

    public Task DeleteAsync(string id)
    {
        return visitRepository.DeleteAsync(id);
    }

    public async Task<List<Visit>> GetAllAsync()
    {
        return (await visitRepository.GetAllAsync())
            .OrderByDescending(v => v.CreatedAt)
            .ToList();
    }
}