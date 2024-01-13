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
        services.AddScoped<IPlantRepository, PlantRepository>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IVisitRepository, VisitRepository>();
        services.AddScoped<IAssociationRepository, AssociationRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IVisitService, VisitService>();
    }
}