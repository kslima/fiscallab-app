namespace FiscalLabApp.Services;

public class OnlineStatusEvent
{
    public event Func<bool, Task>? IsOnlineEvent;

    public async Task NotifyOnlineStatusAsync(bool isOnline)
    {
        if (IsOnlineEvent is not null)
        {
            await IsOnlineEvent.Invoke(isOnline);
        }
        IsOnlineEvent?.Invoke(isOnline);
    }
}