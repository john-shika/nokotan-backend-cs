#!pwsh

$currentWorkDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $currentWorkDir -ErrorAction Stop
Set-Location ..

dotnet ef migrations add AddUser --context NokoWebApi.Repositories.UserRepository
dotnet ef migrations add AddSession --context NokoWebApi.Repositories.SessionRepository

dotnet ef database update --context NokoWebApi.Repositories.UserRepository
dotnet ef database update --context NokoWebApi.Repositories.SessionRepository
