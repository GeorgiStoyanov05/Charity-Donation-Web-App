using System.ComponentModel.DataAnnotations;

namespace Charities.Data.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<Charity>? Charities { get; set; }
    }
}
