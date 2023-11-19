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
    
    
    //Desintegrador Probe
    public string ProbeTYpe { get; set; } = string.Empty;
    public string TubeInserted { get; set; } = string.Empty;
    public string TubeDischarged { get; set; } = string.Empty;
    public string Collect { get; set; } = string.Empty;
    public string SampleAmount { get; set; } = string.Empty;
    public string BrothExtraction { get; set; } = string.Empty;
    public string LoadPosition { get; set; } = string.Empty;
    public string ToothedCrown { get; set; } = string.Empty;
    public string ToothedCrownType { get; set; } = string.Empty;
    public string LastCrownChange { get; set; } = string.Empty;
    public string Observations3 { get; set; } = string.Empty;
    public string HomogeneousSamples { get; set; } = string.Empty;
    public string KnifeConservation { get; set; } = string.Empty;
    public string AgainstKnifeConservation { get; set; } = string.Empty;
    public string KnifeAgainstKnifeDistance { get; set; } = string.Empty;
    public string HammerConservation  { get; set; } = string.Empty;
    public string CleanMixer  { get; set; } = string.Empty;
    public string DesintegratorRpm  { get; set; } = string.Empty;
    public string PreparationIndex  { get; set; } = string.Empty;
    public string LastRazorChange  { get; set; } = string.Empty;
    public string LastHammerChange  { get; set; } = string.Empty;
    public string LastAgainstKnifeChange  { get; set; } = string.Empty;
    public string Observations4 { get; set; } = string.Empty;
}