using Charities.Data.Models;

namespace CharityProject.Services
{
    public interface ICommentService
    {
        Task<Comment> CreateComment(Comment comment);
    }
}
