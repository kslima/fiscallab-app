using FiscalLabApp.Interfaces;

namespace FiscalLabApp.Services;

public class SyncService(IApiService apiService, IVisitService visitService)
{
    public async Task SyncAsync(bool isOnline)
    {
        if (!isOnline) return;
        var allVisits = await visitService.GetAllAsync();
        var visitsToSync = allVisits
            .Where(x => x.IsFinished)
            .Where(x => x.FinishedAt is not null)
            .ToArray();

        await apiService.CreateManyVisits(visitsToSync);
    }
}