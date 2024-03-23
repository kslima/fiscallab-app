namespace FiscalLabApp.Services;

public class SelectedVisitEvent
{
    public event Action? SelectedVisit;
    
    public void NotifyFill()
    {
        SelectedVisit?.Invoke();
    }
}