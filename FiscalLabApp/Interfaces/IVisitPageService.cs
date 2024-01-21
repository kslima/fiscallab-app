using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IVisitPageService
{
    Task<List<VisitPage>> GetAllAsync();
}