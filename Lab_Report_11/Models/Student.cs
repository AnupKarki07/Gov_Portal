using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 120, ErrorMessage = "Enter a valid age")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Enter a valid phone number")]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Course is required")]
    [StringLength(100)]
    public string Course { get; set; } = string.Empty;

    [Required(ErrorMessage = "Batch is required")]
    [StringLength(50)]
    public string Batch { get; set; } = string.Empty;

    [Range(1900, 2100, ErrorMessage = "The field Joined Year must be between 1900 and 2100.")]
    [Display(Name = "Joined Year")]
    public int JoinedYear { get; set; }
}
