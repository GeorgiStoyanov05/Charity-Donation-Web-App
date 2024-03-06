using Charities.Data.Models;

namespace CharityProject.Services
{
    public interface IUpdateService
    {
        Update GetUpdate(Guid updateId, Charity charity);

        Task<Charity> PostUpdateToCharity(Update update, Charity charity);

        Task<Charity> DeleteUpdateFromCharity(Update update, Charity charity);

    }
}
