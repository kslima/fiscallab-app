using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class MenuService(IndexedDbAccessor indexedDbAccessor) : IMenuService
{
    public Task<Menu[]> GetAllAsync()
    {
        return Task.FromResult(Array.Empty<Menu>());
    }

    public async Task<MenuOption[]> GetMenuOptions(PageType pageType)
    {
        var options = await indexedDbAccessor.GetValueAsync<MenuOption[]>(IndexedDbAccessor.OptionsCollectionName);
        return options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .ToArray();
    }

    public async Task<string[]> GetOptions(PageType pageType, MenuType menuType)
    {
        var options = await indexedDbAccessor.GetValueAsync<MenuOption[]>(IndexedDbAccessor.OptionsCollectionName);
        return options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .Where(p => p.Menu.Equals(menuType.ToString()))
            .SelectMany(p => p.Options)
            .ToArray();
    }

    public async Task<string[]> AddOptionAsync(PageType pageType, MenuType menuType, string option)
    {
        var options = await indexedDbAccessor.GetValueAsync<MenuOption[]>(IndexedDbAccessor.OptionsCollectionName);
        var menu = options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .Single(p => p.Menu.Equals(menuType.ToString()));
        
        menu.Options.Add(option);
        await indexedDbAccessor.SetValueAsync(IndexedDbAccessor.OptionsCollectionName, menu);
        return await GetOptions(pageType, menuType);
    }
    
    public async Task<string[]> RemoveOptionAsync(PageType pageType, MenuType menuType, string option)
    {
        var options = await indexedDbAccessor.GetValueAsync<MenuOption[]>(IndexedDbAccessor.OptionsCollectionName);
        var menu = options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .Single(p => p.Menu.Equals(menuType.ToString()));

        menu.Options = menu.Options
            .Where(o => !o.Equals(option))
            .ToList();
        
        await indexedDbAccessor.SetValueAsync(IndexedDbAccessor.OptionsCollectionName, menu);
        return await GetOptions(pageType, menuType);
    }
}