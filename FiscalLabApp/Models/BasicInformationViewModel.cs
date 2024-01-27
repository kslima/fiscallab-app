namespace FiscalLabApp.Models;

public class BasicInformationViewModel
{
    public Plant Plant { get; set; } = null!;
    public Association Association { get; set; } = null!;
    public string Consultant { get; set; } = string.Empty;
    public string Inspector { get; set; } = string.Empty;
    public string Leader { get; set; } = string.Empty;
    public string LaboratoryLeader { get; set; } = string.Empty;
    public DateOnly VisitDate { get; set; }
    public TimeOnly VisitTime { get; set; }
    public DateTime CreatedAt { get; set; }
}