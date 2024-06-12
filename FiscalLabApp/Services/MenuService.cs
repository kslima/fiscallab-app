using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class MenuService : IMenuService
{
    private readonly IndexedDbAccessor _indexedDbAccessor;
    private readonly IApiService _apiService;

    public MenuService(
        IndexedDbAccessor indexedDbAccessor,
        IApiService apiService)
    {
        _indexedDbAccessor = indexedDbAccessor;
        _apiService = apiService;
    }

    public async Task CreateManyAsync(Menu[] menus)
    {
        await _indexedDbAccessor.SetAllValuesAsync(CollectionsHelper.MenusCollection, menus);
    }
    
    public Task<Menu> GetById(string id)
    {
        return _indexedDbAccessor.GetValueByIdAsync<Menu>(CollectionsHelper.MenusCollection, id);
    }

    public async Task<Menu[]> GetMenuOptions(PageType pageType)
    {
        var options = await _indexedDbAccessor.GetValueAsync<Menu[]>(CollectionsHelper.MenusCollection);
        return options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .ToArray();
    }

    public async Task<Menu[]> GetAllAsync()
    {
        return await _indexedDbAccessor.GetValueAsync<Menu[]>(CollectionsHelper.MenusCollection);
    }

    public async Task UpdateAsync(Menu menu)
    {
        await _indexedDbAccessor.SetValueAsync(CollectionsHelper.MenusCollection, menu);
    }
    
    public async Task RestoreAsync()
    {
        var menus = await _apiService.ListOptionsAsync();
        await CreateManyAsync(menus);
    }
}