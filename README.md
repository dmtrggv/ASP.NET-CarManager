# CarManager

## Overview
CarManager is a **university project** built using ASP.NET Core.  
The application provides a simple system for managing cars — adding, editing, deleting, and viewing car records.

## Technologies Used
- ASP.NET Core 9 (MVC / Razor Pages)
- Microsoft ASP.NET Core Identity
- Entity Framework Core 9  
  - SQLite Provider  
  - EF Core Tools & Design-time Services
- C#
- HTML / CSS / JavaScript
- .NET Code Generation Tools (Scaffolding)

## Project Structure
- `CarManager.sln` — Visual Studio solution  
- `CarManager/` — main project folder containing controllers, models, views, data context, and configuration  
- Entity Framework Core migrations stored in the `Migrations/` folder  

## Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/dmtrggv/ASP.NET-CarManager.git
```

### 2. Make migrations
```bash
PМ> dotnet clean
PМ> dotnet build
PМ> Add-Migration TestMigrationName
PМ> Update-Database
