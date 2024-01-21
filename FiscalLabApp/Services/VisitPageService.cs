using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class VisitPageService(IndexedDbAccessor indexedDbAccessor) : IVisitPageService
{
    public async Task<List<VisitPage>> GetAllAsync()
    {
        return await indexedDbAccessor.GetValueAsync<List<VisitPage>>(IndexedDbAccessor.VisitPageCollectionName);
    }
}