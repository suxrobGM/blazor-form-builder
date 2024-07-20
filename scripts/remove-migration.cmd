@echo off

cd ../
echo Do you want to remove the latest migration (y/n):
set /p UserPromptResult=

if /I "%UserPromptResult%" == "y" (
    echo Removing the latest migration from PostgreSQL...
    dotnet ef migrations remove --project ../src/FormBuilder.API
)

echo Removed the latest migration
pause
