using Charities.Data.Models;

namespace CharityProject.Services
{
    public interface ICommentService
    {
        Task<Charity> AddCommentToCharity(Charity charity, Comment comment);

        Task<Charity> DeleteComment(Guid commentId, Guid charityId);
    }
}
