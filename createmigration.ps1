$migr_name = Read-Host 'Enter name for migration?'
dotnet ef migrations add $migr_name
dotnet ef database update
