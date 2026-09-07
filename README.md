# TaskFlow API

A RESTful Task Management API built with **ASP.NET Core 10** and designed using **Clean Architecture**, **CQRS**, and **MediatR**.

TaskFlow allows authenticated users to create and manage projects, organize tasks within those projects, and track task status, priority, and due dates.

---

## ✨ Features

### 🔐 Authentication

* User registration
* User login
* Password hashing with BCrypt
* JWT Bearer authentication
* JWT claims for user identity, email, and name

### 📁 Project Management

* Create projects
* Retrieve the authenticated user's projects
* Associate projects with their owner
* Track project creation time

### ✅ Task Management

* Create tasks inside projects
* Retrieve tasks by project
* Update tasks
* Delete tasks
* Task status tracking
* Task priority levels
* Optional due dates

### 🛡️ Validation

* FluentValidation
* User registration validation
* Email validation
* Password length validation
* Project validation
* Task validation

### 🏗️ Architecture & Design

* Clean Architecture
* CQRS
* MediatR
* Repository Pattern
* Unit of Work Pattern
* Dependency Injection
* DTO-based API contracts
* Entity Framework Core
* SQL Server

### 📚 API Documentation

* Swagger / OpenAPI
* JWT authentication support in Swagger UI

### 🌐 Other

* CORS configuration for frontend applications
* EF Core migrations
* Nullable reference types
* Global using support through .NET implicit usings

---

## 🏛️ Architecture

TaskFlow is organized into four main layers:

```text
                        ┌─────────────────────┐
                        │    TaskFlow.WebAPI  │
                        │   Controllers / API │
                        └──────────┬──────────┘
                                   │
                                   ▼
                     ┌─────────────────────────┐
                     │   TaskFlow.Application  │
                     │ CQRS / Handlers / DTOs  │
                     │ Validators / Mapping    │
                     └───────────┬─────────────┘
                                 │
                                 ▼
                       ┌───────────────────┐
                       │  TaskFlow.Domain  │
                       │ Entities / Enums  │
                       │ Interfaces        │
                       └─────────▲─────────┘
                                 │
                                 │
                    ┌────────────┴────────────┐
                    │ TaskFlow.Infrastructure │
                    │ EF Core / SQL Server    │
                    │ Repository / UnitOfWork │
                    │ JWT                     │
                    └─────────────────────────┘
```

### Project Structure

```text
TaskFlow
│
├── TaskFlow.Application
│   ├── Common
│   ├── DTOs
│   ├── Features
│   │   ├── Auth
│   │   ├── Projects
│   │   └── Task
│   ├── Mapping
│   ├── Validator
│   └── DependencyInjection.cs
│
├── TaskFlow.Domain
│   ├── Entities
│   ├── Enum
│   ├── Interfaces
│   └── Dependency-free domain layer
│
├── TaskFlow.Infrastructure
│   ├── Data
│   ├── Migrations
│   ├── Repositories
│   ├── Services
│   └── DependencyInjection.cs
│
├── TaskFlow.WebAPI
│   ├── Controllers
│   ├── Properties
│   ├── Program.cs
│   └── appsettings.json
│
└── TaskFlow.sln
```

---

## 🧩 Core Domain

TaskFlow currently contains three main entities:

```text
User
 │
 └── Projects
      │
      └── Tasks
```

### User

A user contains:

* `Id`
* `FullName`
* `Email`
* `PasswordHash`

### Project

A project contains:

* `Id`
* `Name`
* `Description`
* `CreatedAt`
* `UserId`
* `Tasks`

### Task

A task contains:

* `Id`
* `Title`
* `Description`
* `Status`
* `Priority`
* `CreatedAt`
* `DueDate`
* `ProjectId`

---

## 🔄 Task Status

Tasks support three states:

```text
Todo
  ↓
InProgress
  ↓
Done
```

## 🚦 Task Priority

Three priority levels are available:

```text
Low
Medium
High
```

---

## 🔑 Authentication Flow

The authentication flow is based on JWT Bearer tokens.

```text
Register
   │
   ▼
Create User
   │
   ▼
Hash Password with BCrypt
   │
   ▼
Generate JWT
   │
   ▼
Return Token
```

Login follows a similar flow:

```text
Login
  │
  ▼
Find User
  │
  ▼
Verify Password
  │
  ▼
Generate JWT
  │
  ▼
Return Token
```

Authenticated requests must provide:

```http
Authorization: Bearer <token>
```

---

## 📡 API Endpoints

### Authentication

| Method | Endpoint             | Description         | Auth |
| ------ | -------------------- | ------------------- | ---- |
| POST   | `/api/Auth/register` | Register a new user | ❌    |
| POST   | `/api/Auth/login`    | Authenticate a user | ❌    |

### Projects

