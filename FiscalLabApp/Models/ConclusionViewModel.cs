namespace FiscalLabApp.Models;

public class ConclusionViewModel
{
    public string InspectorPerformance { get; set; } = string.Empty;
    public string LaboratoryReceptivity { get; set; } = string.Empty;
    public string Pendencies { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public bool MarkToSend { get; set; }
}