using FiscalLabApp.Interfaces;
using FiscalLabApp.Repositories;
using FiscalLabApp.Services;

namespace FiscalLabApp.Extensions;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IndexedDbAccessor>();
        services.AddScoped<IPlantRepository, PlantRepository>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IVisitRepository, VisitRepository>();
        services.AddScoped<IVisitService, VisitService>();
    }
}