using System.Text.Json;
using Blazored.Toast.Services;
using FiscalLabApp.Enums;
using FiscalLabApp.Features.Backup;
using FiscalLabApp.Features.Restore;
using FiscalLabApp.Features.Visits;
using FiscalLabApp.Helpers;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FiscalLabApp.Components.Pages;

public partial class Settings : ComponentBase
{
    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject]
    private IToastService ToastService { get; set; } = null!;
    [Inject]
    private SyncEventNotifier SyncEventNotifier { get; set; } = null!;
    [Inject]
    private ApplicationContextAccessor ApplicationContextAccessor { get; set; } = null!;
    [Inject]
    private IVisitService VisitService { get; set; } = null!;
    [Inject]
    private IBackupService BackupService { get; set; } = null!;
    [Inject]
    private IRestoreService RestoreService { get; set; } = null!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    private bool IsTaskRunning { get; set; }
    
    private async Task HandleSyncVisitsAsync()
    {
        try
        {
            IsTaskRunning = true;

            var visits = await VisitService.GetAllLocalAsync();
            var successOnUpsert = await VisitService.UpsertAsync(visits);
            if (!successOnUpsert)
            {
                ToastService.ShowError(MessageHelper.ErrorOnSyncVisits);
                return;
            }

            var parameters = new VisitParameters
            {
                Status = VisitStatus.InProgress,
                PageSize = 100
            };
        
            var response = await VisitService.ListAsync(parameters);
            if (!response.IsSuccess)
            {
                ToastService.ShowError(MessageHelper.ErrorOnSyncVisits);
                return;
            }

            visits = response.Data!;
            await VisitService.CreateManyAsync(visits);
            ToastService.ShowSuccess(MessageHelper.SuccessOnSyncVisits);
        }
        catch (Exception e)
        {
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", e.ToString());
            ToastService.ShowError(MessageHelper.ErrorOnSyncVisits);
        }
        finally
        {
            IsTaskRunning = false;
        }
    }

    private async Task HandleRestoreAsync()
    {
        try
        {
            IsTaskRunning = true;
            var visitParameters = new VisitParameters
            {
                Status = VisitStatus.InProgress,
                PageSize = 100
            };
            await RestoreService.RestoreAsync(visitParameters);
            ToastService.ShowSuccess(MessageHelper.SuccessOnRestoreData);
            NavigationManager.Refresh();

        }
        catch (Exception e)
        {
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", e.ToString());
            ToastService.ShowError(MessageHelper.ErrorOnRestoreData);
        }
        finally
        {
            IsTaskRunning = false;
        }
    }

    private async Task HandleBackupAsync()
    {
        try
        {
            IsTaskRunning = true;

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
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", e.ToString());
            ToastService.ShowError(MessageHelper.ErrorOnBackup);
        }
        finally
        {
            IsTaskRunning = false;
        }
    }
}