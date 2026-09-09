using System.ComponentModel.DataAnnotations;

namespace EmployeeMVC.Models;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Contact number is required")]
    [Phone(ErrorMessage = "Enter a valid phone number")]
    [Display(Name = "Contact No")]
    public string ContactNo { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "Joined date is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Joined Date")]
    public DateTime JoinedDate { get; set; } = DateTime.Today;
}
