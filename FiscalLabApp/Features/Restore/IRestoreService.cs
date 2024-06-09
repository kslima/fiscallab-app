using FiscalLabApp.Models;

namespace FiscalLabApp.Features.Restore;

public interface IRestoreService
{
    Task RestoreAsync(VisitParameters visitParameters);
}