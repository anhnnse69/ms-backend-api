@echo off
setlocal

echo ========================================
echo  Docker Compose Restart Script
echo ========================================
echo.
echo Select an option:
echo 1. Restart (down + build + up)
echo 2. Restart without rebuild (down + up)
echo 3. Full clean restart (down + prune + build + up)
echo 4. Exit
echo.

set /p choice=Enter your choice (1-4): 

if "%choice%"=="1" goto restart_build
if "%choice%"=="2" goto restart_no_build
if "%choice%"=="3" goto full_clean
if "%choice%"=="4" goto end
echo Invalid choice!
pause
exit /b 1

:restart_build
echo.
echo [Restart with build]
docker-compose down
docker-compose build
docker-compose up -d
goto show_logs

:restart_no_build
echo.
echo [Restart without build]
docker-compose down
docker-compose up -d
goto show_logs

:full_clean
echo.
echo [Full clean restart]
docker-compose down -v
docker system prune -f
docker-compose build --no-cache
docker-compose up -d
goto show_logs

:show_logs
echo.
echo Containers status:
docker-compose ps
echo.
echo Following logs... (Press Ctrl+C to exit)
docker-compose logs -f
goto end

:end
pause