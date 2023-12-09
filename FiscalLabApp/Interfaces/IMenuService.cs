using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IMenuService
{
    Task<Menu> GetByCode(string id);
    Task<Menu[]> GetMenuOptions(PageType pageType);
    Task<string[]> AddOptionAsync(PageType pageType, MenuType menuType, string option);
    Task SetOptionAsync(string code, List<string> options);
}