@echo off

cd ../
echo Applying migrations...
dotnet ef database update

echo Successfully applied migrations.
pause
