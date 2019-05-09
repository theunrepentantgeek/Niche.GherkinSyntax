##
## Psake build for the Niche Scenario Testing Framework
##

properties {
    $baseDir = resolve-path ..\
    $srcDir = resolve-path $baseDir\src
    $testsDir = resolve-path $baseDir\tests
    $buildDir = "$baseDir\build"
}

# Do our integration build 
Task Integration.Build -Depends Clean.BuildFolder, Clean.SourceFolder, Unit.Tests

# Do our formal build
Task Formal.Build -Depends Clean.BuildFolder, Clean.SourceFolder, Unit.Tests, Compile.Docs, Package

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

    Write-Info "docfx executable: $docfxExe"
}

## --------------------------------------------------------------------------------
##   Cleaning Targets
## --------------------------------------------------------------------------------
## Tasks used to clean up 

Task Clean.SourceFolder {

    Write-Info "Cleaning $srcDir"
    remove-item $srcDir\*\bin\* -recurse -ErrorAction SilentlyContinue
    remove-item $srcDir\*\obj\* -recurse -ErrorAction SilentlyContinue
    remove-item $srcDir\*\publish\* -recurse -ErrorAction SilentlyContinue

    Write-Info "Cleaning $testsDir"
    remove-item $testsDir\*\bin\* -recurse -ErrorAction SilentlyContinue
    remove-item $testsDir\*\obj\* -recurse -ErrorAction SilentlyContinue
    remove-item $testsDir\*\publish\* -recurse -ErrorAction SilentlyContinue
}

Task Clean.BuildFolder {

    Write-Info "Cleaning $buildDir"
    remove-item $buildDir -ErrorAction SilentlyContinue -Force -Recurse
    mkdir $buildDir -ErrorAction SilentlyContinue | Out-Null

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

    $branch = git symbolic-ref --short HEAD
    Write-Output "Branch   $branch"

    $commit = git rev-parse --short head
    Write-Output "Commit   $commit"

    $script:version = "$versionBase.$patchVersion"
    Write-Output "Version: $version"

    if ($branch -eq "master") {
        $script:semanticVersion = $version
    }
    elseif ($branch -eq "develop") {
        $script:semanticVersion = "$version-beta.$commit"
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

Task Extract.Metadata -Depends Requires.DocFx {

    $project = resolve-path $baseDir\docfx\docfx.json

    $env:DOCFX_SOURCE_BRANCH_NAME = "master"
    Write-Info "DocFx project is $project"

    & $docfxExe metadata $project
}

Task Compile.Docs -Depends Requires.DocFx, Extract.Metadata {

    $project = resolve-path $baseDir\docfx\docfx.json

    $env:DOCFX_SOURCE_BRANCH_NAME = "master"
    Write-Info "DocFx project is $project"

    & $docfxExe build $project
}

Task Package -Depends Requires.DotNetExe, Compile {

    $project = resolve-path $srcDir\Niche.GherkinSyntax\*.csproj
    Write-Info "Project is $project"

    & $dotnetExe pack $project /p:Version=$semanticVersion --no-build -o $buildDir
}

## --------------------------------------------------------------------------------
##   Support Functions
## --------------------------------------------------------------------------------

formatTaskName { 
    param($taskName) 

    $width = (get-host).UI.RawUI.WindowSize.Width - 2
    if ($width -eq $null -or $width -gt 100) {
        $width = 100
    }

    $divider = "=" * $width
    return "`r`n$divider`r`n  $taskName`r`n$divider`r`n"
} 

function Write-SubtaskName($subtaskName) {
    $divider = "-" * ($subtaskName.Length + 4)
    Write-Output "$divider`r`n  $subtaskName`r`n$divider`r`n"
}

function Write-Info($message) {
    Write-Host "[*] $message"
}

function Write-Warning($message) {
    Write-Host "[!] $message"
}
