using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IApiService
{
    Task<VisitPage[]> GetAllVisitPagesAsync();
    Task<Menu[]> GetAllMenusAsync();
    Task<Plant[]> GetAllPlantsAsync();
}