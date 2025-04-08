# Use this script to run your program LOCALLY.

$ErrorActionPreference = "Stop" # Exit early if any commands fail

$temp_directory = "C:\tmp\codecrafters-build-http-server-csharp"
$temp_path = Join-Path $temp_directory "codecrafters-http-server.exe"

$script_path = $MyInvocation.MyCommand.Path
$dirname = Split-Path $script_path -Parent

Write-Host "[INFO] Directory of the script : $dirname `n" -ForegroundColor White
cd $dirname # Ensure compile steps are run within the repository directory

# Compile the project
Write-Host "[INFO] Compiling the project... `n" -ForegroundColor White
try {
    dotnet build --configuration Release --output $temp_directory codecrafters-http-server.csproj
} catch {
    Write-Host "[ERROR] Build failed: $_" -ForegroundColor Red
    exit 1
}
Write-Output ""

# Run the executable if it exists
if (Test-Path $temp_path) {
    Write-Host "[INFO] Running the program... `n" -ForegroundColor White
    & "$temp_path"
} else {
    Write-Host "[ERROR] Executable not found at $temp_path" -ForegroundColor Red
    exit 1
}

exit