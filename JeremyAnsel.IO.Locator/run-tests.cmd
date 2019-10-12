@echo off
setlocal

cd "%~dp0"

if '%Configuration%' == '' if not '%1' == '' set Configuration=%1
if '%Configuration%' == '' set Configuration=Debug

dotnet tool update coverlet.console --tool-path packages
dotnet tool update dotnet-reportgenerator-globaltool --tool-path packages

if exist bld\coverage rd /s /q bld\coverage
md bld\coverage

packages\coverlet "JeremyAnsel.IO.Locator.Tests\bin\%Configuration%\netcoreapp3.0\JeremyAnsel.IO.Locator.Tests.dll" --target "dotnet" --targetargs "test JeremyAnsel.IO.Locator.Tests -f netcoreapp3.0 --no-build" --output "bld\coverage\results.json"
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

packages\coverlet "JeremyAnsel.IO.DiscLocator.Tests\bin\%Configuration%\netcoreapp3.0\JeremyAnsel.IO.DiscLocator.Tests.dll" --target "dotnet" --targetargs "test JeremyAnsel.IO.DiscLocator.Tests -f netcoreapp3.0 --no-build" --output "bld\coverage\results-netcoreapp3.0.xml" --merge-with "bld\coverage\results.json" --format opencover
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

del "bld\coverage\results.json"

packages\coverlet "JeremyAnsel.IO.Locator.Tests\bin\%Configuration%\net452\JeremyAnsel.IO.Locator.Tests.dll" --target "dotnet" --targetargs "test JeremyAnsel.IO.Locator.Tests -f net452 --no-build" --output "bld\coverage\results.json"
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

packages\coverlet "JeremyAnsel.IO.DiscLocator.Tests\bin\%Configuration%\net452\JeremyAnsel.IO.DiscLocator.Tests.dll" --target "dotnet" --targetargs "test JeremyAnsel.IO.DiscLocator.Tests -f net452 --no-build" --output "bld\coverage\results-net452.xml" --merge-with "bld\coverage\results.json" --format opencover
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

del "bld\coverage\results.json"

packages\reportgenerator -reports:"bld\coverage\results-netcoreapp3.0.xml;bld\coverage\results-net452.xml" -reporttypes:Html;Badges -targetdir:bld\coverage -verbosity:Info
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%
