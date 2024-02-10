using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class SelectedVisitEvent
{
    public event Action<VisitViewModel>? SelectedVisit;
    public VisitViewModel? SelectedVisitModel;

    public void NotifySelectedVisit(VisitViewModel visit)
    {
        SelectedVisit?.Invoke(visit);
    }
}