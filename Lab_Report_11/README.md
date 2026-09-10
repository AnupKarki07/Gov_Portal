# Lab Report 11 — Implementing EF Core in ASP.NET Core MVC for Student Record Management

An ASP.NET Core MVC app using the EF Core **Code First** approach: a
`Student` model, an `ApplicationDbContext`, migrations to generate the
database, and full CRUD via `StudentController`.

## Project layout

```
Lab_Report_11/
├── StudentManagement.csproj
├── Program.cs
├── appsettings.json
├── Data/ApplicationDbContext.cs
├── Models/Student.cs
├── Controllers/StudentController.cs
├── Views/
│   ├── _ViewImports.cshtml
│   ├── _ViewStart.cshtml
│   ├── Shared/_Layout.cshtml
│   └── Student/
│       ├── Index.cshtml
│       ├── Create.cshtml
│       ├── Edit.cshtml
│       ├── Details.cshtml
│       └── Delete.cshtml
└── wwwroot/css/site.css
```

## Student model (`Models/Student.cs`)

`Id, Name, Age, Phone, Course, Batch, JoinedYear` with data annotations for
validation (`[Required]`, `[Range]`, `[Phone]`).

## 0. Get SQL Server running (macOS / Apple Silicon)

SQL Server doesn't run natively on macOS, and `Trusted_Connection` (Windows
integrated auth) only works on Windows — so the default connection string
will fail with *"A network-related or instance-specific error... error: 40"*
on a Mac. Run SQL Server in Docker instead, and connect with a SQL login:

```bash
# Requires Docker Desktop. azure-sql-edge is Microsoft's arm64-native image
# (works on M1/M2/M3); it's SQL-Server-wire-compatible.
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=YourStrong!Passw0rd" \
  -p 1433:1433 --name sql1 --hostname sql1 \
  -d mcr.microsoft.com/azure-sql-edge:latest
```

Pick your own password and use it consistently below.

## 1. Configure the connection string

Edit `appsettings.json`:

```json
"DefaultConnection": "Server=localhost,1433;Database=StudentManagementDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
```

Replace the password with whatever you passed to `docker run` above. If
you're on Windows with a real SQL Server / LocalDB instance instead, you can
swap back to `Server=.\SQLEXPRESS;Trusted_Connection=True;...`.

## 2. Install the EF Core CLI tool (once, machine-wide)

```bash
dotnet tool install --global dotnet-ef
```

## 3. Create and apply the migration

```bash
cd Lab_Report_11
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
```

This generates a `Migrations/` folder from the `Student` model and
`ApplicationDbContext`, then creates the `StudentManagementDB` database and
`Students` table in SQL Server.

## 4. Run the app

```bash
dotnet run
```

Open the printed URL — it routes to `Student/Index`.

## CRUD flow

| Action  | Controller method                          | View             |
|---------|---------------------------------------------|------------------|
| Create  | `Create()` GET / `Create(Student)` POST      | `Create.cshtml`  |
| Read    | `Index()`, `Details(id)`                     | `Index.cshtml`, `Details.cshtml` |
| Update  | `Edit(id)` GET / `Edit(id, Student)` POST    | `Edit.cshtml`    |
| Delete  | `Delete(id)` GET / `DeleteConfirmed(id)` POST | `Delete.cshtml`  |

All writes go through `ApplicationDbContext` (`Add`, `Update`, `Remove` +
`SaveChangesAsync()`) — EF Core translates these into the underlying SQL.

## Screenshots to capture for the report

- The `Students` table created in SQL Server after migration
- Student list page (`Index`)
- Add student form (`Create`)
- Edit student form (`Edit`)
- Student details page (`Details`)
- Delete confirmation page (`Delete`)
