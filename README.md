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
  3. Rebuild, spustenie migrácií
```bash
dotnet ef database update
```
### 3. Spustenie aplikácie
