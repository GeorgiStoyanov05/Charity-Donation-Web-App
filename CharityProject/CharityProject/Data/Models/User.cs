using Microsoft.AspNetCore.Identity;

namespace Charities.Data.Models
{
    public class User:IdentityUser
    {
        public ICollection<Update>? Updates { get; set; }
        public ICollection<Donation>? Donations { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
