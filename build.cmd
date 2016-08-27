@echo off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=2016.2.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

bin\rstc.exe compile -i templates\bs3\*.md -o templates\bs3\templates.dotSettings -r templates\bs3\README.md

nuget restore src\Resharper.BootstrapTemplates.sln

"%ProgramFiles(x86)%\MSBuild\14.0\Bin\msbuild" src\Resharper.BootstrapTemplates.sln /t:Rebuild /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

nuget pack resharper.nuspec -NoPackageAnalysis -Version %version% -Properties "Configuration=%config%;ReSharperDep=Wave;ReSharperVer=[6.0]"
