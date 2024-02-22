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
            extendedCharity.Comments!.Add(comment);
            context.Charities.Update(charity);
            await context.SaveChangesAsync();
            return extendedCharity;
        }

        public Comment GetComment(Guid id)
        {
            Charity charity = context.Charities.Include(c => c.Comments).ToList()[0];
            return charity.Comments!.FirstOrDefault()!;
        }

        public async Task<Charity> DeleteComment(Guid commentId)
        {
            Charity charity = context.Charities.Include(c => c.Comments).ToList()[0];
            Comment comment = charity.Comments!.Where(c => c.Id == commentId).FirstOrDefault()!;
            charity.Comments!.Remove(comment);
            await context.SaveChangesAsync();
            return charity;
        }
    
    }
}
