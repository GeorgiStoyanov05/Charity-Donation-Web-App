using Charities.Data.Models;
using CharityProject.Models;

namespace CharityProject.Services
{
    public interface ICaseService
    {

        Task<Charity> CreateCharity(CreateCaseViewModel model, string userId);

        Task<List<Category>> GetAllCategories();

        Task<List<Charity>> GetAllCharities();
    }
}
