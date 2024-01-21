using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class MenuService(IndexedDbAccessor indexedDbAccessor) : IMenuService
{
    public Task<Menu> GetById(string id)
    {
        return indexedDbAccessor.GetValueByIdAsync<Menu>(IndexedDbAccessor.MenuCollectionName, id);
    }

    public async Task<Menu[]> GetMenuOptions(PageType pageType)
    {
        var options = await indexedDbAccessor.GetValueAsync<Menu[]>(IndexedDbAccessor.MenuCollectionName);
        return options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .ToArray();
    }
    
    public async Task SetOptionAsync(string code, List<Option> options)
    {
        var menu = await GetById(code);
        menu.Options = options;
        await indexedDbAccessor.SetValueAsync(IndexedDbAccessor.MenuCollectionName, menu);
    }
}