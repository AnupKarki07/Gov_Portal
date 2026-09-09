-- Run this in SQL Server Management Studio (or sqlcmd) before running the app.

CREATE DATABASE StudentDB;
GO

USE StudentDB;
GO

CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Course NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL
);
GO
