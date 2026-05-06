namespace Skope.Data;

public class Dashboard
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public int OwnerId { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public int? RefreshIntervalSeconds { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UpdatedById { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedById { get; set; }

    public Organization Organization { get; set; } = null!;
    public User Owner { get; set; } = null!;
    public User UpdatedBy { get; set; } = null!;
    public User? DeletedBy { get; set; }
    public ICollection<Widget> Widgets { get; set; } = [];
    public ICollection<DashboardShare> Shares { get; set; } = [];
}
