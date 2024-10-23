#!pwsh

$currWorkDir = Get-Location
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptDir -ErrorAction Stop
Set-Location ..

$downloads = @(
    @{
        Url = "https://cdn.jsdelivr.net/npm/@scalar/api-reference@1.25.48/dist/style.min.css"
        OutFile = "wwwroot/css/scalar.api-reference.css"
    },
    @{
        Url = "https://cdn.jsdelivr.net/npm/@scalar/api-reference@latest/dist/browser/standalone.min.js"
        OutFile = "wwwroot/js/scalar.api-reference.js"
    }
)

foreach ($download in $downloads) {
    $url = $download.Url
    $outFile = $download.OutFile

    $outDir = [System.IO.Path]::GetDirectoryName($outFile)
    if (-not (Test-Path -Path $outDir)) {
        try {
            New-Item -ItemType Directory -Path $outDir -Force
            Write-Output "Successfully created directory: $outDir"
        }
        catch {
            Write-Output "Failed to create directory: $outDir"
            continue
        }
    }

    try {
        Invoke-WebRequest -Uri $url -OutFile $outFile
        Write-Output "Successfully downloaded file: $outFile"
    }
    catch {
        Write-Output "Failed to download file: $url"
    }
}

Set-Location $currWorkDir
