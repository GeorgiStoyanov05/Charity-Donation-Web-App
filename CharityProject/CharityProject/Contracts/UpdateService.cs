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
            Update update = charity.Updates.FirstOrDefault(u => u.Id == updateId)!;
            return update;
        }

        public async Task<Charity> PostUpdateToCharity(Update update, Charity charity)
        {
            if (update == null)
            {
                throw new ArgumentNullException("Update not found.");
            }
            if (charity == null)
            {
                throw new ArgumentNullException("Charity not found.");
            }
            try
            {
                charity.Updates.Add(update);
                context.Charities.Update(charity);
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new ArgumentException("There was an error while posting the update.");
            }
            return charity;
        }
        public async Task<Charity> DeleteUpdateFromCharity(Update update, Charity charity)
        {
            if (update == null)
            {
                throw new ArgumentNullException("Update not found.");
            }
            if (charity == null)
            {
                throw new ArgumentNullException("Charity not found.");
            }
            try
            {
                charity.Updates.Remove(update);
                context.Charities.Update(charity);
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new ArgumentException("There was an error while deleting the update.");
            }
            return charity;
        }
    }
}
