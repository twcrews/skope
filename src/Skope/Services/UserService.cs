using Crews.PlanningCenter.Api;
using Crews.PlanningCenter.Api.People.V2025_11_10;

namespace Skope.Services;

public class UserService(PeopleClient peopleClient)
{
    public Person? CurrentUser { get; private set; }
    public event EventHandler? CurrentUserChanged;
    
    public async Task UpdateCurrentUserAsync()
    {
        PersonResponse response = await peopleClient.Latest.Me.GetAsync();
        PersonResource? resource = response.Data;
        CurrentUser = response.Data?.Attributes;
        CurrentUserChanged?.Invoke(this, EventArgs.Empty);
    }
}
