using System.ComponentModel.DataAnnotations;

namespace StudentWorshopPortal.Models.ViewModels;

public class StudentRegisterViewModel
{
    [Required(ErrorMessage = "Student ID is required.")]
    [RegularExpression(
        @"^[A-Za-z0-9]{6,12}$",
        ErrorMessage = "Student ID must contain 6 to 12 letters or numbers."
    )]
    [Display(Name = "Student ID")]
    public string StudentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "Full name must be between 2 and 100 characters."
    )]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [StringLength(
        100,
        MinimumLength = 6,
        ErrorMessage = "Password must be at least 6 characters long."
    )]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm password is required.")]
    [DataType(DataType.Password)]
    [Compare(
        nameof(Password),
        ErrorMessage = "Password and confirm password must match."
    )]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}