namespace FiscalLabApp.Models;

public class VisitImage
{
    public string VisitId { get; set; } = string.Empty;
    public List<Image> Images { get; set; } = [];
}