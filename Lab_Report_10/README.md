# Lab Report 10 — ADO.NET CRUD Operation Using Console Application

A console app that performs Create, Read, Update and Delete on a `Students`
table in SQL Server using raw ADO.NET (`SqlConnection`, `SqlCommand`,
`SqlDataReader`) — no ORM.

## Project layout

```
Lab_Report_10/
├── StudentCRUD.csproj
├── Program.cs
├── Database/schema.sql
└── README.md
```

## 0. Get SQL Server running (macOS / Apple Silicon)

SQL Server itself doesn't run natively on macOS, and `Trusted_Connection`
(Windows integrated auth) only works on Windows — so `Server=localhost;...
Trusted_Connection=True` will always fail with *"A network-related or
instance-specific error... error: 40"* on a Mac. Run SQL Server in Docker
instead, and connect with a SQL login:

```bash
# Requires Docker Desktop. azure-sql-edge is Microsoft's arm64-native image
# (works on M1/M2/M3); it's SQL-Server-wire-compatible.
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=YourStrong!Passw0rd" \
  -p 1433:1433 --name sql1 --hostname sql1 \
  -d mcr.microsoft.com/azure-sql-edge:latest
```

Pick your own password and use it consistently below. To run the SQL below
against the container, use **Azure Data Studio** (free, cross-platform GUI,
closest thing to SSMS on a Mac) or `sqlcmd` — connect to `localhost,1433`
with the `sa` login.

## 1. Create the database

Open `Database/schema.sql` in Azure Data Studio (or run it with `sqlcmd`) to
create `StudentDB` and the `Students` table.

## 2. Configure the connection

`Program.cs` has a `ConnectionString` constant near the top:

```csharp
private const string ConnectionString =
    "Server=localhost,1433;Database=StudentDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;";
```

Replace the password with whatever you passed to `docker run` above. If
you're on Windows with a real SQL Server / LocalDB instance instead, you can
swap back to `Server=.\SQLEXPRESS;Trusted_Connection=True;...`.

## 3. Run it

```bash
cd Lab_Report_10
dotnet restore
dotnet run
```

## Menu

```
1. Add Student     -> INSERT via ExecuteNonQuery()
2. View Students    -> SELECT via SqlDataReader
3. Update Student   -> UPDATE via ExecuteNonQuery()
4. Delete Student   -> DELETE via ExecuteNonQuery()
5. Exit
```

All queries use parameterized `SqlCommand` (`@Name`, `@Age`, ...) instead of
string concatenation, so user input can never be interpreted as SQL.

Age input is validated with a retry loop (`Please enter a valid age.` on
non-numeric input), and after Add/Update/Delete the app automatically
reprints the current `STUDENT LIST` so you can see the change take effect.

## Screenshots to capture for the report

- The console menu
- Output after adding a student
- Output of "View Students" listing rows
- Output after updating a student
- Output after deleting a student
