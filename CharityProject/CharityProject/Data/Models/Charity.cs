using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charities.Data.Models
{
    public class Charity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string? Name { get; set; }

        
        [Required]
        [ForeignKey("Creator")]
        public string? CreatorId { get; set; }
        public Creator? Creator { get; set; } 
        
        
        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Location { get; set; }

        //TODO: Updates
        public ICollection<Update> Updates { get; set; }

        [Required]
        [Range(1.00, double.MaxValue)]
        public double? FundsNeeded { get; set; }

        //TODO: FundsDonated
        public ICollection<Donation> Donations { get; set; }

        [Required]
        [Url]
        public string? ImageUrl { get; set; }

        //TODO: Comments
        public ICollection<Comment>? Comments { get; set; }

        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        [Required]
        public Category? Category { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsRejected { get; set; } = false;

        public bool IsDeleted { get; set; } = false;
    }
}
