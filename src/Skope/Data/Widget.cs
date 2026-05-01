namespace Skope.Data;

public class Widget
{
    public int Id { get; set; }
    public int DashboardId { get; set; }
    public required string Title { get; set; }
    public required string Type { get; set; }
    public string? Configuration { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; }

    public Dashboard Dashboard { get; set; } = null!;
}
