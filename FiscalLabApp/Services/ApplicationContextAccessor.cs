using FiscalLabApp.Enums;

namespace FiscalLabApp.Services;

public class ApplicationContextAccessor
{
    public NetworkStatus NetworkStatus { get; set; }

    public bool IsOnline()
    {
        return NetworkStatus == NetworkStatus.Online;
    }
}