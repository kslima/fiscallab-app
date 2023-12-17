using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IImageRepository
{
    Task<VisitImage> AddAsync(VisitImage visitImage);
    Task<VisitImage?> GetByVisitIdAsync(string visitId);
}