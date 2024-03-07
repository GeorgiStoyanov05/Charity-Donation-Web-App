using Charities.Data.Models;

namespace CharityProject.Models
{
    public class AllCasesViewModel
    {
        public List<Charity> Charities { get; set; } = null!; 

        public bool DonatedCharities { get; set; } = false!;

        public string CharityName { get; set; } = null!;
    }
}
