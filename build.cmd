@echo off
set bsver=%1
if "%bsver%" == "" (
   set bsver=3
)

set config=%2
if "%config%" == "" (
   set config=Release
)

set version=2017.1.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

rstc\rstc.exe compile -i templates\bs%bsver%\*.md -o templates\bs%bsver%\templates.dotSettings -r templates\bs%bsver%\README.md

nuget restore src\Resharper.BootstrapTemplates.sln

"%ProgramFiles(x86)%\MSBuild\14.0\Bin\msbuild" src\Resharper.BootstrapTemplates.sln /t:Rebuild /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

nuget pack resharper.nuspec -NoPackageAnalysis -Version %version% -Properties "Configuration=%config%;ReSharperDep=Wave;ReSharperVer=[8.0];BsVer=%bsver%"
