using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IMenuService
{
    Task CreateManyAsync(Menu[] menus);
    Task<Menu> GetById(string id);
    Task<Menu[]> GetMenuOptions(PageType pageType);
    Task<Menu[]> GetAllAsync();
    Task UpdateAsync(Menu menu);
    Task RestoreAsync();
}