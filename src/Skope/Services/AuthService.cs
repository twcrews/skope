using Microsoft.EntityFrameworkCore;
using Skope.Data;

namespace Skope.Services;

public class AuthService(IDbContextFactory<AppDbContext> dbFactory)
{
    public async Task UpsertUserAndTokenAsync(
        string pcPersonId,
        string accessToken,
        string refreshToken,
        DateTime accessTokenExpiresAt)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var now = DateTime.UtcNow;

        var user = await db.Users.FirstOrDefaultAsync(u => u.PlanningCenterId == pcPersonId);

        if (user is null)
        {
            user = new User
            {
                PlanningCenterId = pcPersonId,
                CreatedAt = now,
                UpdatedAt = now,
                LastSeenAt = now
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }
        else
        {
            user.LastSeenAt = now;
            user.UpdatedAt = now;
        }

        var token = await db.UserTokens.FirstOrDefaultAsync(t => t.UserId == user.Id);
        if (token is null)
        {
            try
            {
                db.UserTokens.Add(new UserToken
                {
                    UserId = user.Id,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = accessTokenExpiresAt,
                    RefreshTokenExpiresAt = now.AddDays(90),
                    CreatedAt = now,
                    UpdatedAt = now
                });
                await db.SaveChangesAsync();
                return;
            }
            catch (DbUpdateException)
            {
                // Unique constraint violation — a record was inserted concurrently; fall through to update it
                token = await db.UserTokens.FirstOrDefaultAsync(t => t.UserId == user.Id);
                if (token is null) throw;
            }
        }

        token.AccessToken = accessToken;
        token.RefreshToken = refreshToken;
        token.ExpiresAt = accessTokenExpiresAt;
        token.RefreshTokenExpiresAt = now.AddDays(90);
        token.UpdatedAt = now;
        await db.SaveChangesAsync();
    }
}
