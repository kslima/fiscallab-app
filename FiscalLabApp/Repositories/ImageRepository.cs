using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Services;

namespace FiscalLabApp.Repositories;

public class ImageRepository(IndexedDbAccessor indexedDbAccessor) : IImageRepository
{
    private const string VisitImageCollectionName = "visit_images";
    public async Task<VisitImage> AddAsync(VisitImage visitImage)
    {
        await indexedDbAccessor.SetValueAsync(VisitImageCollectionName, visitImage);
        return visitImage;
    }

    public Task<VisitImage?> GetByVisitIdAsync(string visitId)
    {
        return indexedDbAccessor.GetValueByIdAsync<VisitImage?>(VisitImageCollectionName, visitId);
    }
}