using System.ComponentModel.DataAnnotations;

namespace ModelBindingDemo.Models;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Contact number is required")]
    [Phone(ErrorMessage = "Enter a valid phone number")]
    [Display(Name = "Contact No")]
    public string ContactNo { get; set; } = string.Empty;

    [Range(0, 100000000, ErrorMessage = "Salary must be between 0 and 100000000")]
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "Joined date is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Joined Date")]
    public DateTime JoinedDate { get; set; } = DateTime.Today;
}
