namespace Skope.Data;

public class Dashboard
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public int? RefreshIntervalSeconds { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User Owner { get; set; } = null!;
    public ICollection<Widget> Widgets { get; set; } = [];
    public ICollection<DashboardShare> Shares { get; set; } = [];
}
