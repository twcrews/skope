using System.Net.Http.Json;
using System.Text.Json;
using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Skope.Data;

namespace Skope.Authentication;

public sealed class SkopeTokenProvider(
    IHttpContextAccessor httpContextAccessor,
    IDbContextFactory<AppDbContext> dbFactory,
    IConfiguration config,
    ILogger<SkopeTokenProvider> logger) : IPlanningCenterTokenProvider
{
    public async Task<string?> GetAccessTokenAsync()
    {
        var sub = httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (sub is null) return null;

        await using var db = await dbFactory.CreateDbContextAsync();
        var token = await db.UserTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.User.PlanningCenterId == sub);

        if (token is null) return null;

        var now = DateTime.UtcNow;

        if (token.ExpiresAt > now)
            return token.AccessToken;

        if (token.RefreshTokenExpiresAt <= now)
        {
            logger.LogWarning("Both access and refresh tokens expired for user {Sub}", sub);
            return null;
        }

        return await RefreshAsync(db, token, now);
    }

    private async Task<string?> RefreshAsync(AppDbContext db, UserToken token, DateTime now)
    {
        var clientId = config["PlanningCenter:ClientId"];
        var clientSecret = config["PlanningCenter:ClientSecret"];

        using var http = new HttpClient();
        var response = await http.PostAsync(
            "https://api.planningcenteronline.com/oauth/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = token.RefreshToken,
                ["client_id"] = clientId ?? "",
                ["client_secret"] = clientSecret ?? ""
            }));

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Token refresh failed for user {UserId}: {Status}",
                token.UserId, response.StatusCode);
            return null;
        }

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        var newAccessToken = body.GetProperty("access_token").GetString()!;
        var newRefreshToken = body.TryGetProperty("refresh_token", out var rt)
            ? rt.GetString()! : token.RefreshToken;
        var expiresIn = body.TryGetProperty("expires_in", out var ei) ? ei.GetInt32() : 7200;

        token.AccessToken = newAccessToken;
        token.RefreshToken = newRefreshToken;
        token.ExpiresAt = now.AddSeconds(expiresIn);
        token.RefreshTokenExpiresAt = now.AddDays(90);
        token.UpdatedAt = now;
        await db.SaveChangesAsync();

        return newAccessToken;
    }
}
