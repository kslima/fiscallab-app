using Blazored.Toast;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Repositories;
using FiscalLabApp.Services;

namespace FiscalLabApp.Extensions;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddBlazoredToast();
        services.AddScoped<IndexedDbAccessor>();
        services.AddScoped<IApiService, ApiService>();
        services.AddScoped<IPlantService, PlantService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IAssociationService, AssociationService>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IVisitService, VisitService>();
        services.AddScoped<SyncService>();
        services.AddSingleton<ApplicationContextAccessor>();
        services.AddSingleton<SelectedVisitEvent>();
        services.AddSingleton<SyncEventNotifier>();
    }
}