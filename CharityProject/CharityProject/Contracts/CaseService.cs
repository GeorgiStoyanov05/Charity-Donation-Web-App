using Charities.Data.Models;
using CharityProject.Data;
using CharityProject.Models;
using CharityProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CharityProject.Contracts
{
    public class CaseService:ICaseService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public CaseService(ApplicationDbContext _context, UserManager<User> userManager)
        {
            this.context = _context;
            this.userManager = userManager;
        }

        public async Task<Charity> CreateCharity(CreateCaseViewModel model, string userId)
        {
            var user = await context.Users.FindAsync(userId);
            Creator creator = new Creator() 
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email

            };
            await context.Owners.AddAsync(creator);
            await context.SaveChangesAsync();
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

            await context.Charities.AddAsync(charity);
            await context.SaveChangesAsync();
            return charity;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            List<Category> categories = await context.Categories.ToListAsync();
            return categories;
        }

        public async Task<List<Charity>> GetAllCharities()
        {
            return await context.Charities.Include(c => c.Donations).Include(c=>c.Updates).Include(c=>c.Category).Include(c=>c.Comments).ToListAsync();
        }

        public async Task<Charity> GetCharity(Guid id)
        {
            return await context.Charities.Include(c => c.Comments).ThenInclude(c => c.User).Include(c => c.Donations).ThenInclude(c=>c.User).Include(c => c.Updates).ThenInclude(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Charity> AddCommentToCharity(Charity charity, Comment comment)
        {
            Charity extendedCharity = context.Charities.Where(c => c.Id == charity.Id).Include(c => c.Comments).ToList()[0];
            extendedCharity.Comments!.Add(comment);
            context.Charities.Update(charity);
            await context.SaveChangesAsync();
            return extendedCharity;
        }
    }
}
