# Lab Report 9 — Model Binding in ASP.NET Core MVC

An ASP.NET Core MVC app that demonstrates model binding: an HTML form posts
to an action, ASP.NET Core binds the posted fields to a strongly-typed
`Employee` model, `ModelState` validates it, and the result is rendered back
through a view.

## Project layout

```
Lab_Report_9/
├── EmployeeMVC.csproj
├── Program.cs
├── appsettings.json
├── Models/Employee.cs
├── Controllers/EmployeeController.cs
├── Views/
│   ├── _ViewImports.cshtml
│   ├── _ViewStart.cshtml
│   ├── Shared/_Layout.cshtml
│   └── Employee/
│       ├── Create.cshtml
│       └── Result.cshtml
└── wwwroot/css/site.css
```

## Employee model (`Models/Employee.cs`)

`Id, Name, Email, Age, ContactNo, Salary, JoinedDate` — with `[Required]`,
`[EmailAddress]`, `[Range]` and `[Phone]` data annotations so both
client-side and server-side validation work.

## How model binding flows

1. `GET /Employee/Create` renders the empty form (`Views/Employee/Create.cshtml`).
2. Submitting the form sends a `POST` to `EmployeeController.Create(Employee employee)`.
   ASP.NET Core's default model binder maps each form field
   (`Name`, `Email`, `Age`, `ContactNo`, `Salary`, `JoinedDate`) onto the
   matching `Employee` property automatically — no manual parsing required.
3. `ModelState.IsValid` is checked. If validation fails, the same view is
   redisplayed with the entered values and validation messages. If it
   passes, the controller returns the `Result` view with the bound model,
   showing everything the user submitted.

## Run it

```bash
cd Lab_Report_9
dotnet restore
dotnet run
```

Then open the URL shown in the console (e.g. `https://localhost:5001`) —
it routes straight to `Employee/Create`.

## Screenshots to capture for the report

- The empty employee input form
- The form showing a validation error (e.g. submit with empty Name)
- The `Result` page after a successful submission
