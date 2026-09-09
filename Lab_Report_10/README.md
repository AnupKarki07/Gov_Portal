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

## 1. Create the database

Open `Database/schema.sql` in SQL Server Management Studio (or run it with
`sqlcmd`) to create `StudentDB` and the `Students` table.

## 2. Configure the connection

`Program.cs` has a `ConnectionString` constant near the top:

```csharp
private const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";
```

Update `Server=` to your SQL Server instance name (e.g. `.\SQLEXPRESS`), or
swap to SQL authentication (`User Id=...;Password=...;`) if you're not using
Windows auth.

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

## Screenshots to capture for the report

- The console menu
- Output after adding a student
- Output of "View Students" listing rows
- Output after updating a student
- Output after deleting a student
