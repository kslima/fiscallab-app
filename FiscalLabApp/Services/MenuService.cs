using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class MenuService(IndexedDbAccessor indexedDbAccessor) : IMenuService
{
    public Task<Menu> GetByCode(string id)
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

    private async Task<string[]> GetOptions(PageType pageType, MenuType menuType)
    {
        var options = await indexedDbAccessor.GetValueAsync<Menu[]>(IndexedDbAccessor.MenuCollectionName);
        return options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .Where(p => p.Code.Equals(menuType.ToString()))
            .SelectMany(p => p.Options)
            .ToArray();
    }

    public async Task<string[]> AddOptionAsync(PageType pageType, MenuType menuType, string option)
    {
        var options = await indexedDbAccessor.GetValueAsync<Menu[]>(IndexedDbAccessor.MenuCollectionName);
        var menu = options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .Single(p => p.Code.Equals(menuType.ToString()));
        
        menu.Options.Add(option);
        await indexedDbAccessor.SetValueAsync(IndexedDbAccessor.MenuCollectionName, menu);
        return await GetOptions(pageType, menuType);
    }

    public async Task SetOptionAsync(string code, List<string> options)
    {
        var menu = await GetByCode(code);
        menu.Options = options;
        await indexedDbAccessor.SetValueAsync(IndexedDbAccessor.MenuCollectionName, menu);
    }
}