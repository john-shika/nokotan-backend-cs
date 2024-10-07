#!pwsh

$currentWorkDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $currentWorkDir -ErrorAction Stop
Set-Location ..

dotnet ef database drop --context NokoWebApi.Repositories.UserRepository
dotnet ef database drop --context NokoWebApi.Repositories.SessionRepository

dotnet ef migrations remove --context NokoWebApi.Repositories.UserRepository
dotnet ef migrations remove --context NokoWebApi.Repositories.SessionRepository
