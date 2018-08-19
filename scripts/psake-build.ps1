##
## Psake build for the Niche Scenario Testing Framework
##

properties {
    $baseDir = resolve-path ..\
    $srcDir = resolve-path $baseDir\src
    $testsDir = resolve-path $baseDir\tests
}

# Do our integration build 
Task Integration.Build -Depends Unit.Tests

# Do our formal build
Task Formal.Build -Depends Unit.Tests, Compile.Docs

## --------------------------------------------------------------------------------
##   Prerequisite Targets
## --------------------------------------------------------------------------------
## Tasks used to ensure that prerequisites are available when needed. 

Task Requires.DotNetExe {
    $script:dotnetExe = (get-command dotnet -ErrorAction SilentlyContinue).Path

    if ($dotnetExe -eq $null) {
        $script:dotnetExe = resolve-path $env:ProgramFiles\dotnet\dotnet.exe -ErrorAction SilentlyContinue
    }
    
    if ($dotnetExe -eq $null) {
        throw "Failed to find dotnet.exe"
    }

    Write-Output "Dotnet executable: $dotnetExe"
}

Task Requires.DocFx {
    $script:docfxExe = (get-command docfx -ErrorAction SilentlyContinue).Path

    if ($docfxExe -eq $null) {
        throw "Failed to find docfx.exe"
    }

    Write-Info "docfx executable: $docfx"
}

## --------------------------------------------------------------------------------
##   Build Targets
## --------------------------------------------------------------------------------
## Tasks used to perform steps of the actual build

Task Generate.Version {
    
    $script:versionBase = get-content $baseDir\version.txt -ErrorAction SilentlyContinue
    if ($versionBase -eq $null) {
        throw "Unable to load $baseDir\version.txt"
    }

    $versionLastUpdated = git rev-list -1 HEAD $baseDir\version.txt
    $script:patchVersion = git rev-list "$versionLastUpdated..HEAD" --count

    $branch = git name-rev --name-only HEAD
    Write-Output "Branch   $branch"

    $commit = git rev-parse --short head
    Write-Output "Commit   $commit"

    $script:version = "$versionBase.$patchVersion"
    Write-Output "Version: $version"

    if ($branch -eq "master") {
        $script:semanticVersion = $version
    }
    else {
        $semverBranch = $branch -replace "[^A-Za-z0-9-]+", "."
        $script:semanticVersion = "$version-beta.$semverBranch.$commit"
    }

    Write-Output "Semver:  $semanticVersion"
}

Task Compile -Depends Generate.Version, Requires.DotNetExe {
    
    $solution = resolve-path $baseDir\*.sln
    Write-Info "Solution is $solution"
    Write-Host

    & $dotNetExe build $solution
}

Task Unit.Tests -Depends Requires.DotNetExe, Compile {

    foreach ($project in (resolve-path $testsDir\*\*.csproj)){
        $projectName = split-path $project -Leaf
        Write-SubtaskName $projectName
        exec {
            & $dotnetExe test $project --no-build
        }
    }
}

Task Compile.Docs -Depends Requires.DocFx {

    $project = resolve-path $baseDir\docfx\docfx.json
    Write-Info "DocFx project is $project"

    & $docfxExe build $project
}

## --------------------------------------------------------------------------------
##   Support Functions
## --------------------------------------------------------------------------------

formatTaskName { 
    param($taskName) 

    $divider = "-" * ((get-host).UI.RawUI.WindowSize.Width - 2)
    return "`r`n$divider`r`n  $taskName`r`n$divider`r`n"
} 

function Write-SubtaskName($subtaskName) {
    $divider = "-" * ($subtaskName.Length + 4)
    Write-Output "$divider`r`n  $subtaskName`r`n$divider`r`n"
}

function Write-Info($message) {
    Write-Host "[i] $message"
}

function Write-Warning($message) {
    Write-Host "[!] $message"
}
