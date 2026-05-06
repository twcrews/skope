using Microsoft.EntityFrameworkCore;

namespace Skope.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Organization> Organizations => Set<Organization>();
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

        modelBuilder.Entity<Organization>()
            .HasIndex(o => o.PlanningCenterId)
            .IsUnique();

        modelBuilder.Entity<Organization>()
            .HasIndex(o => o.Slug)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.PlanningCenterId)
            .IsUnique();

        modelBuilder.Entity<Organization>()
            .HasOne(o => o.BillingContact)
            .WithMany()
            .HasForeignKey(o => o.BillingContactId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Organization)
            .WithMany(o => o.Members)
            .HasForeignKey(u => u.OrganizationId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Dashboard>()
            .HasOne(d => d.Organization)
            .WithMany(o => o.Dashboards)
            .HasForeignKey(d => d.OrganizationId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Dashboard>()
            .HasIndex(d => new { d.OrganizationId, d.Slug })
            .IsUnique();

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
