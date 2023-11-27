using FiscalLabApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace FiscalLabApp.Repositories.SqLite;

public class ApplicationDbContext : DbContext
{
    public DbSet<Email> Emails { get; set; }
    
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options, IJSRuntime jsRuntime) : base(options)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/file.js").AsTask());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Email>()
            .HasKey(e => e.Id);
        
        modelBuilder
            .Entity<Email>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging(false);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
#if RELEASE
            await PersistDatabaseAsync(cancellationToken);       
#endif
        return result;
    }
    
    private async Task PersistDatabaseAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Start saving database");
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("syncDatabase", false, cancellationToken);
        Console.WriteLine("Finish save database");
    }
} 