using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IApiService
{
    Task<List<VisitPage>> GetAllVisitPagesAsync();
}