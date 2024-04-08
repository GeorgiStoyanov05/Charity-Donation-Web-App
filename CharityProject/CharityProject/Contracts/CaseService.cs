using Charities.Data.Models;
using CharityProject.Data;
using CharityProject.Models;
using CharityProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CharityProject.Contracts
{
    public class CaseService : ICaseService
    {
        private readonly ApplicationDbContext context;

        public CaseService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task<Charity> CreateCharity(CreateCaseViewModel model, string userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException("This user doesn't exist.");
            }
            var creator = context.Owners.Find(userId);
            if (creator == null)
            {
                creator = new Creator()
                {
                    Id = userId,
                    Username = user!.UserName,
                    Email = user.Email

                };
                await context.Owners.AddAsync(creator);
                await context.SaveChangesAsync();
            }
            Charity charity = new Charity()
            {
                Name = model.Name,
                CreatorId = creator.Id,
                Description = model.Description,
                Location = model.Location,
                FundsNeeded = model.FundsNeeded,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                IsDeleted = false,
                IsApproved = false
            };
            try
            {
                await context.Charities.AddAsync(charity);
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new ArgumentException("There was an error while creating this charity.");
            }
            return charity;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            List<Category> categories = await context.Categories.ToListAsync();
            return categories;
        }

        public async Task<List<Charity>> GetAllCharities()
        {
            List<Charity> charities = await context.Charities.Include(c => c.Donations).ThenInclude(d => d.User).Include(c => c.Updates).ThenInclude(d => d.User).Include(c => c.Category).Include(c => c.Comments)!.ThenInclude(d => d.User).ToListAsync();
            return charities;
        }

        public async Task<Charity> GetCharity(Guid id)
        {
            Charity charity = await context.Charities.Include(c => c.Comments).ThenInclude(c => c.User).Include(c => c.Donations).ThenInclude(c => c.User).Include(c => c.Updates).ThenInclude(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
            return charity;
        }

        public async Task<List<int>> ExtractCountData()
        {
            List<int> data;
            var donations = await context.Charities.Include(c => c.Donations).Select(c => c.Donations).ToListAsync();
            int totalDonationsCount = donations.Sum(d => d.Count);
            int totalFundsDonated = Convert.ToInt32(donations.Sum(d => d.Sum(d1 => d1.Amount)));
            int volunteers = context.Users.ToList().Count();
            int totalProjects = context.Charities.Where(c => c.IsApproved == true && c.IsDeleted == false).ToList().Count;
            data = new List<int>() { totalDonationsCount, totalFundsDonated, volunteers, totalProjects };
            return data;
        }

        public async Task<Charity> UpdateCharity(Charity charity)
        {
                context.Charities.Update(charity);
                await context.SaveChangesAsync();
            return charity;
        }
    }
}
