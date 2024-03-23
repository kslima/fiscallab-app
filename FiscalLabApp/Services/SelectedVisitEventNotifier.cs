namespace FiscalLabApp.Services;

public class SelectedVisitEventNotifier
{
    public event Action? SelectedVisit;
    
    public void NotifyFill()
    {
        SelectedVisit?.Invoke();
    }
}