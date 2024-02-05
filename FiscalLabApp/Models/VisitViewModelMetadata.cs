namespace FiscalLabApp.Models;

public class VisitViewModelMetadata
{
    public List<VisitPage> Pages { get; set; } = [];
    public int TotalItems { get; set; }
    public int PendingItems { get; set; }
}