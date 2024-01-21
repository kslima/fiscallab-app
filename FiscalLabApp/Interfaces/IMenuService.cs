using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IMenuService
{
    Task<Menu> GetById(string id);
    Task<Menu[]> GetMenuOptions(PageType pageType);
    Task SetOptionAsync(string code, List<Option> options);
}