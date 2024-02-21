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
            return View(charities);
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
                User = currentUser
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(DetailsCaseViewModel model)
        {
            Comment comment = new Comment()
            {
                Text = model.Comment.Text,
                CreatedDate = DateTime.Now,
                UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                CharityId = model.Charity.Id
            };
            Charity charity = await caseService.GetCharity(model.Charity.Id);
            await caseService.AddCommentToCharity(charity, comment);
            return RedirectToAction("DetailsCase", "Cases", charity);
        }
    }
}
