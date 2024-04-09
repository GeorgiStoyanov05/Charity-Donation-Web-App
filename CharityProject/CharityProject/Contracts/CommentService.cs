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

        public CommentService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task<Charity> AddCommentToCharity(Charity charity, Comment comment)
        {
            if (charity == null)
            {
                throw new ArgumentNullException("Charity not found.");
            }
            Charity extendedCharity = context.Charities.Where(c => c.Id == charity.Id).Include(c => c.Comments).ToList()[0];
            try
            {
                extendedCharity.Comments!.Add(comment);
                context.Charities.Update(charity);
                await context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new ArgumentException("Your comment can not be empty.");
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
            if (context.Charities.Where(c => c.Id == charityId).ToList().Count == 0)
            {
                throw new ArgumentNullException("Charity not found.");
            }
            Charity charity = context.Charities.Include(c => c.Comments).Where(c=>c.Id==charityId).ToList()[0];
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
