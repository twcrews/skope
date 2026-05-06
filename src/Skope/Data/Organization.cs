namespace Skope.Data;

public class Organization
{
    public int Id { get; set; }
    public required string PlanningCenterId { get; set; }
    public required string Name { get; set; }
    public string? DisplayName { get; set; }
    public required string Slug { get; set; }
    public int? BillingContactId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? BillingContact { get; set; }
    public ICollection<User> Members { get; set; } = [];
    public ICollection<Dashboard> Dashboards { get; set; } = [];
}
