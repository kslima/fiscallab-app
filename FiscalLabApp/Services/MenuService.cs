using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class MenuService(IndexedDbAccessor indexedDbAccessor) : IMenuService
{
    public async Task<Menu[]> GetAllAsync()
    {
        return Array.Empty<Menu>();
    }

    public async Task<MenuOption[]> GetOptions(PageType pageType)
    {
        var options = await indexedDbAccessor.GetValueAsync<MenuOption[]>("options");
        return options
            .Where(p => p.Page.Equals(pageType.ToString()))
            .ToArray();
    }
}