namespace FiscalLabApp.Services;

public class SelectedPageEventNotifier
{
    public event Action<string>? SelectedPageEvent;
    
    public void Notify(string currentPage)
    {
        SelectedPageEvent?.Invoke(currentPage);
    }
}