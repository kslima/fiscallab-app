using System.Net;
using Blazored.Toast;
using FiscalLabApp.Features.Backup;
using FiscalLabApp.Handlers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Repositories;
using FiscalLabApp.Resources;
using FiscalLabApp.Services;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;

namespace FiscalLabApp.Extensions;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var apiOptions = configuration
            .GetRequiredSection(nameof(ApiOptions))
            .Get<ApiOptions>()!;

        services.AddSingleton(apiOptions);

        services.AddHttpClient();
        services.AddTransient<AuthenticatedHttpClientHandler>();
        services
            .AddHttpClient("api", client => { client.BaseAddress = new Uri(apiOptions.BaseUrl); })
            .AddPolicyHandler(_ => GetRetryPolicy())
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(10))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
        
        services.AddBlazoredToast();
        services.AddScoped<IndexedDbAccessor>();
        services.AddScoped<IApiService, ApiService>();
        services.AddScoped<IAuthenticationService, ApiService>();
        services.AddScoped<IPlantService, PlantService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IAssociationService, AssociationService>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IVisitService, VisitService>();
        services.AddScoped<IBackupService, BackupService>();
        services.AddScoped<SyncService>();
        services.AddSingleton<ApplicationContextAccessor>();
        services.AddSingleton<NetworkStatusEventNotifier>();
        services.AddSingleton<SelectedPageEventNotifier>();
        services.AddSingleton<SelectedVisitEventNotifier>();
        services.AddSingleton<SyncEventNotifier>();
    }
    
    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(response => response.StatusCode == HttpStatusCode.TooManyRequests)
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));
    }
}