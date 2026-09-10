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

## 1. Configure the connection string

Edit `appsettings.json`:

```json
"DefaultConnection": "Server=localhost;Database=StudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

Change `Server=` to your SQL Server instance, or switch to SQL auth if
needed.

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
