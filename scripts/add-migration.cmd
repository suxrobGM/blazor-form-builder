@echo off

cd ../

:prompt
set "MigrationName="
set /p MigrationName="Enter Migration Name: "

if "%MigrationName%" == "" (
    echo Error: Migration name cannot be empty.
    goto prompt
)

echo Running migration for SQL Server...
dotnet ef migrations add %MigrationName% --project ../src/FormBuilder.API

echo Migrations completed.

echo Do you want to apply migrations (y/n):
set /p ApplyMigrationResult=

if /I "%ApplyMigrationResult%" == "y" (
    cd ./Scripts
	call ./apply-migration.cmd
)

pause
