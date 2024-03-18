using Charities.Data.Models;
using CharityProject.Data;
using CharityProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CharityProject.Contracts
{
    
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public CommentService(ApplicationDbContext _context, UserManager<User> userManager)
        {
            this.context = _context;
            this.userManager = userManager;
        }

        public async Task<Charity> AddCommentToCharity(Charity charity, Comment comment)
        {
            Charity extendedCharity = context.Charities.Where(c => c.Id == charity.Id).Include(c => c.Comments).ToList()[0];
            if (extendedCharity == null)
            {
                throw new ArgumentNullException("Charity not found.");
            }
            try
            {
                extendedCharity.Comments!.Add(comment);
                context.Charities.Update(charity);
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new ArgumentException("There was an error while posting this comment.");
            }
            return extendedCharity;
        }

        public Comment GetComment(Guid id)
        {
            Charity charity = context.Charities.Include(c => c.Comments).ToList()[0];
            return charity.Comments!.FirstOrDefault()!;
        }

        public async Task<Charity> DeleteComment(Guid commentId, Guid charityId)
        {
            Charity charity = context.Charities.Include(c => c.Comments).Where(c=>c.Id==charityId).ToList()[0];
            if (charity == null)
            {
                throw new ArgumentNullException("Charity not found.");
            }
            Comment comment = charity.Comments!.Where(c => c.Id == commentId).FirstOrDefault()!;
            if (comment == null)
            {
                throw new ArgumentNullException("Comment not found.");
            }
            try
            {
                charity.Comments!.Remove(comment);
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new ArgumentException("There was an error while deleting your comment.");
            }
            return charity;
        }
    
    }
}
