@echo off
setlocal

cd "%~dp0"

if '%Configuration%' == '' if not '%1' == '' set Configuration=%1
if '%Configuration%' == '' set Configuration=Debug

if exist build\coverage rd /s /q build\coverage
md build\coverage

packages\OpenCover.4.7.922\tools\OpenCover.Console.exe -register:path64 -output:build\coverage\results.xml -target:packages\xunit.runner.console.2.0.0\tools\xunit.console.exe -targetargs:"JeremyAnsel.IO.Locator.Tests\bin\%Configuration%\JeremyAnsel.IO.Locator.Tests.dll JeremyAnsel.IO.DiscLocator.Tests\bin\%Configuration%\JeremyAnsel.IO.DiscLocator.Tests.dll -noshadow" "-filter:+[JeremyAnsel.IO.Locator]* +[JeremyAnsel.IO.DiscLocator]* -[*.Tests]*" -hideskipped:File;Filter;Attribute -returntargetcode
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

packages\ReportGenerator.4.2.20\tools\net47\ReportGenerator.exe -reports:build\coverage\results.xml -reporttypes:Html;Badges -targetdir:build\coverage -verbosity:Info
