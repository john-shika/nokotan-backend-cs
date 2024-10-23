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
    $outDir = $ctxs[$ctx].OutDir
    # $timestamp = [DateTimeOffset]::Now.ToUnixTimeSeconds()
    # $message = "Add$context_$timestamp"
    $message = "Add$ctx"
    dotnet ef migrations add $message --context $repo --project . --output-dir $outDir
    dotnet ef database update --context $repo --project .
}

Set-Location $currWorkDir
