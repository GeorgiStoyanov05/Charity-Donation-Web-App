using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charities.Data.Models
{
    public class Charity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
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


        [Required]
        [Range(1.00, double.MaxValue)]
        public double? FundsNeeded { get; set; }

        //TODO: FundsDonated

        [Required]
        [Url]
        public string? ImageUrl { get; set; }

        //TODO: Comments

        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsDeleted { get; set; } = false;
    }
}
