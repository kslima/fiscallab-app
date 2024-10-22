namespace FiscalLabApp.Features.DataBackup;

public interface IBackupService
{
    Task<BackupResult> CreateAsync();
}