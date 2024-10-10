#!pwsh

$currentWorkDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $currentWorkDir -ErrorAction Stop
Set-Location ..

$contexts = @{
    User = @{
        Repository = "NokoWebApi.Repositories.UserRepository"
        OutputDir = "Migrations/UserRepositoryMigrations"
    }
    Session = @{
        Repository = "NokoWebApi.Repositories.SessionRepository"
        OutputDir = "Migrations/SessionRepositoryMigrations"
    }
}

foreach ($context in $contexts.Keys) {
    $repository = $contexts[$context].Repository
    $outputDir = $contexts[$context].OutputDir
    # $timestamp = [DateTimeOffset]::Now.ToUnixTimeSeconds()
    # $message = "Add$context_$timestamp"
    $message = "Add$context"
    dotnet ef migrations add $message --context $repository --project .. --output-dir $outputDir
    dotnet ef database update --context $repository --project ..
}

Set-Location $currentWorkDir
Set-Location ..
