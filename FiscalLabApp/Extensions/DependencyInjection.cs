using FiscalLabApp.Interfaces;
using FiscalLabApp.Repositories;
using FiscalLabApp.Repositories.SqLite;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FiscalLabApp.Extensions;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services)
    {
#if DEBUG
        Console.WriteLine("debugging....");
        services.AddDbContextFactory<ApplicationDbContext>(
            options =>
            {
                options.UseInMemoryDatabase("FiscalLabDb");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
#else
            Console.WriteLine("not debugging....");
            services.AddDbContextFactory<ApplicationDbContext>(
                    options => options.UseSqlite($"Filename={DatabaseService<ApplicationDbContext>.FileName}"));
#endif
        
        services.AddScoped<IndexedDbAccessor>();
        services.AddScoped<IPlantRepository, PlantRepository>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IVisitRepository, VisitRepository>();
        services.AddScoped<IVisitService, VisitService>();
    }
    
    public static async Task InitializeAsync(this WebAssemblyHost host)
    {
        // Initialize DatabaseContext and sync with IndexedDb Files
        var dbService = host.Services.GetRequiredService<DatabaseService<ApplicationDbContext>>();
        await dbService.InitDatabaseAsync();

        // // Sync Speakers
        // var speakerService = host.Services.GetRequiredService<SpeakerService>();
        // await speakerService.InitializeAsync();
        //
        // // Sync Contributions
        // var contributionService = host.Services.GetRequiredService<ContributionsService>();
        // await contributionService.InitAsync();
    }
}