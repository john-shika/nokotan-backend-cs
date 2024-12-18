#!pwsh

$currWorkDir = Get-Location
$scriptRootDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptRootDir -ErrorAction Stop
Set-Location ..

$ctxs = @{
    User = @{
        Repository = "NokoWebApi.Repositories.UserRepository"
        OutDir = "Migrations/UserRepositoryMigrations"
    }
    Session = @{
        Repository = "NokoWebApi.Repositories.SessionRepository"
        OutDir = "Migrations/SessionRepositoryMigrations"
    }
}

foreach ($ctx in $ctxs.Keys) {
    $repo = $ctxs[$ctx].Repository
    dotnet ef database drop --context $repo --project . --force
    dotnet ef migrations remove --context $repo --project .
}

Set-Location $currWorkDir
