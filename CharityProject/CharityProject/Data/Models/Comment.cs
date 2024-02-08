using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charities.Data.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [StringLength(maximumLength:200)]
        public string? Text { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }

        [Required]
        [ForeignKey("Charity")]
        public Guid CharityId { get; set; }
        public Charity? Charity { get; set; }
    }
}
