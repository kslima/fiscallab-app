using System.ComponentModel.DataAnnotations;
using FiscalLabApp.Enums;

namespace FiscalLabApp.Models;

public class Visit
{
    //Main
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public PlantModel Plant { get; set; } = new();
    [Required]
    public string Consultant { get; set; } = string.Empty;
    [Required]
    public string Association { get; set; } = string.Empty;
    public string Inspector { get; set; } = string.Empty;
    public string Leader { get; set; } = string.Empty;
    public string LaboratoryLeader { get; set; } = string.Empty;
    [Required]
    public DateOnly VisitDate { get; set; }
    [Required]
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
    
    
    //analytical balance
    public string HomogeneousWeight { get; set; } = string.Empty;
    public string FinalWeight { get; set; } = string.Empty;
    public string CalibratedBalance  { get; set; } = string.Empty;
    public string LeveledBalance  { get; set; } = string.Empty;
    public string CalibrationCertificateBalance  { get; set; } = string.Empty;
    public string Observations5 { get; set; } = string.Empty;
    public string AverageAmbientTemperature { get; set; } = string.Empty;
    public string Observations6 { get; set; } = string.Empty;
    
    //press refractometer
    public string PressureGaugeCertificated { get; set; } = string.Empty;
    public string DiscardCup { get; set; } = string.Empty;
    public string CollectorBottle { get; set; } = string.Empty;
    public string Pressure { get; set; } = string.Empty;
    public string Timer { get; set; } = string.Empty;
    public string PressCleaning { get; set; } = string.Empty;
    public string Observations7 { get; set; } = string.Empty;
    public string BrothHomogenization { get; set; } = string.Empty;
    public string RefractometerCalibrationCertificate { get; set; } = string.Empty;
    public string PrecisionReading { get; set; } = string.Empty;
    public string RefractometerBenchmarking { get; set; } = string.Empty;
    public string RefractometerCleaning { get; set; } = string.Empty;
    public string InternalTemperature { get; set; } = string.Empty;
    public string Observations8 { get; set; } = string.Empty;
    
    //clarification saccharimeter
    public string ClarificationSaccharimeterBottle { get; set; } = string.Empty;
    public string ClarificationSaccharimeterAgitation { get; set; } = string.Empty;
    public string ClarificationSaccharimeterHasDilution { get; set; } = string.Empty;
    public string ClarificationSaccharimeterClarifier { get; set; } = string.Empty;
    public string ClarificationSaccharimeterPressure { get; set; } = string.Empty;
    public string ClarificationSaccharimeterClarifierAmount { get; set; } = string.Empty;
    public string ClarificationSaccharimeterBottleClarifiedVolume { get; set; } = string.Empty;
    public string ClarificationSaccharimeterBottleAfterClarifiedVolume { get; set; } = string.Empty;
    public string ClarificationSaccharimeterObservations9 { get; set; } = string.Empty;
    public string ClarificationSaccharimeterStabilization { get; set; } = string.Empty;
    public string ClarificationSaccharimeterBenchmarking { get; set; } = string.Empty;
    public string ClarificationSaccharimeterQuartzPattern { get; set; } = string.Empty;
    public string ClarificationSaccharimeterQuartzResult { get; set; } = string.Empty;
    public string ClarificationSaccharimeterQuartzReading { get; set; } = string.Empty;
    public string ClarificationSaccharimeterCalibrationCertificate { get; set; } = string.Empty;
    public string ClarificationSaccharimeterTubeCleaning { get; set; } = string.Empty;
    public string ClarificationSaccharimeterClearCollingCooler { get; set; } = string.Empty;
    public string ClarificationSaccharimeterObservations10 { get; set; } = string.Empty;
    
