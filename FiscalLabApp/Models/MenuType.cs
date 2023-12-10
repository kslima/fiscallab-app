namespace FiscalLabApp.Models;

public enum MenuType
{
    //main page
    MainPageConsultant,
    MainPageAssociation,
    MainPagePlant,
    MainPageInspector,
    MainPageLeader,
    MainPageLaboratoryLeader,

    //cane scale page
    CaneBalancePageInScale,
    CaneBalancePageOutScale,
    CaneBalancePageCargoDraw,
    CaneBalancePageScaleObservations,
    CaneBalancePageCalibration1,
    CaneBalancePageCalibration2,
    CaneBalancePageResponsibleBody,
    CaneBalancePageCalibrationCertificate,
    CaneBalancePageObservations1,
    CaneBalancePagePlantPercentage,
    CaneBalancePageProviderPercentage,
    CaneBalancePagePlantFarmPercentage,
    CaneBalancePageFarmProviderPercentage,
    CaneBalancePageObservations2,

    //desintegrator probe page
    DesintegratorProbePageProbeType,
    DesintegratorProbePageTubeInserted,
    DesintegratorProbePageTubeDischarged,
    DesintegratorProbePageCollect,
    DesintegratorProbePageSampleAmount,
    DesintegratorProbePageBrothExtraction,
    DesintegratorProbePageLoadPosition,
    DesintegratorProbePageToothedCrown,
    DesintegratorProbePageToothedCrownType,
    DesintegratorProbePageObservations3,
    
    DesintegratorProbePageHomogeneousSamples,
    DesintegratorProbePageKnifeConservation,
    DesintegratorProbePageAgainstKnifeConservation,
    DesintegratorProbePageKnifeAgainstKnifeDistance,
    DesintegratorProbePageHammerConservation,
    DesintegratorProbePageCleanMixer,
    DesintegratorProbePageDesintegratorRpm,
    DesintegratorProbePagePreparationIndex,
    DesintegratorProbePageObservations4,

    //analytical balance
    AnalyticalBalanceHomogeneousWeight,
    AnalyticalBalanceFinalWeight,
    AnalyticalBalanceCalibratedBalance,
    AnalyticalBalanceLeveledBalance,
    AnalyticalBalanceCalibrationCertificateBalance,
    AnalyticalBalanceObservations5,
    AnalyticalBalanceAverageAmbientTemperature,
    AnalyticalBalanceObservations6,

    //press refractometer
    PressRefractometerPressureGaugeCertificated,
    PressRefractometerDiscardCup,
    PressRefractometerCollectorBottle,
    PressRefractometerPressure,
    PressRefractometerTimer,
    PressRefractometerPressCleaning,
    PressRefractometerObservations7,
    PressRefractometerBrothHomogenization,
    PressRefractometerRefractometerCalibrationCertificate,
    PressRefractometerPrecisionReading,
    PressRefractometerRefractometerBenchmarking,
    PressRefractometerRefractometerCleaning,
    PressRefractometerInternalTemperature,
    PressRefractometerObservations8,

    //clarification saccharimeter
    ClarificationSaccharimeterBottle,
    ClarificationSaccharimeterAgitation,
    ClarificationSaccharimeterHasDilution,
    ClarificationSaccharimeterClarifier,
    ClarificationSaccharimeterPressure,
    ClarificationSaccharimeterClarifierAmount,
    ClarificationSaccharimeterBottleClarifiedVolume,
    ClarificationSaccharimeterBottleAfterClarifiedVolume,
    ClarificationSaccharimeterObservations9,
    
    ClarificationSaccharimeterStabilization,
    ClarificationSaccharimeterBenchmarking,
    ClarificationSaccharimeterQuartzPattern,
    ClarificationSaccharimeterQuartzResult,
    ClarificationSaccharimeterQuartzReading,
    ClarificationSaccharimeterCalibrationCertificate,
    ClarificationSaccharimeterTubeCleaning,
    ClarificationSaccharimeterClearCollingCooler,
    ClarificationSaccharimeterObservations10,

    //benchmarking equipment
    BenchmarkingEquipmentLoadCell,
    BenchmarkingEquipmentThermometer,
    BenchmarkingEquipmentTachometer,
    BenchmarkingEquipmentPachymeter,
    BenchmarkingEquipment500Gm,
    BenchmarkingEquipment100Gm,
    BenchmarkingEquipment1Gm,
    BenchmarkingEquipmentSucroseTest,
    BenchmarkingEquipmentRange10,
    BenchmarkingEquipmentRange20,
    BenchmarkingEquipmentRange30,
    BenchmarkingEquipmentZ25,
    BenchmarkingEquipmentZ50,
    BenchmarkingEquipmentZ75,
    BenchmarkingEquipmentZ100,
    BenchmarkingEquipmentObservations11,
    BenchmarkingEquipmentObservations12,
    
    //system consistency
    SystemConsistencyObservations,
    
    //conclusion
    ConclusionInspectorPerformance,
    ConclusionLaboratoryPerformance,
    ConclusionPendencies,
    ConclusionObservations
}