#!pwsh

param (
    [string]$pattern = ""
)

$currWorkDir = Get-Location
$scriptRootDir = Split-Path -Parent $MyInvocation.MyCommand.Definition

Set-Location $scriptRootDir -ErrorAction Stop
Set-Location ..

$files = Get-ChildItem -Recurse -File -Filter *.cs

foreach ($file in $files) {
    $content = Get-Content $file.FullName
    if ($content -imatch $pattern) {
        Write-Output $file.FullName
    }
}

Set-Location $currWorkDir
