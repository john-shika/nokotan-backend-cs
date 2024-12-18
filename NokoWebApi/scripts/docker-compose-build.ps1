#!pwsh

$currWorkDir = Get-Location
$scriptRootDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptRootDir -ErrorAction Stop
Set-Location ..

$env:DOCKER_BUILDKIT=1

$dockerExec = "C:\Program Files\Docker\Docker\resources\bin\docker.exe"

& $dockerExec compose -f .\docker-compose.yaml -p NokoWebApi build
& $dockerExec compose up

Set-Location $currWorkDir
