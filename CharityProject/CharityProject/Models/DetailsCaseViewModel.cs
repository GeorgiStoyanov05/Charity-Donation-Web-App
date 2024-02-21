using Charities.Data.Models;

namespace CharityProject.Models
{
    public class DetailsCaseViewModel
    {
        public List<Charity> Charities { get; set; } = null!;

        public Charity Charity { get; set; } = null!;

        public User User { get; set; } = null!;

        public Comment Comment { get; set; } = null!;

        public Donation Donation { get; set; } = null!;

        public Update Update { get; set; } = null!;
    }
}
