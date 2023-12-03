using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class MenuService(IndexedDbAccessor indexedDbAccessor) : IMenuService
{
    public Task<Menu[]> GetAllAsync()
    {
        return Task.FromResult(Array.Empty<Menu>());
    }

    public async Task<MenuOption[]> GetOptions(PageType pageType)
    {
        var options = await indexedDbAccessor.GetValueAsync<MenuOption[]>("options");
        return options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .ToArray();
    }
}