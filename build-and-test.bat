@echo off
REM Build the solution, run tests, and treat warnings as errors for linting
cd /d %~dp0src

dotnet build Tardis.sln --no-restore -warnaserror
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet test Tardis.sln --no-build
