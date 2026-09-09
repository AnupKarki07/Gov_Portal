using Microsoft.Data.SqlClient;

namespace StudentCRUD;

internal static class Program
{
    // Update this to match your SQL Server instance before running.
    private const string ConnectionString =
        "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

    private static void Main()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("===== Student Management (ADO.NET) =====");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Students");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    ViewStudents();
                    break;
                case "3":
                    UpdateStudent();
                    break;
                case "4":
                    DeleteStudent();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void AddStudent()
    {
        Console.Write("Enter Name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter Course: ");
        string? course = Console.ReadLine();

        Console.Write("Enter Email: ");
        string? email = Console.ReadLine();

        const string query = "INSERT INTO Students (Name, Age, Course, Email) " +
                              "VALUES (@Name, @Age, @Course, @Email)";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Age", age);
        command.Parameters.AddWithValue("@Course", course);
        command.Parameters.AddWithValue("@Email", (object?)email ?? DBNull.Value);

        connection.Open();
        int rows = command.ExecuteNonQuery();

        Console.WriteLine(rows > 0 ? "Student added successfully." : "Failed to add student.");
    }

    private static void ViewStudents()
    {
        const string query = "SELECT Id, Name, Age, Course, Email FROM Students ORDER BY Id";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);

        connection.Open();
        using var reader = command.ExecuteReader();

        Console.WriteLine();
        Console.WriteLine($"{"Id",-5}{"Name",-20}{"Age",-5}{"Course",-20}{"Email",-25}");
        Console.WriteLine(new string('-', 75));

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int age = reader.GetInt32(2);
            string course = reader.GetString(3);
            string email = reader.IsDBNull(4) ? "" : reader.GetString(4);

            Console.WriteLine($"{id,-5}{name,-20}{age,-5}{course,-20}{email,-25}");
        }
    }

    private static void UpdateStudent()
    {
        Console.Write("Enter Id of student to update: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter new Name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter new Age: ");
        int age = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter new Course: ");
        string? course = Console.ReadLine();

        Console.Write("Enter new Email: ");
        string? email = Console.ReadLine();

        const string query = "UPDATE Students " +
                              "SET Name = @Name, Age = @Age, Course = @Course, Email = @Email " +
                              "WHERE Id = @Id";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Age", age);
        command.Parameters.AddWithValue("@Course", course);
        command.Parameters.AddWithValue("@Email", (object?)email ?? DBNull.Value);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        int rows = command.ExecuteNonQuery();

        Console.WriteLine(rows > 0 ? "Student updated successfully." : "No student found with that Id.");
    }

    private static void DeleteStudent()
    {
        Console.Write("Enter Id of student to delete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        const string query = "DELETE FROM Students WHERE Id = @Id";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        int rows = command.ExecuteNonQuery();

        Console.WriteLine(rows > 0 ? "Student deleted successfully." : "No student found with that Id.");
    }
}
