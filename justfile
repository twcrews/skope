set allow-duplicate-recipes

_default:
    @just --list --unsorted

# Start the app with hot reload
start: db
    dotnet watch --project src/Skope/Skope.csproj

# Build the solution
build:
    dotnet build

# Run all tests
test:
    dotnet test

# Start the VSCode debugger. WARNING: targets the most recently focused VSCode window.
debug:
    code --command workbench.action.debug.start

# Start the database server and ensure the skope database is up to date
[unix]
db:
    #!/usr/bin/env bash
    set -euo pipefail

    if ! dotnet ef --version &>/dev/null; then
        echo "dotnet-ef tool not found. Install with: dotnet tool install --global dotnet-ef"
        exit 1
    fi

    if docker inspect skope-sql &>/dev/null; then
        if [ "$(docker inspect -f '{{{{.State.Running}}}}' skope-sql)" != "true" ]; then
            docker start skope-sql > /dev/null
        fi
    else
        docker run -d --name skope-sql \
            -e ACCEPT_EULA=Y \
            -e "MSSQL_SA_PASSWORD=YourStrong!Passw0rd" \
            -p 1433:1433 \
            mcr.microsoft.com/mssql/server:2025-latest
    fi

    printf "Waiting for SQL Server..."
    until nc -z localhost 1433 2>/dev/null; do
        printf "."
        sleep 1
    done
    printf "\n"

    if [ ! -d "src/Skope/Migrations" ] || [ -z "$(ls -A src/Skope/Migrations 2>/dev/null)" ]; then
        dotnet ef migrations add InitialCreate --project src/Skope
    fi
    dotnet ef database update --project src/Skope --connection "Server=localhost,1433;Database=skope;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True"

[windows]
db:
    #!/usr/bin/env pwsh
    $ErrorActionPreference = "Stop"
    if (-not (dotnet ef --version 2>$null)) {
        Write-Error "dotnet-ef tool not found. Install with: dotnet tool install --global dotnet-ef"
        exit 1
    }
    if (-not (Test-Path "src/Skope/Migrations") -or @(Get-ChildItem "src/Skope/Migrations").Count -eq 0) {
        dotnet ef migrations add InitialCreate --project src/Skope
    }
    dotnet ef database update --project src/Skope --connection "Server=(localdb)\mssqllocaldb;Database=skope;Trusted_Connection=True"
