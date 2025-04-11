# This script compiles and runs your .NET program locally.
# Optional: Accepts a -directory parameter to pass to the executable.

param(
    [Parameter(ParameterSetName = "directory", HelpMessage="Specifies the root directory to be used by the server")]
    [string]
    $DirectoryPath
)

$ErrorActionPreference = "Stop" # Exit early if any commands fail

# Get the script's directory
$scriptPath = $MyInvocation.MyCommand.Path
$repoRoot = Split-Path $scriptPath -Parent

# Path to the built executable
$buildPath = Join-Path $repoRoot "bin\Release\net9.0\codecrafters-http-server.exe"

# Go to project root
Write-Host "[INFO] Script directory: $repoRoot `n" -ForegroundColor Cyan
Set-Location $repoRoot

# Build the project
Write-Host "[INFO] Compiling the project... `n" -ForegroundColor White
dotnet build --configuration Release

if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] Build failed." -ForegroundColor Red
    exit 1
}
Write-Host "[SUCCESS] Build completed.`n" -ForegroundColor Green

# Check if the executable exists
if (Test-Path $buildPath) {
    Write-Host "[INFO] Running the executable... `n" -ForegroundColor White

    if ($PSCmdlet.ParameterSetName -eq 'directory') {
        Write-Host "[INFO] Running with --directory flag: $DirectoryPath" -ForegroundColor Yellow
        & "$buildPath" --directory $DirectoryPath

    }else{
        Write-Host "[INFO] Running without --directory flag" -ForegroundColor Yellow
        & "$buildPath"
    }

} else {
    Write-Host "[ERROR] Executable not found at: $buildPath" -ForegroundColor Red
    exit 1
}