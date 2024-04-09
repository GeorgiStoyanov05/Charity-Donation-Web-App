using Charities.Data.Models;
using CharityProject.Data;
using CharityProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CharityProject.Contracts
{
    public class DonationService : IDonationService
    {
        private readonly ApplicationDbContext context;

        public DonationService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public async Task<Charity> MakeDonationToCharity(Donation donation, Charity charity)
        {
            try
            {
                Charity extendedCharity = context.Charities.Where(c => c.Id == charity.Id).Include(c => c.Donations).ToList()[0];
                if (extendedCharity == null)
                {
                    throw new ArgumentNullException("Charity not found.");
                }
                if (donation.Amount < 1)
                {
                    throw new ArgumentOutOfRangeException("Donation amount cannot be less than 1.00$");
                }
                extendedCharity.Donations.Add(donation);
                context.Charities.Update(extendedCharity);
                await context.SaveChangesAsync();
                return extendedCharity;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
