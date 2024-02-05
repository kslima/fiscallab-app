namespace FiscalLabApp.Models;

public class VisitPage
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int TotalItems { get; set; }
    public int PendingItems { get; set; }

    public string GetFullName()
    {
        return $"{DisplayName} - {TotalItems}";
    }
}