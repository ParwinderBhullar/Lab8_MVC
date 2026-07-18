using System.ComponentModel.DataAnnotations;

namespace StudentWorshopPortal.Models.ViewModels;

public class StudentLoginViewModel
{
    [Required(ErrorMessage = "Student ID is required.")]
    [Display(Name = "Student ID")]
    public string StudentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}