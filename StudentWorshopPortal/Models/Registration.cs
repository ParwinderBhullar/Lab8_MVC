using System.ComponentModel.DataAnnotations;

namespace StudentWorshopPortal.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student full name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Student full name must be between 2 and 100 characters.")]
        [Display(Name = "Student Full Name")]
        public string StudentFullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Student number is required.")]
        [RegularExpression(@"^[A-Za-z0-9]{6,12}$", ErrorMessage = "Student number must contain 6 to 12 letters or digits.")]
        [Display(Name = "Student Number")]
        public string StudentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(120, ErrorMessage = "Email address cannot exceed 120 characters.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a workshop.")]
        [Display(Name = "Workshop Title")]
        public string WorkshopTitle { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string? Comments { get; set; }

        public int WorkshopId { get; set; }

        public Workshop Workshop { get; set; } = null!;

    }
}
