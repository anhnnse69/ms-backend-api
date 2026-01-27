@echo off
setlocal enabledelayedexpansion

echo ========================================
echo  EF Core: Add New Migration
echo ========================================
echo.

:: Generate Timestamp (YYYYMMDD_HHMMSS)
set "datestamp=%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%"
set "timestamp=%TIME:~0,2%%TIME:~3,2%%TIME:~6,2%"
set "timestamp=%timestamp: =0%"
set "MIG_NAME=Mig_%datestamp%_%timestamp%"

echo [1/2] Creating migration: %MIG_NAME%...
dotnet ef migrations add %MIG_NAME% --project MS.Infrastructure --startup-project MS.API
if %errorlevel% neq 0 (
    echo.
    echo [ERROR] Failed to create migration!
    pause
    exit /b %errorlevel%
)

echo [2/2] Applying migration to database...
dotnet ef database update --project MS.Infrastructure --startup-project MS.API
if %errorlevel% neq 0 (
    echo.
    echo [ERROR] Failed to update database!
    pause
    exit /b %errorlevel%
)

echo.
echo [SUCCESS] Migration %MIG_NAME% created and applied.
echo ========================================
pause