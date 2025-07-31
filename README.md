# 🎓 CQRS University System

![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Tests](https://img.shields.io/badge/tests-passing-brightgreen)
![Architecture](https://img.shields.io/badge/architecture-Clean--Arch-blueviolet)
![Status](https://img.shields.io/badge/status-active-success)
![Coverage](https://img.shields.io/badge/coverage-90%25-success)

A personal .NET 8 Web API project that manages university operations (students, courses, departments, and authentication) using **CQRS**, **MediatR**, **EF Core**, and **ASP.NET Identity**. Built with Clean Architecture (4-Tier) for scalability, maintainability, and separation of concerns.

---

## 📚 Technologies Used

- ✅ .NET 8
- ✅ CQRS Pattern
- ✅ MediatR
- ✅ Entity Framework Core
- ✅ ASP.NET Identity
- ✅ JWT Token Authentication
- ✅ FluentValidation
- ✅ Swagger UI
- ✅ Visual Studio 2022

---

## 🧱 Project Architecture

```
📁 CQRSUniversitySystem.API
├── Features/
│ ├── Auth/
│ │ └── Commands/
│ │ ├── Login/
│ │ └── Register/
│ ├── Courses/
│ │ ├── Commands/
│ │ └── Queries/
│ ├── Departments/
│ │ ├── Commands/
│ │ └── Queries/
│ └── Students/
│ ├── Commands/
│ └── Queries/
├── Application/
│ ├── Abstractions/
│ │ ├── ICommand.cs
│ │ ├── ICommandHandler.cs
│ │ ├── IQuery.cs
│ │ ├── IQueryHandler.cs
│ │ ├── ITokenService.cs
│ │ ├── IBaseRepository.cs
│ │ ├── ICourseRepository.cs
│ │ ├── IDepartmentRepository.cs
│ │ ├── IStudentRepository.cs
│ │ └── IUnitOfWork.cs
├── Middleware/
│ └── GlobalExceptionHandler.cs
├── Filters/
│ └── ExecutionTimeActionFilter.cs

```
---

## ✨ Features

- ✅ **Authentication**
  - Register and Login with JWT
  - Role seeding: Admin and System roles auto-created on startup
- ✅ **Courses**
  - Create, remove, get by ID, get students
- ✅ **Departments**
  - Create, remove, list all, get students/courses
- ✅ **Students**
  - Create, remove, filter, get by ID, get enrolled courses
- ✅ **Global Exception Handling**
- ✅ **Execution Time Logging** using custom Action Filter
- ✅ **Custom Mapping**
- ✅ **Swagger UI** for API exploration

---

## 🧪 Future Improvements

- ✅ Automate validation handling inside **CQRS Handlers**
- ✅ Add full **Unit Tests** using **xUnit** and **FakeItEasy**

---

## 🛠 How to Run

1. Clone the repo

   ```bash
   git clone https://github.com/your-username/CQRS-UniversitySystem.git
   cd CQRS-UniversitySystem
2. Update the DB connection in appsettings.json.

3. Run migrations
```
dotnet ef database update
```

✅ Swagger docs available at https://cqrs-university.runasp.net/index.html

---

## Requirements
- Visual Studio 2022 or newer

- SQL Server

- .NET 8 SDK

- EF Core Tools

---

## 🖼️ Screenshots
- Screenshots will be added in /screenshots/ folder (coming soon)

---

## 👤 Author
Mohamed Eltorky
.NET Backend Developer
📫 Contact: [m.eltorky1014@gmail.com]
