using FiscalLabApp.Models;

namespace FiscalLabApp.Extensions;

public static class MenuOptionExtensions
{
    public static Menu? GetMenu(this Menu[] menus, MenuType menuType)
    {
        return menus.SingleOrDefault(o => o.Name.Equals(menuType.ToString()));
    }
}