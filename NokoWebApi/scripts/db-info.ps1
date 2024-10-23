#!pwsh

$currWorkDir = Get-Location
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptDir -ErrorAction Stop
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
    dotnet ef dbcontext info --context $repo --project .
}

Set-Location $currWorkDir
