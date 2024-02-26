using Charities.Data.Models;

namespace CharityProject.Services
{
    public interface IDonationService
    {
        Task<Charity> MakeDonationToCharity(Donation donation, Charity charity);
    }
}
