using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IMenuService
{
    public Task<Menu[]> GetAllAsync();
    public Task<MenuOption[]> GetOptions(PageType pageType);
    
}