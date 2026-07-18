using System.ComponentModel.DataAnnotations;

namespace StudentWorshopPortal.Models
{
    public class Workshop : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Workshop title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Workshop title must be between 3 and 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Instructor name is required.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Instructor name must be between 2 and 80 characters.")]
        public string Instructor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Workshop date is required.")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Location must be between 2 and 80 characters.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string Description { get; set; } = string.Empty;

        public ICollection<Registration> Registrations { get; set; }
        = new List<Registration>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date.HasValue && Date.Value.Date < DateTime.Today)
            {
                yield return new ValidationResult(
                    "Workshop date cannot be in the past.",
                    new[] { nameof(Date) });
            }
        }
    }
}
