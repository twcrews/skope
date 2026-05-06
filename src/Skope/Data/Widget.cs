namespace Skope.Data;

public class Widget
{
    public int Id { get; set; }
    public int DashboardId { get; set; }
    public WidgetType WidgetType { get; set; }
    public DataSource DataSource { get; set; }
    public string? Title { get; set; }
    public int Position { get; set; }
    public bool IsPublic { get; set; }
    public string DisplayConfig { get; set; } = "{}";
    public string SourceConfig { get; set; } = "{}";

    public int CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpdatedById { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedById { get; set; }

    public Dashboard Dashboard { get; set; } = null!;
    public User CreatedBy { get; set; } = null!;
    public User UpdatedBy { get; set; } = null!;
    public User? DeletedBy { get; set; }
}
