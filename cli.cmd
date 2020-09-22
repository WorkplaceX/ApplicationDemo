echo off
if exist "./Application.Cli/bin/" (
    dotnet run --project "Application.Cli/Application.Cli.csproj" --no-build -- %*
) ELSE (
    dotnet run --project "Application.Cli/Application.Cli.csproj" -- %*
)