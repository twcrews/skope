# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
dotnet build                    # Build the solution
dotnet run --project Crews.PlanningCenter.Dashboards  # Run the app
dotnet test                     # Run all tests
dotnet test --filter "FullyQualifiedName~TestName"    # Run a single test
```

The app runs at `http://localhost:5228` (HTTP) or `https://localhost:7259` (HTTPS) in development.

## Architecture

**Stack**: .NET 10 / ASP.NET Core / Blazor Web (Interactive Server mode) / Fluent UI components

**Purpose**: Users create, share, and clone dashboards built from Planning Center API data. Dashboards have their own URLs, configurable refresh rates, and access controls.

### Project Layout

- `Crews.PlanningCenter.Dashboards/` — Main Blazor Web app
- `Crews.PlanningCenter.Dashboards.Tests/` — xUnit test project

### Key Dependencies

- `Crews.PlanningCenter.Api` (v3.0.0) — Planning Center API client
- `Microsoft.FluentUI.AspNetCore.Components` (v4.14.0) — UI component library
- `xunit` + `coverlet` — Testing and code coverage

### Blazor Rendering

Pages use `@rendermode InteractiveServer` for interactivity. The app is configured as a Blazor Web app (not Blazor Server or WASM standalone) — components are rooted in `App.razor` and routed via `Routes.razor`. `Program.cs` calls `AddRazorComponents().AddInteractiveServerComponents()`.

### Component Organization

- `Components/Layout/` — `MainLayout.razor`, `NavMenu.razor`
- `Components/Pages/` — Page components (routable)
- `Components/_Imports.razor` — Global `@using` directives for all components
