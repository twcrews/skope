using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Extensions;
using Crews.Skope.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Services 
    .AddAuthentication(options => 
    { 
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
        options.DefaultChallengeScheme = PlanningCenterAuthenticationDefaults.AuthenticationScheme; 
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
    }) 
    .AddCookie() 
    .AddPlanningCenterAuthentication(config => 
    { 
        config.Scope.Clear(); 
        config.Scope.Add("openid"); 
        config.Scope.Add("api"); 
        config.Scope.Add("calendar"); 
        config.Scope.Add("check_ins"); 
        config.Scope.Add("giving"); 
        config.Scope.Add("groups"); 
        config.Scope.Add("people"); 
        config.Scope.Add("publishing"); 
        config.Scope.Add("registrations"); 
        config.Scope.Add("services"); 
    }); 
 
builder.Services.AddPlanningCenterApi();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

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

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
