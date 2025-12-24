@echo off
:: Вмикаємо підтримку UTF-8 (щоб бачити емодзі та кирилицю)
chcp 65001 >nul
setlocal

:: --- НАЛАШТУВАННЯ ШЛЯХІВ ---
set "SOURCE_FILE=.env"
set "FRONTEND_DIR=.\frontend"
:: Перевір, чи це правильний шлях до папки з .csproj
set "BACKEND_DIR=.\Backend\track-list-api\track-list-api"

echo 🔄 Синхронізація .env файлів...

:: 1. Перевірка наявності головного файлу
if not exist "%SOURCE_FILE%" (
    echo ❌ Помилка: Файл .env не знайдено в корені!
    echo Спочатку створи файл .env.
    pause
    exit /b 1
)

:: 2. Копіювання у Фронтенд
if exist "%FRONTEND_DIR%" (
    copy /Y "%SOURCE_FILE%" "%FRONTEND_DIR%\.env" >nul
    echo ✅ Скопійовано в: %FRONTEND_DIR%
) else (
    echo ⚠️ Папку не знайдено: %FRONTEND_DIR%
)

:: 3. Копіювання у Бекенд
if exist "%BACKEND_DIR%" (
    copy /Y "%SOURCE_FILE%" "%BACKEND_DIR%\.env" >nul
    echo ✅ Скопійовано в: %BACKEND_DIR%
) else (
    echo ⚠️ Папку не знайдено: %BACKEND_DIR%
)

echo.
echo 🎉 Готово! Вікно закриється через 3 секунди...
timeout /t 3 >nul