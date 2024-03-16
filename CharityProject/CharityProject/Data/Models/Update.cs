using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charities.Data.Models
{
    public class Update
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Update field is required.")]
        [StringLength(maximumLength: 200)]
        public string? Text { get; set; }

        public DateTime? DateCreated { get; set; }

        [Required]
        [ForeignKey("Charity")]
        public Guid CharityId { get; set; }
        public Charity? Charity { get; set; }

        [Required]
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
