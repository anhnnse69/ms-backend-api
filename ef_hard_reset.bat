@echo off
echo ========================================
echo  EF Core: HARD RESET (Database + Migrations)
echo ========================================
echo.
echo WARNING: This will DELETE your database and all migration history!
set /p confirm="Are you sure you want to proceed? (Y/N): "
if /i "%confirm%" neq "Y" goto end

echo.
echo [1/4] Dropping Database...
dotnet ef database drop --force --project MS.Infrastructure --startup-project MS.API

echo [2/4] Cleaning Migrations folder...
powershell -Command "Remove-Item -Path 'MS.Infrastructure\Migrations\*' -Recurse -Force"

echo [3/4] Creating Fresh Initial Migration...
dotnet ef migrations add Initial_Reset --project MS.Infrastructure --startup-project MS.API

echo [4/4] Updating Database to Initial State...
dotnet ef database update --project MS.Infrastructure --startup-project MS.API

echo.
echo [SUCCESS] Database and Migrations have been reset to Initial.
echo ========================================

:end
pause