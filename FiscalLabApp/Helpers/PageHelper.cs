using FiscalLabApp.Models;

namespace FiscalLabApp.Helpers;

public static class PageHelper
{
    public const string BasicInformationPageName = "main";
    public const string SugarcaneBalancePageName = "sugarcane-balance";
    public const string DesintegratorProbePageName = "desintegrator-probe";
    public const string AnalyticalBalancePageName = "analytical-balance";
    public const string PressRefractometerPageName = "press-refractometer";
    public const string ClarificationSaccharimeterPageName = "clarification-saccharimeter";
    public const string BenchmarkingEquipmentPageName = "benchmarking-equipment";
    public const string SystemConsistencyPageName = "system-consistency";
    public const string ConclusionPageName = "conclusion";
    
    public const string BasicInformationPageTitle = "main";
    public const string SugarcaneBalancePageNTitle  = "sugarcane-balance";
    public const string DesintegratorProbePageTitle  = "desintegrator-probe";
    public const string AnalyticalBalancePageTitle  = "analytical-balance";
    public const string PressRefractometerPageTitle  = "press-refractometer";
    public const string ClarificationSaccharimeterPagTitle  = "clarification-saccharimeter";
    public const string BenchmarkingEquipmentPageTitle  = "benchmarking-equipment";
    public const string SystemConsistencyPageTitle  = "system-consistency";
    public const string ConclusionPageTitle  = "conclusion";

    
    public static VisitPage[] GetPages()
    {
        return
        [
            new VisitPage
            {
                Id = 1,
                Name = BasicInformationPageName,
                DisplayName = "Principal",
                TotalItems = 100,
                PendingItems = 55
            },
            new VisitPage
            {
                Id = 2,
                Name = SugarcaneBalancePageName,
                DisplayName = "Balança de Cana",
                TotalItems = 88,
                PendingItems = 60
            },
            new VisitPage
            {
                Id = 3,
                Name = DesintegratorProbePageName,
                DisplayName = "Sonda/Desintegrador",
                TotalItems = 88,
                PendingItems = 60
            },
            new VisitPage
            {
                Id = 4,
                Name = AnalyticalBalancePageName,
                DisplayName = "Balança Analítica/Temperatura",
                TotalItems = 88,
                PendingItems = 60
            },
            new VisitPage
            {
                Id = 5,
                Name = PressRefractometerPageName,
                DisplayName = "Prensa/Refratômetro",
                TotalItems = 88,
                PendingItems = 60
            },
            new VisitPage
            {
                Id = 6,
                Name = ClarificationSaccharimeterPageName,
                DisplayName = "Clarificação/Sacarímetro",
                TotalItems = 88,
                PendingItems = 60
            },
            new VisitPage
            {
                Id = 7,
                Name = BenchmarkingEquipmentPageName,
                DisplayName = "Equipamentos de Aferição/Medias",
                TotalItems = 88,
                PendingItems = 60
            },
            new VisitPage
            {
                Id = 8,
                Name = SystemConsistencyPageName,
                DisplayName = "Consistência do Sistema",
                TotalItems = 88,
                PendingItems = 60
            },
            new VisitPage
            {
                Id = 9,
                Name = ConclusionPageName,
                DisplayName = "Conclusão",
                TotalItems = 88,
                PendingItems = 60
            }
        ];
    }
    
    public static string[] GetPageTitles()
    {
        return
        [
            BasicInformationPageTitle,
            SugarcaneBalancePageNTitle,
            DesintegratorProbePageTitle,
            AnalyticalBalancePageTitle,
            PressRefractometerPageTitle,
            ClarificationSaccharimeterPagTitle,
            BenchmarkingEquipmentPageTitle,
            SystemConsistencyPageTitle,
            ConclusionPageTitle
        ];
    }
}