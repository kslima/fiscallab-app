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
    CaneScalePageInScale,
    CaneScalePageOutScale,
    CaneScalePageCargoDraw,
    CaneScalePageScaleObservations,
    CaneScalePageCalibration1,
    CaneScalePageCalibration2,
    CaneScalePageResponsibleBody,
    CaneScalePageCalibrationCertificate,
    CaneScalePageObservations1,
    CaneScalePagePlantPercentage,
    CaneScalePageProviderPercentage,
    CaneScalePagePlantFarmPercentage,
    CaneScalePageFarmProviderPercentage,
    CaneScalePageObservations2,
}