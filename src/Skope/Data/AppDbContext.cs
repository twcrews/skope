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

        modelBuilder.Entity<User>()
            .HasIndex(u => u.PlanningCenterId)
            .IsUnique();

        modelBuilder.Entity<UserToken>()
            .HasIndex(t => t.UserId)
            .IsUnique();

        modelBuilder.Entity<Dashboard>()
            .HasIndex(d => d.Slug);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.PlanningCenterOrganizationId);

        modelBuilder.Entity<Dashboard>()
            .HasOne(d => d.UpdatedBy)
            .WithMany()
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Widget>()
            .HasOne(w => w.CreatedBy)
            .WithMany()
            .HasForeignKey(w => w.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Widget>()
            .HasOne(w => w.UpdatedBy)
            .WithMany()
            .HasForeignKey(w => w.UpdatedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Dashboard>()
            .HasOne(d => d.DeletedBy)
            .WithMany()
            .HasForeignKey(d => d.DeletedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Widget>()
            .HasOne(w => w.DeletedBy)
            .WithMany()
            .HasForeignKey(w => w.DeletedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Dashboard>()
            .HasQueryFilter(d => d.DeletedAt == null);

        modelBuilder.Entity<Widget>()
            .HasQueryFilter(w => w.DeletedAt == null);

        modelBuilder.Entity<DashboardShare>()
            .HasQueryFilter(s => s.Dashboard.DeletedAt == null);
    }
}
