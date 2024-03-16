namespace FiscalLabApp.Services;

public class SyncEventNotifier
{
    public event EventHandler ? SyncEvent;
    
    public async Task NotifyAsync()
    {
        if (SyncEvent is not null)
        {
            foreach (var handler in SyncEvent.GetInvocationList())
            {
                if (handler is EventHandler eventHandler)
                {
                    await Task.Run(() => eventHandler(this, EventArgs.Empty));
                }
            }
        }
    }
}