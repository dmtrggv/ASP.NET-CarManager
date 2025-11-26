# CarManager

## Overview
CarManager is a **university project** built using ASP.NET Core.  
The application provides a simple system for managing cars — adding, editing, deleting, and viewing car records.

## Technologies Used
- ASP.NET Core MVC / Razor Pages  
- Entity Framework Core  
- SQL Server (or another EF-compatible database)  
- C#  
- HTML / CSS / JavaScript  

## Project Structure
- `CarManager.sln` — Visual Studio solution  
- `CarManager/` — main project folder containing controllers, models, views, data context, and configuration  
- Entity Framework Core migrations stored in the `Migrations/` folder  

## Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/dmtrggv/ASP.NET-CarManager.git

### 2. Make migrations
```bash
dotnet clean
dotnet build
Migration-Add TestMigrationName
Update-Database
