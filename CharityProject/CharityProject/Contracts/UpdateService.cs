using Charities.Data.Models;
using CharityProject.Data;
using CharityProject.Services;

namespace CharityProject.Contracts
{
    public class UpdateService : IUpdateService
    {
        private readonly ApplicationDbContext context;

        public UpdateService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public Update GetUpdate(Guid updateId, Charity charity)
        {
            return charity.Updates.FirstOrDefault(u=>u.Id==updateId)!;
        }

        public async Task<Charity> PostUpdateToCharity(Update update, Charity charity)
        {
            charity.Updates.Add(update);
            context.Charities.Update(charity);
            await context.SaveChangesAsync();
            return charity;
        }
        public async Task<Charity> DeleteUpdateFromCharity(Update update, Charity charity)
        {
            charity.Updates.Remove(update);
            context.Charities.Update(charity);
            await context.SaveChangesAsync();
            return charity;
        }
    }
}
