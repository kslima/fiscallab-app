namespace FiscalLabApp.Interfaces;

public interface ITab
{
    string GetName();
    int GetTotalItems();
    int GetPendingItems();
}