using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charities.Data.Models
{
    public class Donation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey("Charity")]
        public Guid CharityId { get; set; }
        public Charity? Charity { get; set; }

        [Required]
        [Range(1.00, double.MaxValue)]
        public double Amount { get; set; }

        [Required]
        public DateTime DateMade { get; set; }

        public string Comment { get; set; } = "";

        [Required]
        public bool IsAnonymous { get; set; } = false;
    }
}
