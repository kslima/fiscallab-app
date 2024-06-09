using System.Text.Json;
using Blazored.Toast.Services;
using FiscalLabApp.Components.Shared;
using FiscalLabApp.Features.Backup;
using FiscalLabApp.Helpers;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FiscalLabApp.Components.Pages;

public partial class Settings : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private SyncEventNotifier SyncEventNotifier { get; set; } = null!;
    [Inject] private ApplicationContextAccessor ApplicationContextAccessor { get; set; } = null!;
    [Inject] private SyncService SyncService { get; set; } = null!;
    [Inject] private IBackupService BackupService { get; set; } = null!;
    private SettingButton _restoreButton = null!;
    private SettingButton _backupButton = null!;
    private SettingButton _syncVisitsButton = null!;
    private bool ShowSyncVisitsSpinner { get; set; }
    private string SyncVisitsStatus { get; set; } = "Sincronizar visitas";

    private bool ShowBackupSpinner { get; set; }
    private string BackupStatus { get; set; } = "Backup";

    private bool ShowClearDataSpinner { get; set; }
    private string ClearDataStatus { get; set; } = "Limpar dados";
    private string? Error { get; set; }

    private async Task SyncVisitsAsync()
    {
        try
        {
            SyncVisitsStatus = "Sincronizando....";
            ShowSyncVisitsSpinner = true;
            StateHasChanged();
            await SyncService.SyncAsync();
            StateHasChanged();
            await SyncEventNotifier.NotifyAsync();
            ToastService.ShowSuccess(MessageHelper.SuccessOnSyncVisits);
        }
        catch (Exception e)
        {
            Error = e.ToString();
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Error);
            StateHasChanged();
            ToastService.ShowError(MessageHelper.ErrorOnSyncVisits);
        }
        finally
        {
            SyncVisitsStatus = "Sincronizar";
            ShowSyncVisitsSpinner = false;
        }
    }

    private async Task ClearDataBase()
    {
        var confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Limpar Aplicativo ?");
        if (!confirmed) return;

        try
        {
            ClearDataStatus = "Limpando dados....";
            ShowClearDataSpinner = true;
            StateHasChanged();

            await JsRuntime.InvokeVoidAsync("clearIndexedDB");
            StateHasChanged();
        }
        catch (Exception e)
        {
            Error = e.ToString();
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Error);
            StateHasChanged();
            ToastService.ShowError(MessageHelper.ErrorOnClearData);
        }
        finally
        {
            ClearDataStatus = "Limpar dados";
            ShowClearDataSpinner = false;
        }
    }

    private async Task CopyError()
    {
        await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Error);
    }

    private async Task HandleSyncVisitsClick()
    {
        _syncVisitsButton.IsLoading = true;
        await Task.Delay(5000);
        _syncVisitsButton.IsLoading = false;
    }

    private async Task HandleRestoreClick()
    {
        _restoreButton.IsLoading = true;
        await Task.Delay(5000);
        _restoreButton.IsLoading = false;
    }

    private async Task HandleBackupAsync()
    {
        try
        {
            _backupButton.IsLoading = true;

            var backup = await BackupService.CreateAsync();
            var currentDate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var fileName = $"backup_{currentDate}.json";
            await JsRuntime.InvokeAsync<object>(
                "shareJsonFile",
                JsonSerializer.Serialize(backup, JsonHelper.JsonSerializerOptions),
                fileName
            );
        }
        catch (Exception e)
        {
            Error = e.ToString();
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Error);
            ToastService.ShowError(MessageHelper.ErrorOnBackup);
        }
        finally
        {
            _backupButton.IsLoading = false;
        }
    }
}