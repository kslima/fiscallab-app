namespace FiscalLabApp.Models;

public class Visit
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Plant { get; set; } = string.Empty;
    public string Consultant { get; set; } = string.Empty;
    public string Association { get; set; } = string.Empty;
    public string Inspector { get; set; } = string.Empty;
    public string Leader { get; set; } = string.Empty;
    public string LaboratoryLeader { get; set; } = string.Empty;
    public DateOnly VisitDate { get; set; }
    public TimeOnly VisitTime { get; set; }
    public DateTime CreatedAt { get; set; }
}