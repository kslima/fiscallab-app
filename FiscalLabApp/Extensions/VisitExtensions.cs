using FiscalLabApp.Helpers;
using FiscalLabApp.Models;

namespace FiscalLabApp.Extensions;

public static class VisitExtensions
{
    public static VisitPage[] CreatePages(this Visit visit)
    {
        return
        [
            CreatePage(1, PageHelper.BasicInformationPageName, "Principal", visit.BasicInformation),
            CreatePage(2, PageHelper.SugarcaneBalancePageName, "Balança de Cana", visit.SugarcaneBalance),
            CreatePage(3, PageHelper.DesintegratorProbePageName, "Sonda/Desintegrador", visit.DesintegratorProbe),
            CreatePage(4, PageHelper.AnalyticalBalancePageName, "Balança Analítica/Temperatura",
                visit.AnalyticalBalance),
            CreatePage(5, PageHelper.PressRefractometerPageName, "Prensa/Refratômetro", visit.PressRefractometer),
            CreatePage(6, PageHelper.ClarificationSaccharimeterPageName, "Clarificação/Sacarímetro",
                visit.ClarificationSaccharimeter),
            CreatePage(7, PageHelper.BenchmarkingEquipmentPageName, "Equipamentos de Aferição/Medias",
                visit.BenchmarkingEquipment),
            CreatePage(8, PageHelper.SystemConsistencyPageName, "Consistência do Sistema", visit.SystemConsistency),
            CreatePage(9, PageHelper.ConclusionPageName, "Conclusão", visit.Conclusion)
        ];
    }

    private static VisitPage CreatePage(int order, string name, string displayName, object source)
    {
        var (totalProperties, pendingProperties) = CheckProperties(source);
        return new VisitPage
        {
            Id = order,
            Name = name,
            DisplayName = displayName,
            TotalItems = totalProperties,
            PendingItems = pendingProperties
        };
    }

    private static (int totalProperties, int nullOrEmptyStringCount) CheckProperties(object obj)
    {
        var type = obj.GetType();
        var properties = type.GetProperties();

        var totalProperties = properties.Length;
        var nullOrEmptyStringCount = 0;

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);

            if (value == null || (value is string s && string.IsNullOrWhiteSpace(s)))
            {
                nullOrEmptyStringCount++;
            }
        }

        return (totalProperties, nullOrEmptyStringCount);
    }
}