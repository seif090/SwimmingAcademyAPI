# 🏊‍♂️ Swimming Academy Management System - Web API

A professional ASP.NET Web API backend for managing a swimming academy with full database-first architecture. This system supports managing swimmers, schools, pre-teams, coaches, users, and detailed role-based actions. Built using stored procedures and SQL functions for maximum performance and integrity.

---

## 📚 Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Technologies](#technologies-used)
- [Database Schema](#database-schema)
- [Stored Procedures](#stored-procedures)
- [API Endpoints](#api-endpoints)
- [Setup Instructions](#setup-instructions)
- [Project Structure](#project-structure)
- [Screenshots](#screenshots)
- [License](#license)

---

## ✨ Features

- 🔐 Secure role-based authorization: Admin, SuperAdmin, Swimmer, Parent, Coach.
- 👨‍👩‍👧 Swimmer enrollment, level tracking, and attendance.
- 🏫 School and PreTeam session management.
- 📦 Invoice generation and registration logic.
- 🗂 Full CRUD using stored procedures — no EF migrations.
- 📜 Centralized logging of all actions.
- 🧠 Action-based privileges via `Users_Priv`, `AppCodes`, and `Actions`.

---

## 🏗 Architecture

- **Architecture**: Database-First
- **Business Logic**: SQL Stored Procedures and Functions
- **No Services Layer**: Direct Repository → Controller access
- **Data Access**: ADO.NET with raw commands and data readers

---

## 🛠 Technologies Used

- ASP.NET Core Web API (.NET 8)
- Microsoft SQL Server
- ADO.NET (no Entity Framework migrations)
- Swagger / Swashbuckle
- AutoMapper (optional)
- Visual Studio 2022

---

## 🗃 Database Schema

> Database: `SwimminAcadmy`

Key Tables:
- `Swimmers.Info`
- `Swimmers.Parent`, `Swimmers.Technical`, `Swimmers.Log`
- `Schools.Info`, `Schools.Details`, `Schools.Log`
- `PreTeam.Info`, `PreTeam.Details`, `PreTeam.Log`
- `AppCodes`, `Users`, `Users_Priv`, `Actions`
- `InvTrans`, `Trans`

---

## 📜 Stored Procedures

Key Modules:

### Swimmers:
- `Add_Swimmer`
- `UpdateSwimmerLevel`
- `SavePteam_Trans`, `SaveSchool_Trans`
- `ViewPossible_Pteam`, `ViewPossible_School`

### PreTeam:
- `Create_PTeam`, `Updated`, `EndPreTeam`
- `ShowPTeam`, `SearchActions`, `PTeamDetails_tab`

### Schools:
- `Create_School`, `Updated`, `EndSchool`
- `ShowSchool`, `SchoolDetalis_Tab`, `SearchActions`

> All logic is encapsulated in stored procedures to maintain data integrity and traceability.

---

## 🔌 API Endpoints (Examples)

| Endpoint | Method | Description |
|---------|--------|-------------|
| `/api/swimmers/add` | `POST` | Add a new swimmer |
| `/api/swimmers/update-level` | `PUT` | Update swimmer level |
| `/api/swimmers/{id}/technical-tab` | `GET` | Get swimmer’s technical tab info |
| `/api/schools/show` | `POST` | Show school data (filter) |
| `/api/preteam/search` | `POST` | Search PreTeam info |

> For full list, open `/swagger/index.html` after running the project.

---

## ⚙️ Setup Instructions

### Prerequisites
- Microsoft SQL Server installed
- Visual Studio 2022 or newer
- .NET SDK 8 installed

### Steps

1. Clone the repo:

git clone https://github.com/your-username/swimming-academy-api.git
cd swimming-academy-api
Import the SQL file:

Use GS-LastVersion.sql to create the full database.

Modify appsettings.json with your connection string.

Build and run the Web API:
dotnet build
dotnet run

Open in browser:
https://localhost:5001/swagger


