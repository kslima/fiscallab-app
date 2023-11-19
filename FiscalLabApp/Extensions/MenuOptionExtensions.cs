using FiscalLabApp.Models;

namespace FiscalLabApp.Extensions;

public static class MenuOptionExtensions
{
    public static MenuOption? GetOption(this MenuOption[] options, MenuType menuType)
    {
        return options.SingleOrDefault(o => o.Menu.Equals(menuType.ToString()));
    }
}