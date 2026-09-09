# Net-Centric Computing — Lab Projects

Three independent, ready-to-run projects, one per lab assignment. Each has
its own `.csproj` and README with setup/run instructions and a list of
screenshots to capture for the report. They don't share a solution file on
purpose — each is meant to stand alone (and be submitted to its own repo per
the lab instructions).

| Folder | Lab | Stack |
|---|---|---|
| [`Lab_Report_9/`](Lab_Report_9) | Model Binding in ASP.NET Core MVC | ASP.NET Core MVC |
| [`Lab_Report_10/`](Lab_Report_10) | ADO.NET CRUD Operation Using Console Application | .NET Console + ADO.NET (SQL Server) |
| [`Lab_Report_11/`](Lab_Report_11) | Implementing EF Core in ASP.NET Core MVC for Student Record Management | ASP.NET Core MVC + EF Core (Code First, SQL Server) |

## Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB, Express, or full) for Lab 10 and Lab 11
- For Lab 11: `dotnet tool install --global dotnet-ef`

## Quick start

```bash
# Lab 9 — no database needed
cd Lab_Report_9 && dotnet restore && dotnet run

# Lab 10 — run Database/schema.sql first, then:
cd Lab_Report_10 && dotnet restore && dotnet run

# Lab 11 — set the connection string in appsettings.json, then:
cd Lab_Report_11 && dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

See each project's own README for full details.
