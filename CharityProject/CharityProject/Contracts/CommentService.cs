using Charities.Data.Models;
using CharityProject.Data;
using CharityProject.Services;
using Microsoft.AspNetCore.Identity;

/*
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
        public async Task<Comment> CreateComment(Comment comment, Guid charityId)
        {
           var charity =  await context.Charities.FindAsync(charityId);
             
        }
    
    }
}
*/