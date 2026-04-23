# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
just start                                            # Start the app with hot reload
just build                                            # Build the solution
just test                                             # Run all tests
dotnet test --filter "FullyQualifiedName~TestName"    # Run a single test
```

The app runs at `http://localhost:5010` (HTTP) or `https://localhost:7223` (HTTPS) in development.

## Architecture

**Stack**: .NET 10 / ASP.NET Core / Blazor Web (Interactive Server mode) / MudBlazor components

**Purpose**: Users create, share, and clone dashboards built from Planning Center API data. Dashboards have their own URLs, configurable refresh rates, and access controls.

### Project Layout

- `src/Skope/` — Main Blazor Web app
- `tests/Skope.Tests/` — xUnit test project

### Key Dependencies

- `Crews.PlanningCenter.Api` (v3.2.0) — Planning Center API client
- `MudBlazor` (v9.*) — UI component library (layout, theming, dark mode)
- `xunit` + `coverlet` — Testing and code coverage

### Authentication

Cookie-based auth with Planning Center OAuth. `Program.cs` registers `AddAuthentication()` with both schemes and `AddPlanningCenterApi()`. Scopes requested: `openid`, `api`, `calendar`, `check_ins`, `giving`, `groups`, `people`, `publishing`, `registrations`, `services`. Secrets (client ID/secret) are stored in user secrets.

### Blazor Rendering

Pages use `@rendermode InteractiveServer` for interactivity. The app is configured as a Blazor Web app (not Blazor Server or WASM standalone) — components are rooted in `App.razor` and routed via `Routes.razor`. `Program.cs` calls `AddRazorComponents().AddInteractiveServerComponents()` and `AddMudServices()`.

### Component Organization

- `Components/Layout/` — `MainLayout.razor` (MudBlazor layout with app bar and drawer), `ReconnectModal.razor`
- `Components/Pages/` — Page components (routable)
- `Components/_Imports.razor` — Global `@using` directives for all components

### Theming

MudBlazor theming is configured in `MainLayout.razor` via `MudThemeProvider`. Light/dark mode is toggled inline via `DarkModeToggle()` using the `_isDarkMode` bool. Custom palettes (`_lightPalette`, `_darkPalette`) define brand colors; no separate CSS custom property system. MudBlazor CSS/JS are loaded in `App.razor` via `_content/MudBlazor/` assets.
