namespace FiscalLabApp.Models;

public class VisitViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public BasicInformationViewModel BasicInformation { get; set; } = new();
    public SugarcaneBalanceViewModel SugarcaneBalance { get; set; } = new();
    public DesintegratorProbeViewModel DesintegratorProbe { get; set; } = new();
    public AnalyticalBalanceViewModel AnalyticalBalance { get; set; } = new();
    public PressRefractometerViewModel PressRefractometer { get; set; } = new();
    public ClarificationSaccharimeterViewModel ClarificationSaccharimeter { get; set; } = new();
    public BenchmarkingEquipmentViewModel BenchmarkingEquipment { get; set; } = new();
    public SystemConsistencyViewModel SystemConsistency { get; set; } = new();
    public ConclusionViewModel Conclusion { get; set; } = new();
    public bool IsFinished { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? SyncedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public DateTime? SentAt { get; set; }
    public List<ImageViewModel> Images { get; set; } = [];

    public VisitViewModelMetadata GetMetadata()
    {
        var pages = new List<VisitPage>
        {
            CreatePage(1,"main","Principal", BasicInformation),
            CreatePage(2,"sugarcane-balance","Balança de Cana", SugarcaneBalance),
            CreatePage(3,"desintegrator-probe","Sonda/Desintegrador", DesintegratorProbe),
            CreatePage(4,"analytical-balance","Balança Analítica/Temperatura", AnalyticalBalance),
            CreatePage(5,"press-refractometer","Prensa/Refratômetro", PressRefractometer),
            CreatePage(6,"clarification-saccharimeter","Clarificação/Sacarímetro", ClarificationSaccharimeter),
            CreatePage(7,"benchmarking-equipment","Equipamentos de Aferição/Medias", BenchmarkingEquipment),
            CreatePage(8,"system-consistency","Consistência do Sistema", SystemConsistency),
            CreatePage(9,"conclusion","Conclusão", Conclusion),
        };

        var visitViewModelMetadata = new VisitViewModelMetadata
        {
            Pages = pages,
            TotalItems = pages.Sum(p => p.TotalItems),
            PendingItems = pages.Sum(p => p.PendingItems)
        };
        return visitViewModelMetadata;
    }
    
    
    private static VisitPage CreatePage(int order, string name, string displayName, object obj)
    {
        var (totalProperties, pendingProperties) = CheckProperties(obj);
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