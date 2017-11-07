[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True)]
    [String]$version
)
#WriteInfomation(Get-ScriptDirectory)

$manifestPath = $(Get-Location).Path
$manifestPath = $manifestPath + "\HtmlLabel.Std.csproj"

$appConfig = New-Object XML

# make a backup
Copy-Item -Path $manifestPath -Destination $($manifestPath + ".bak") -Force

# load the config file as an xml object
$appConfig.Load($manifestPath)

    
Write-Host $("mapping VersionCode " + $appConfig.Project.PropertyGroup.Version + " to " + $version)

$appConfig.Project.PropertyGroup.Version = $version

$appConfig.Save($manifestPath)