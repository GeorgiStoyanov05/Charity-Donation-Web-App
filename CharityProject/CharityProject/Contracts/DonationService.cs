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
        private readonly UserManager<User> userManager;

        public DonationService(ApplicationDbContext _context, UserManager<User> userManager)
        {
            this.context = _context;
            this.userManager = userManager;
        }
        public async Task<Charity> MakeDonationToCharity(Donation donation, Charity charity)
        {
            Charity extendedCharity = context.Charities.Where(c => c.Id == charity.Id).Include(c => c.Donations).ToList()[0];
            extendedCharity.Donations.Add(donation);
            context.Charities.Update(extendedCharity);
            await context.SaveChangesAsync();
            return extendedCharity;
        }
    }
}
