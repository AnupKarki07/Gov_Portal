using Microsoft.Data.SqlClient;

namespace StudentCRUD;

internal static class Program
{
    // Update this to match your SQL Server instance before running.
    // macOS has no native SQL Server / Trusted_Connection (Windows integrated
    // auth) — run SQL Server via Docker and use SQL login auth instead. See
    // README.md for the Docker setup; replace the password below with yours.
    private const string ConnectionString =
        "Server=localhost,1433;Database=StudentDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;";

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
                    PrintHeader("ADD STUDENT");
                    AddStudent();
                    Pause();
                    ShowStudentList();
                    break;
                case "2":
                    ShowStudentList();
                    break;
                case "3":
                    PrintHeader("UPDATE STUDENT");
                    UpdateStudent();
                    Pause();
                    ShowStudentList();
                    break;
                case "4":
                    PrintHeader("DELETE STUDENT");
                    DeleteStudent();
                    Pause();
                    ShowStudentList();
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

    private static void PrintHeader(string title)
    {
        Console.WriteLine($"{new string('=', 10)} {title} {new string('=', 10)}");
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void AddStudent()
    {
        Console.Write("Enter student name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter student age: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age))
        {
            Console.WriteLine("Please enter a valid age.");
            Console.Write("Enter student age: ");
        }

        Console.Write("Enter student course: ");
        string? course = Console.ReadLine();

        const string query = "INSERT INTO Students (Name, Age, Course) VALUES (@Name, @Age, @Course)";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Age", age);
        command.Parameters.AddWithValue("@Course", course);

        connection.Open();
        int rows = command.ExecuteNonQuery();

        Console.WriteLine();
        Console.WriteLine(rows > 0 ? "Student added successfully." : "Failed to add student.");
    }

    private static void ShowStudentList()
    {
        PrintHeader("STUDENT LIST");
        ViewStudents();
        Pause();
    }

    private static void ViewStudents()
    {
        const string query = "SELECT Id, Name, Age, Course FROM Students ORDER BY Id";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);

        connection.Open();
        using var reader = command.ExecuteReader();

        Console.WriteLine();
        Console.WriteLine($"{"ID",-5}{"Name",-20}{"Age",-7}{"Course",-15}");
        Console.WriteLine(new string('-', 47));

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int age = reader.GetInt32(2);
            string course = reader.GetString(3);

            Console.WriteLine($"{id,-5}{name,-20}{age,-7}{course,-15}");
        }
    }

    private static void UpdateStudent()
    {
        Console.Write("Enter student ID to update: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter new student name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter new student age: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age))
        {
            Console.WriteLine("Please enter a valid age.");
            Console.Write("Enter new student age: ");
        }

        Console.Write("Enter new student course: ");
        string? course = Console.ReadLine();

        const string query = "UPDATE Students SET Name = @Name, Age = @Age, Course = @Course WHERE Id = @Id";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Age", age);
        command.Parameters.AddWithValue("@Course", course);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        int rows = command.ExecuteNonQuery();

        Console.WriteLine();
        Console.WriteLine(rows > 0 ? "Student updated successfully." : "No student found with that Id.");
    }

    private static void DeleteStudent()
    {
        Console.Write("Enter student ID to delete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        const string query = "DELETE FROM Students WHERE Id = @Id";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        int rows = command.ExecuteNonQuery();

        Console.WriteLine();
        Console.WriteLine(rows > 0 ? "Student deleted successfully." : "No student found with that Id.");
    }
}
