using FiscalLabApp.Models;

namespace FiscalLabApp.Extensions;

public static class MenuOptionExtensions
{
    public static Menu? GetMenu(this Menu[] options, MenuType menuType)
    {
        return options.SingleOrDefault(o => o.Code.Equals(menuType.ToString()));
    }
}