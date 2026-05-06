namespace Skope.Data;

public class DashboardShare
{
    public int DashboardId { get; set; }
    public int UserId { get; set; }
    public SharePermission Permission { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Dashboard Dashboard { get; set; } = null!;
    public User User { get; set; } = null!;
}
