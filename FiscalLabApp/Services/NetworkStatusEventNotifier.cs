using FiscalLabApp.Enums;

namespace FiscalLabApp.Services;

public class NetworkStatusEventNotifier
{
    public event EventHandler<NetworkStatus>? NetworkStatusChangedEvent;
    
    public void Notify(NetworkStatus status)
    {
        NetworkStatusChangedEvent?.Invoke(this, status);
    }
}