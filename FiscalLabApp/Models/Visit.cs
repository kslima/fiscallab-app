namespace FiscalLabApp.Models;

public class Visit
{
    //Main
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
    
    //Cane Scale
    public string InScale { get; set; } = string.Empty;
    public string OutScale { get; set; } = string.Empty;
    public string CargoDraw { get; set; } = string.Empty;
    public string ScaleObservations { get; set; } = string.Empty;
    public string Calibration1 { get; set; } = string.Empty;
    public string Calibration2 { get; set; } = string.Empty;
    public string ResponsibleBody { get; set; } = string.Empty;
    public string CalibrationCertificate { get; set; } = string.Empty;
    public string Observations1 { get; set; } = string.Empty;
    public string PlantPercentage { get; set; } = string.Empty;
    public string ProviderPercentage { get; set; } = string.Empty;
    public string PlantFarmPercentage { get; set; } = string.Empty;
    public string FarmProviderPercentage { get; set; } = string.Empty;
    public string Observations2 { get; set; } = string.Empty;
}