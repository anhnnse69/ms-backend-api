@echo off
echo ========================================
echo  Starting Docker Compose
echo ========================================
echo.

echo [1/2] Building Docker images...
docker-compose build
if %errorlevel% neq 0 (
    echo.
    echo [ERROR] Build failed!
    echo ========================================
    pause
    exit /b %errorlevel%
)
echo [SUCCESS] Build completed.
echo.

echo [2/2] Starting containers...
docker-compose up -d
if %errorlevel% neq 0 (
    echo.
    echo [ERROR] Failed to start containers!
    echo ========================================
    pause
    exit /b %errorlevel%
)
echo [SUCCESS] Containers started.
echo.

echo ========================================
echo Container Status:
echo ========================================
docker-compose ps
echo.

echo ========================================
echo Following logs... (Press Ctrl+C to exit)
echo ========================================
docker-compose logs -f

pause