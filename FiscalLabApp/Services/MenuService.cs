using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class MenuService(IndexedDbAccessor indexedDbAccessor) : IMenuService
{
    public async Task CreateManyAsync(Menu[] menus)
    {
        await indexedDbAccessor.SetAllValuesAsync(CollectionsHelper.MenusCollection, menus);
    }
    
    public Task<Menu> GetById(string id)
    {
        return indexedDbAccessor.GetValueByIdAsync<Menu>(CollectionsHelper.MenusCollection, id);
    }

    public async Task<Menu[]> GetMenuOptions(PageType pageType)
    {
        var options = await indexedDbAccessor.GetValueAsync<Menu[]>(CollectionsHelper.MenusCollection);
        return options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .ToArray();
    }

    public async Task<Menu[]> GetAllAsync()
    {
        return await indexedDbAccessor.GetValueAsync<Menu[]>(CollectionsHelper.MenusCollection);
    }

    public async Task SetOptionAsync(string code, List<Option> options)
    {
        var menu = await GetById(code);
        menu.Options = options;
        await indexedDbAccessor.SetValueAsync(CollectionsHelper.MenusCollection, menu);
    }
}