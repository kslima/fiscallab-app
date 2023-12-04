using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IMenuService
{
    Task<Menu[]> GetAllAsync();
    Task<MenuOption[]> GetMenuOptions(PageType pageType);
    Task<string[]> GetOptions(PageType pageType, MenuType menuType);
    Task<string[]> AddOptionAsync(PageType pageType, MenuType menuType, string option);
    Task<string[]> RemoveOptionAsync(PageType pageType, MenuType menuType, string option);
}