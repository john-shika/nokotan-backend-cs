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
    dotnet ef database drop --context $repository --project .. --force
    dotnet ef migrations remove --context $repository --project ..
}

Set-Location $currentWorkDir
Set-Location ..
