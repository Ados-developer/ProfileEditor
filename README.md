# Profile Editor (.NET 8 + ASP.NET Core Identity)

Tento projekt je ASP.NET Core aplikácia pre správu používateľského profilu.  
Používa **.NET 8**, **Entity Framework Core** a **SQL Server**.

---

## 🚀 Ako spustiť projekt

### 1. Klonovanie repozitára
```bash
git clone https://github.com/Ados-developer/ProfileEditor.git
cd ProfileEditor
```
### 2. Databáza
  1. v SQL Server Management Studio 20 vytvor novú databázu
  2. Nastav pripojenie k databáze v appsettings.json
  3. Rebuild projektu a spustenie migrácií
```bash
dotnet clean
dotnet build
dotnet ef database update
```
### 3. Spustenie aplikácie
```bash
dotnet run
```

## Potrebné k spusteniu
1. [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
2. [SQL Server](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
