#!pwsh

$currentWorkDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $currentWorkDir -ErrorAction Stop
Set-Location ..

dotnet ef dbcontext info --context NokoWebApi.Repositories.UserRepository
dotnet ef dbcontext info --context NokoWebApi.Repositories.SessionRepository
