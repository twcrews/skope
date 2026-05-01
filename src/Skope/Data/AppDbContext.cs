using Microsoft.EntityFrameworkCore;

namespace Skope.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserToken> UserTokens => Set<UserToken>();
    public DbSet<Dashboard> Dashboards => Set<Dashboard>();
    public DbSet<Widget> Widgets => Set<Widget>();
    public DbSet<DashboardShare> DashboardShares => Set<DashboardShare>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DashboardShare>()
            .HasKey(s => new { s.DashboardId, s.UserId });

        modelBuilder.Entity<DashboardShare>()
            .HasOne(s => s.User)
            .WithMany(u => u.DashboardShares)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Dashboard>()
            .HasIndex(d => d.Slug)
            .IsUnique();
    }
}