    //benchmarking equipment
    public string BenchmarkingEquipmentLoadCell { get; set; } = string.Empty;
    public string BenchmarkingEquipmentThermometer { get; set; } = string.Empty;
    public string BenchmarkingEquipmentTachometer { get; set; } = string.Empty;
    public string BenchmarkingEquipmentPachymeter { get; set; } = string.Empty;
    public string BenchmarkingEquipment500Gm { get; set; } = string.Empty;
    public string BenchmarkingEquipment100Gm { get; set; } = string.Empty;
    public string BenchmarkingEquipment1Gm { get; set; } = string.Empty;
    public string BenchmarkingEquipmentSucroseTest { get; set; } = string.Empty;
    public string BenchmarkingEquipmentRange10 { get; set; } = string.Empty;
    public string BenchmarkingEquipmentRange20 { get; set; } = string.Empty;
    public string BenchmarkingEquipmentRange30 { get; set; } = string.Empty;
    public string BenchmarkingEquipmentZ25 { get; set; } = string.Empty;
    public string BenchmarkingEquipmentZ50 { get; set; } = string.Empty;
    public string BenchmarkingEquipmentZ75 { get; set; } = string.Empty;
    public string BenchmarkingEquipmentZ100 { get; set; } = string.Empty;
    public string BenchmarkingEquipmentObservations11 { get; set; } = string.Empty;
    public decimal BenchmarkingEquipmentExpectedCrop { get; set; }
    public decimal BenchmarkingEquipmentAccomplishedCrop { get; set; }
    public decimal BenchmarkingEquipmentPreviousCrop { get; set; }
    public decimal BenchmarkingEquipmentPercentageRealized { get; set; }
    public decimal BenchmarkingEquipmentVariationBetweenCrops  { get; set; }
    public decimal BenchmarkingEquipmentCurrentFiber { get; set; }
    public decimal BenchmarkingEquipmentPreviousFiber { get; set; }
    public decimal BenchmarkingEquipmentFiberVariation { get; set; }
    public decimal BenchmarkingEquipmentCurrentAtr { get; set; }
    public decimal BenchmarkingEquipmentPreviousAtr { get; set; }
    public decimal BenchmarkingEquipmentAtrVariation { get; set; }
    public string BenchmarkingEquipmentObservations12 { get; set; } = string.Empty;
    
    //system-consistency
    public string SystemConsistencyOc { get; set; } = string.Empty;
    public string SystemConsistencyFarm { get; set; } = string.Empty;
    public string SystemConsistencyOwner { get; set; } = string.Empty;
    public Clarify SystemConsistencyClarifier { get; set; }
    public decimal SystemConsistencyPlantPbu { get; set; }
    public decimal SystemConsistencyConsecanaPbu { get; set; }
    public decimal SystemConsistencyDifferencePbu { get; set; }
    public decimal SystemConsistencyPlantPurity { get; set; }
    public decimal SystemConsistencyConsecanaPurity { get; set; }
    public decimal SystemConsistencyDifferencePurity { get; set; }
    public decimal SystemConsistencyPlantPol { get; set; }
    public decimal SystemConsistencyConsecanaPol { get; set; }
    public decimal SystemConsistencyDifferencePol { get; set; }
    public decimal SystemConsistencyPlantFiber { get; set; }
    public decimal SystemConsistencyConsecanaFiber { get; set; }
    public decimal SystemConsistencyDifferenceFiber { get; set; }
    public decimal SystemConsistencyPlantPcc { get; set; }
    public decimal SystemConsistencyConsecanaPcc { get; set; }
    public decimal SystemConsistencyDifferencePcc { get; set; }
    public decimal SystemConsistencyPlantAr { get; set; }
    public decimal SystemConsistencyConsecanaAr { get; set; }
    public decimal SystemConsistencyDifferenceAr { get; set; }
    public decimal SystemConsistencyPlantAtr { get; set; }
    public decimal SystemConsistencyConsecanaAtr { get; set; }
    public decimal SystemConsistencyDifferenceAtr { get; set; }
    public string SystemConsistencyObservations { get; set; } = string.Empty;
    
    //conclusion
    public string ConclusionInspectorPerformance { get; set; } = string.Empty;
    public string ConclusionLaboratoryReceptivity { get; set; } = string.Empty;
    public string ConclusionPendencies { get; set; } = string.Empty;
    public string ConclusionObservations { get; set; } = string.Empty;
}