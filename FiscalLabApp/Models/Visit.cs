using FiscalLabApp.Enums;

namespace FiscalLabApp.Models;

public class Visit
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public VisitStatus Status { get; set; } = VisitStatus.InProgress;
    public BasicInformation BasicInformation { get; set; } = new();
    public SugarcaneBalance SugarcaneBalance { get; set; } = new();
    public DesintegratorProbe DesintegratorProbe { get; set; } = new();
    public AnalyticalBalance AnalyticalBalance { get; set; } = new();
    public PressRefractometer PressRefractometer { get; set; } = new();
    public ClarificationSaccharimeter ClarificationSaccharimeter { get; set; } = new();
    public BenchmarkingEquipment BenchmarkingEquipment { get; set; } = new();
    public SystemConsistency SystemConsistency { get; set; } = new();
    public Conclusion Conclusion { get; set; } = new();
    public bool NotifyByEmail { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? FinishedAt { get; set; }
    public DateTime? SyncedAt { get; set; }
    public DateTime? NotifiedByEmailAt { get; set; }
    public List<Image> Images { get; set; } = [];
    public List<BalanceTest> BalanceTests { get; set; } = [];
}