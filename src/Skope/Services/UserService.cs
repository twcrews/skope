using System.Text.Json;
using Crews.PlanningCenter.Api;
using Crews.PlanningCenter.Api.People.V2025_11_10;
using Microsoft.EntityFrameworkCore;
using Skope.Data;

namespace Skope.Services;

public class UserService(PeopleClient peopleClient, IDbContextFactory<AppDbContext> dbFactory)
{
    public Person? CurrentUser { get; private set; }
    public Organization? CurrentOrganization { get; private set; }
    public string? CurrentOrganizationId { get; private set; }
    public event EventHandler? CurrentUserChanged;

    public async Task UpdateCurrentUserAsync()
    {
        PersonResponse response = await peopleClient.Latest.Me
            .IncludeOrganization()
            .GetAsync();
        CurrentUser = response.Data?.Attributes;
        CurrentOrganization = response.Document?.Included?.FirstOrDefault()?.Attributes.Deserialize<Organization>();
        CurrentOrganizationId = response.Document?.Included?.FirstOrDefault()?.Id;

        var pcPersonId = response.Data?.Id;
        if (pcPersonId is not null && CurrentOrganizationId is not null)
        {
            await using var db = await dbFactory.CreateDbContextAsync();
            var user = await db.Users.FirstOrDefaultAsync(u => u.PlanningCenterId == pcPersonId);
            if (user is not null && user.PlanningCenterOrganizationId != CurrentOrganizationId)
            {
                user.PlanningCenterOrganizationId = CurrentOrganizationId;
                await db.SaveChangesAsync();
            }
        }

        CurrentUserChanged?.Invoke(this, EventArgs.Empty);
    }
}