| Method | Endpoint        | Description                                      | Auth |
| ------ | --------------- | ------------------------------------------------ | ---- |
| GET    | `/api/Projects` | Get projects belonging to the authenticated user | ✅    |
| POST   | `/api/Projects` | Create a new project                             | ✅    |

### Tasks

| Method | Endpoint                                   | Description             | Auth |
| ------ | ------------------------------------------ | ----------------------- | ---- |
| GET    | `/api/projects/{projectId}/tasks`          | Get tasks for a project | ✅    |
| POST   | `/api/projects/{projectId}/tasks`          | Create a task           | ✅    |
| PUT    | Task update endpoint                       | Update a task           | ✅    |
| DELETE | `/api/projects/{projectId}/tasks/{taskId}` | Delete a task           | ✅    |

> API routes may evolve as the project continues to be developed.

---

## 🗄️ Database

TaskFlow uses:

* **Entity Framework Core**
* **SQL Server**
* **EF Core Migrations**

The main database relationships are:

```text
Users
  │
  │ 1:N
  ▼
Projects
  │
  │ 1:N
  ▼
TaskItems
```

Cascade delete is configured between:

```text
User → Projects
Project → TaskItems
```

---

## 📦 Technologies

| Technology            | Purpose                               |
| --------------------- | ------------------------------------- |
| ASP.NET Core 10       | Web API                               |
| C#                    | Programming language                  |
| Entity Framework Core | ORM                                   |
| SQL Server            | Database                              |
| MediatR               | CQRS / request handling               |
| FluentValidation      | Request validation                    |
| AutoMapper            | Object mapping                        |
| BCrypt.Net            | Password hashing                      |
| JWT                   | Authentication                        |
| Swagger / OpenAPI     | API documentation                     |
| Repository Pattern    | Data access abstraction               |
| Unit of Work          | Transaction / persistence abstraction |

---

## ⚙️ Getting Started

### Prerequisites

Make sure you have installed:

* .NET 10 SDK
* SQL Server
* Git

### 1. Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/TaskFlow-API.git
cd TaskFlow
```

### 2. Configure the database

Update the connection string in your local configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TaskFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Configure JWT

Provide a secure JWT signing key through your local configuration or .NET User Secrets.

Example configuration:

```json
{
  "Jwt": {
    "Key": "YOUR_SECURE_SECRET_KEY",
    "Issuer": "TaskFlowAPI",
    "Audience": "TaskFlowClient"
  }
}
```

### 4. Apply EF Core migrations

From the solution directory:

```bash
dotnet ef database update \
  --project TaskFlow.Infrastructure \
  --startup-project TaskFlow.WebAPI
```

On Windows CMD:

```cmd
dotnet ef database update --project TaskFlow.Infrastructure --startup-project TaskFlow.WebAPI
```

### 5. Run the API

```bash
dotnet run --project TaskFlow.WebAPI
```

### 6. Open Swagger

After starting the API, open the Swagger UI at the URL shown by ASP.NET Core.

Use the generated JWT token from the login endpoint to authorize protected endpoints.

---

## 🧪 Example Authentication Request

### Register

```http
POST /api/Auth/register
Content-Type: application/json

{
  "fullName": "John Doe",
  "email": "john@example.com",
  "password": "Password123"
}
```

### Login

```http
POST /api/Auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "Password123"
}
```

Example response:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "fullName": "John Doe"
}
```

---

## 🧠 CQRS Structure

TaskFlow separates commands and queries using MediatR.

### Commands

```text
RegisterUserCommand
CreateProjectCommand
CreateTaskCommand
UpdateTaskCommand
DeleteTaskCommand
```

### Queries

```text
LoginUserQuery
GetUserProjectsQuery
GetTasksByProjectQuery
```

Each request is handled by a dedicated handler in the Application layer.

---

## 🔌 Dependency Injection

Each layer exposes its own dependency registration:

```csharp
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
```

This keeps the composition root in the Web API layer while allowing Infrastructure and Application dependencies to remain isolated.

---

## 🛠️ Current Project Scope

TaskFlow is currently focused on the backend/API side of a task management application.

The project is structured as a learning and portfolio-oriented implementation of:

* Clean Architecture
* CQRS
* Mediator-based request handling
* Authentication
* Data access abstraction
* API validation
* Relational data modeling

Additional features and improvements can be added as the project evolves.

---

## 🚀 Future Improvements

Potential future improvements include:

* Project ownership authorization checks
* More granular authorization policies
* Pagination and filtering
* Better exception handling with global middleware
* Improved query projections
* Automated tests
* Integration tests
* Refresh tokens
* Role-based authorization
* Docker support
* CI/CD pipeline
* Production-ready configuration
* Frontend integration

---

## 📄 License

This project is currently intended for educational and portfolio purposes.

A formal open-source license can be added in the future.
