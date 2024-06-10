namespace FiscalLabApp.Models;

public class BasicInformation
{
    public Plant Plant { get; set; } = new();
    public Association Association { get; set; } = new();
    public string Consultant { get; set; } = string.Empty;
    public string Inspector { get; set; } = string.Empty;
    public string Leader { get; set; } = string.Empty;
    public string LaboratoryLeader { get; set; } = string.Empty;
    public DateOnly VisitDate { get; set; }
    public TimeOnly VisitTime { get; set; }
}