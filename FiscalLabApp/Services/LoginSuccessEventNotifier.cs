namespace FiscalLabApp.Services;

public class LoginSuccessEventNotifier
{
    public event Action? SuccessLoginEvent;
    
    public void Notify()
    {
        SuccessLoginEvent?.Invoke();
    }
}