using FiscalLabApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FiscalLabApp.Repositories;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Menu> Menus { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Menu>()
            .HasKey(m => m.Id);
        
        base.OnModelCreating(builder);
    }
}