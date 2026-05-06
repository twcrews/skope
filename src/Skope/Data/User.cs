namespace Skope.Data;

public class User
{
    public int Id { get; set; }
    public required string PlanningCenterId { get; set; }
    public int OrganizationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime LastSeenAt { get; set; }
    public Organization Organization { get; set; } = null!;
    public ICollection<UserToken> Tokens { get; set; } = [];
    public ICollection<Dashboard> OwnedDashboards { get; set; } = [];
    public ICollection<DashboardShare> DashboardShares { get; set; } = [];
}
