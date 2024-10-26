using Microsoft.EntityFrameworkCore;

namespace Efficy.DAL;

public class EfficyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Counter> Counters { get; set; }
    public DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Team>().HasKey(x => x.Id);
        modelBuilder.Entity<Counter>().HasKey(x => x.Id);
        modelBuilder.Entity<Team>().HasMany<Counter>();
    }
}