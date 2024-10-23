#!pwsh

$currentWorkDir = Get-Location
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptDir -ErrorAction Stop
Set-Location "../.."

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
    dotnet ef dbcontext info --context $repository --project "."
}

Set-Location $currentWorkDir
