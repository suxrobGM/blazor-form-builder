@echo off

cd ../src/FormBuilder.API
echo Applying migrations...
dotnet ef database update

echo Successfully applied migrations.
pause
