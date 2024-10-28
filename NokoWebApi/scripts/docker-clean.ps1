#!pwsh

$currWorkDir = Get-Location
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptDir -ErrorAction Stop
Set-Location ..

$env:DOCKER_BUILDKIT=1

$dockerExec = "C:\Program Files\Docker\Docker\resources\bin\docker.exe"

& $dockerExec system prune
& $dockerExec volume prune

Set-Location $currWorkDir