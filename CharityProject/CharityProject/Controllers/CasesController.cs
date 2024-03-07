using Charities.Data.Models;
using CharityProject.Contracts;
using CharityProject.Models;
using CharityProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Charities.Controllers
{
    public class CasesController : Controller
    {
        private readonly ICaseService caseService;
        private readonly UserManager<User> userManager;

        public CasesController(ICaseService caseService, UserManager<User> _userManager)
        {
            this.caseService = caseService;
            this.userManager = _userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            List<Charity> charities = await caseService.GetAllCharities();
            AllCasesViewModel viewModel = new AllCasesViewModel()
            {
                Charities = charities
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Category> categories = await caseService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCaseViewModel model)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var charity = await caseService.CreateCharity(model, userId!);
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public async Task<IActionResult> DetailsCase(Guid id)
        {
            List<Charity> charities = await caseService.GetAllCharities();
            Charity charity = await caseService.GetCharity(id);
            User currentUser = await userManager.GetUserAsync(this.User);
            DetailsCaseViewModel viewModel = new DetailsCaseViewModel()
            {
                Charities = charities,
                Charity = charity,
                User = currentUser,
                BiggestDonations = charity.Donations.OrderByDescending(d => d.Amount).Take(3).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FilterSearch(AllCasesViewModel model)
        {
            List<Charity> charities = await caseService.GetAllCharities();
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (model.DonatedCharities == true)
            {
                if (model.CharityName == null)
                {
                    charities = charities.Where(c => c.Donations.Where(d => d.UserId == userId).ToList().Count != 0).ToList();
                }
                else
                {
                    charities = charities.Where(c => c.Name == model.CharityName && c.Donations.Where(d => d.UserId == userId).ToList().Count != 0).ToList();
                }
            }
            else
            {
                if (model.CharityName != null)
                {
                    charities = charities.Where(c => c.Name == model.CharityName).ToList();
                }
            }

            AllCasesViewModel viewModel = new AllCasesViewModel()
            {
                Charities = charities
            };
            return View("All" ,viewModel);
        }
    }
}
