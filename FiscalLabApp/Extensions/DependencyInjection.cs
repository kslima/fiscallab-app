using FiscalLabApp.Interfaces;
using FiscalLabApp.Repositories;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FiscalLabApp.Extensions;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services)
    {
        // services.AddDbContextFactory<ApplicationDbContext>(
        //     options => options.UseSqlite($"Filename={DatabaseService<ApplicationDbContext>.FileName}"));

        services.AddScoped<IndexedDbAccessor>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IVisitRepository, VisitRepository>();
        services.AddScoped<IVisitService, VisitService>();
    }

    public static async Task InitializeMenusAsync(this WebAssemblyHost host)
    {
        var dbService = host.Services.GetRequiredService<DatabaseService<ApplicationDbContext>>();
        await dbService.InitDatabaseAsync();
        
        var menuService = host.Services.GetRequiredService<IMenuService>();
    }
}