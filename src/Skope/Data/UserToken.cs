namespace Skope.Data;

public class UserToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public User User { get; set; } = null!;
}
