using System.ComponentModel.DataAnnotations;

namespace CharityProject.Models
{
    public class CreateCaseViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        public double? FundsNeeded { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [Url]
        public string? ImageUrl { get; set; }
    }
}
