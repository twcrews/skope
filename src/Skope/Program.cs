using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Skope.Authentication;
using Skope.Components;
using Skope.Data;
using Skope.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = PlanningCenterAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddPlanningCenterAuthentication(options =>
{
    options.Scope.Add("api");
    options.Scope.Add("calendar");
    options.Scope.Add("check_ins");
    options.Scope.Add("giving");
    options.Scope.Add("groups");
    options.Scope.Add("people");
    options.Scope.Add("publishing");
    options.Scope.Add("registrations");
    options.Scope.Add("services");
    options.SaveTokens = true;
    options.Events = new OpenIdConnectEvents
    {
        OnTicketReceived = async ctx =>
        {
            var sub = ctx.Principal!.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
            var accessToken = ctx.Properties!.GetTokenValue("access_token") ?? "";
            var refreshToken = ctx.Properties!.GetTokenValue("refresh_token") ?? "";
            var expiresAt = DateTimeOffset.TryParse(ctx.Properties!.GetTokenValue("expires_at"), out var exp)
                ? exp.UtcDateTime
                : DateTime.UtcNow.AddHours(2);

            var authService = ctx.HttpContext.RequestServices.GetRequiredService<AuthService>();
            await authService.UpsertUserAndTokenAsync(sub, accessToken, refreshToken, expiresAt);
        }
    };
});

builder.Services.AddPlanningCenterApi();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IPlanningCenterTokenProvider, SkopeTokenProvider>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapGet("/login", () => Results.Challenge(
    new AuthenticationProperties { RedirectUri = "/" },
    [PlanningCenterAuthenticationDefaults.AuthenticationScheme]))
    .AllowAnonymous();

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
}).RequireAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
